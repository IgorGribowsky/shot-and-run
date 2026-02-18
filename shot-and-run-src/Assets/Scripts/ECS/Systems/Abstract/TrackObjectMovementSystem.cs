using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public abstract class TrackObjectMovementSystem<T> : ISystem where T : struct, IComponent
    {
        public World World { get; set; }

        protected Stash<View> ViewsStash;
        protected Stash<BalanceConfig> BalanceConfigStash;

        private Entity _levelEntity;
        private Filter _filter;

        private const float ZDeadLine = -10f;

        public virtual void OnAwake()
        {
            ViewsStash = World.GetStash<View>();
            BalanceConfigStash = World.GetStash<BalanceConfig>();

            _levelEntity = World.Filter
                .With<Level>()
                .With<BalanceConfig>()
                .Build()
                .First();

            _filter = World.Filter
                .With<T>()
                .With<View>()
                .Build();
        }

        public virtual void OnUpdate(float deltaTime)
        {
            var speed = BalanceConfigStash.Get(_levelEntity).ObstacleSpeed;
            var vz = deltaTime * speed;

            foreach (var entity in _filter)
            {
                ref var view = ref ViewsStash.Get(entity);

                view.Transform.position += new Vector3(0, 0, -vz);

                ProcessAdditionalMovement(entity, ref view, vz);

                if (view.Transform.position.z <= ZDeadLine)
                {
                    GameObject.Destroy(view.Transform.gameObject);
                }
            }
        }

        protected virtual void ProcessAdditionalMovement(Entity entity, ref View view, float vz) { }

        public virtual void Dispose() { }
    }
}