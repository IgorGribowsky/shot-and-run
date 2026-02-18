using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Domen.Factories;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class ArmyCapacitySyncSystem : ISystem
    {
        public World World { get; set; }

        private Stash<View> _viewsStash;
        private Stash<UnitsCount> _unitsCountStash;

        private Transform _armyTransform;
        private Entity _armyEntity;

        [Inject] private IUnitFactory _unitFactory;

        public void OnAwake()
        {
            _viewsStash = World.GetStash<View>();
            _unitsCountStash = World.GetStash<UnitsCount>();

            var armyFilter = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .With<ArmyStats>()
                .Build();

            _armyEntity = armyFilter.First();

            ref var armyView = ref _viewsStash.Get(_armyEntity);
            _armyTransform = armyView.Transform;

            var unitsCount = _unitsCountStash.Get(_armyEntity).Value;

            AddUnits(unitsCount);
        }

        public void OnUpdate(float deltaTime)
        {
            var unitsCount = _unitsCountStash.Get(_armyEntity).Value;
            var dif = unitsCount - _unitFactory.UnitsCount;

            if (dif > 0)
            {
                AddUnits(dif);
            }
            else if (dif < 0)
            {
                RemoveUnits(Mathf.Abs(dif));
            }
        }

        private void AddUnits(int count)
        {
            for (int i = 0; i < count; i++) 
            {
                CreateUnit();
            }
        }

        private void RemoveUnits(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _unitFactory.DeleteLastUnit();
            }
        }

        private void CreateUnit()
        {
            var randomVector = Random.insideUnitCircle * GameConstants.UnitSpreading;
            var positionToAdd = _unitFactory.UnitsCount != 0
                ? new Vector3(randomVector.x, 0, randomVector.y)
                : new Vector3(0, 0, 0);

            _unitFactory.CreateUnit(positionToAdd, _armyTransform);
        }

        public void Dispose() { }
    }
}
