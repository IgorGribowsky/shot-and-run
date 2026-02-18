using Assets.Scripts.Zenject.Pools;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class BulletLifetimeSystem : ISystem
    {
        public World World { get; set; }

        private Filter _bulletsFilter;

        private Stash<MovedDistance> _movedDistanceStash;
        private Stash<Bullet> _bulletStash;

        [Inject] private BulletPool _pool;

        public void OnAwake()
        {
            _movedDistanceStash = World.GetStash<MovedDistance>();
            _bulletStash = World.GetStash<Bullet>();

            _bulletsFilter = World.Filter
                .With<Bullet>()
                .With<MovedDistance>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var bullet in _bulletsFilter)
            {
                ref var movedDistance = ref _movedDistanceStash.Get(bullet);
                ref var bulletComponent = ref _bulletStash.Get(bullet);

                if (movedDistance.Current >= movedDistance.Max)
                {
                    _pool.Despawn(bulletComponent.Authoring);
                }
            }
        }

        public void Dispose() { }
    }
}
