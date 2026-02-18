using Assets.Scripts.Domen.Factories;
using Assets.Scripts.Zenject.Pools;
using Scellecs.Morpeh;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class EnemyCollisionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _collidedEnemyFilter;

        private Stash<CollisionEvent> _collisionsStash;
        private Stash<UnitsCount> _unitsCountStash;
        private Stash<Unit> _unitsStash;
        private Stash<Bullet> _bulletStash;
        private Stash<ArmyStats> _armyStatsStash;
        private Stash<Health> _healthStash;
        private Stash<HealthCanvas> _healthCanvasStash;
        private Stash<ArmyCountCanvas> _armyCountCanvasStash;

        private Entity _armyEntity;

        [Inject] private IUnitFactory _unitFactory;
        [Inject] private BulletPool _bulletPool;

        public void OnAwake()
        {
            _collidedEnemyFilter = World.Filter
                .With<Enemy>()
                .With<Health>()
                .With<HealthCanvas>()
                .With<CollisionEvent>()
                .Build();

            var armyFilter = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .With<ArmyStats>()
                .Build();

            _armyEntity = armyFilter.First();

            _collisionsStash = World.GetStash<CollisionEvent>();
            _unitsCountStash = World.GetStash<UnitsCount>();
            _armyCountCanvasStash = World.GetStash<ArmyCountCanvas>();
            _unitsStash = World.GetStash<Unit>();
            _bulletStash = World.GetStash<Bullet>();
            _armyStatsStash = World.GetStash<ArmyStats>();
            _healthCanvasStash = World.GetStash<HealthCanvas>();
            _healthStash = World.GetStash<Health>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var enemy in _collidedEnemyFilter)
            {
                var otherEntities = _collisionsStash.Get(enemy).Others;
                foreach (var otherEntity in otherEntities)
                {
                    if (World.IsDisposed(otherEntity))
                    {
                        continue;
                    }

                    if (_unitsStash.Has(otherEntity))
                    {
                        var isUnitDeleted = _unitFactory.TryDeleteSpecificUnit(otherEntity);
                        if (isUnitDeleted)
                        {
                            ref var unitsCount = ref _unitsCountStash.Get(_armyEntity);
                            unitsCount.Value -= 1;
                            ref var armyCountCanvas = ref _armyCountCanvasStash.Get(_armyEntity);
                            armyCountCanvas.IsTextUpdated = true;
                        }
                    }
                    else if (_bulletStash.Has(otherEntity))
                    {
                        ref var bullet = ref _bulletStash.Get(otherEntity);
                        ref var armyStats = ref _armyStatsStash.Get(_armyEntity);
                        ref var health = ref _healthStash.Get(enemy);
                        ref var healthCanvas = ref _healthCanvasStash.Get(enemy);

                        health.Value -= armyStats.Damage;
                        healthCanvas.IsTextUpdated = true;

                        _bulletPool.Despawn(bullet.Authoring);
                    }
                }
            }
        }

        public void Dispose() { }
    }
}
