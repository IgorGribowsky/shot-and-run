using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class ArmyMovementSystem : ISystem
    {
        public World World { get; set; }

        private Stash<MovementInput> _movementInputStash;
        private Stash<View> _viewsStash;
        private Stash<MovementBorders> _movementBordersStash;

        private Entity _controllerEntity;
        private Entity _armyEntity;

        private Filter _unitsFilter;

        private float _maxX;
        private float _minX;
        private float _maxZ;
        private float _minZ;

        //TODO MOVE TO CONSTS
        private const float Speed = 35;

        public void OnAwake()
        {
            _movementInputStash = World.GetStash<MovementInput>();
            _viewsStash = World.GetStash<View>();
            _movementBordersStash = World.GetStash<MovementBorders>();

            var controllerFilter = World.Filter
                .With<Controller>()
                .With<MovementInput>()
                .Build();

            _controllerEntity = controllerFilter.First();

            var armyFilter = World.Filter
                .With<Army>()
                .With<ArmyStats>()
                .With<View>()
                .Build();

            _armyEntity = armyFilter.First();

            _unitsFilter = World.Filter
                .With<Unit>()
                .With<View>()
                .Build();

            var levelFilter = World.Filter
                .With<Level>()
                .With<MovementBorders>()
                .Build();
            var levelEntity = levelFilter.First();
            ref var borders = ref _movementBordersStash.Get(levelEntity);
            _maxX = borders.MaxX;
            _minX = borders.MinX;
            _maxZ = borders.MaxZ;
            _minZ = borders.MinZ;
        }

        public void OnUpdate(float deltaTime)
        {
            var movementDirection = _movementInputStash.Get(_controllerEntity).Direction;
            if (movementDirection.sqrMagnitude > 0)
            {
                float leftUnitX = float.MaxValue;
                float rightUnitX = float.MinValue;
                float downUnitZ = float.MaxValue;
                float topUnitZ = float.MinValue;

                foreach (var unit in _unitsFilter)
                {
                    Vector3 pos = _viewsStash.Get(unit).Transform.position;

                    if (pos.x < leftUnitX) leftUnitX = pos.x;
                    if (pos.x > rightUnitX) rightUnitX = pos.x;
                    if (pos.z < downUnitZ) downUnitZ = pos.z;
                    if (pos.z > topUnitZ) topUnitZ = pos.z;
                }

                float moveX = movementDirection.x * deltaTime * Speed;
                float moveZ = movementDirection.y * deltaTime * Speed;

                var x = CalculateMove(leftUnitX, rightUnitX, moveX, _minX, _maxX);
                var z = CalculateMove(downUnitZ, topUnitZ, moveZ, _minZ, _maxZ);

                ref var armyView = ref _viewsStash.Get(_armyEntity);
                armyView.Transform.position += new Vector3(x, 0, z);
            }
        }

        private float CalculateMove(float minPos, float maxPos, float move, float minLimit, float maxLimit)
        {
            if (move < 0)
            {
                var newPos = minPos + move;
                return newPos < minLimit ? minLimit - minPos : move;
            }
            else
            {
                var newPos = maxPos + move;
                return newPos > maxLimit ? maxLimit - maxPos : move;
            }
        }

        public void Dispose() { }
    }
}
