using Assets.Scripts.Zenject.Pools;
using Scellecs.Morpeh;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class UnitShootingSystem : ISystem
    {
        public World World { get; set; }

        private Entity _armyEntity;

        private Filter _unitsFilter;

        private Stash<View> _viewsStash;
        private Stash<ArmyStats> _armyStatsStash;

        private float currentCd = 0f;

        [Inject] private BulletPool _pool;

        public void OnAwake()
        {
            _viewsStash = World.GetStash<View>();
            _armyStatsStash = World.GetStash<ArmyStats>();

            _armyEntity = World.Filter
                .With<Army>()
                .With<ArmyStats>()
                .Build()
                .First();

            _unitsFilter = World.Filter
                .With<Unit>()
                .With<View>()
                .Build();

            ref var stats = ref _armyStatsStash.Get(_armyEntity);
            currentCd = 1 / stats.AttackSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            currentCd -= deltaTime;

            if (currentCd <= 0)
            {
                ref var stats = ref _armyStatsStash.Get(_armyEntity);

                Shoot(ref stats);

                currentCd = 1 / stats.AttackSpeed;
            }
        }

        private void Shoot(ref ArmyStats armyStats)
        {
            foreach (var unit in _unitsFilter)
            {
                ref var view = ref _viewsStash.Get(unit);
                _pool.Spawn(view.Transform.position, armyStats.BulletRange);
            }
        }

        public void Dispose() { }
    }
}
