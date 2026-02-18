using Scellecs.Morpeh;

namespace Assets.Scripts.ECS.Systems
{
    public class ClearCollisionEventSystem : ISystem
    {
        public World World { get; set; }

        private Stash<CollisionEvent> _collisionStash;

        public void OnAwake() => _collisionStash = World.GetStash<CollisionEvent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in World.Filter.With<CollisionEvent>().Build())
            {
                _collisionStash.Remove(e);
            }
        }

        public void Dispose() { }
    }
}
