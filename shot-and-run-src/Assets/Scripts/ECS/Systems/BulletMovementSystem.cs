using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class BulletMovementSystem : ISystem
    {
        public World World { get; set; }

        private Entity _armyEntity;

        private Filter _bulletsFilter;

        private Stash<View> _viewsStash;
        private Stash<ArmyStats> _armyStatsStash;
        private Stash<MovedDistance> _movedDistanceStash;

        public void OnAwake()
        {
            _viewsStash = World.GetStash<View>();
            _armyStatsStash = World.GetStash<ArmyStats>();
            _movedDistanceStash = World.GetStash<MovedDistance>();

            _armyEntity = World.Filter
                .With<Army>()
                .With<ArmyStats>()
                .Build()
                .First();

            _bulletsFilter = World.Filter
                .With<Bullet>()
                .With<MovedDistance>()
                .With<View>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            var speed = _armyStatsStash.Get(_armyEntity).BulletSpeed;

            foreach (var bullet in _bulletsFilter)
            {
                ref var movedDistance = ref _movedDistanceStash.Get(bullet);
                ref var view = ref _viewsStash.Get(bullet);

                var dz = speed * Time.deltaTime;
                view.Transform.position += new Vector3(0, 0, dz);
                movedDistance.Current += dz;
            }
        }

        public void Dispose() { }
    }
}
