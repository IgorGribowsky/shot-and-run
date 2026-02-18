using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class BulletAuthoring : MonoBehaviour, IEntityAuthoring, IPoolable<Vector3, float>
{
    private Stash<Bullet> _bulletStash;
    private Stash<View> _viewStash;
    private Stash<MovedDistance> _movedDistance;

    [Inject] private World _world;

    public Entity Entity { get; set; }

    private void Awake()
    {
        _bulletStash = _world.GetStash<Bullet>();
        _viewStash = _world.GetStash<View>();
        _movedDistance = _world.GetStash<MovedDistance>();
    }

    public void ConfigureComponents(float maxDistance)
    {
        if (_world.IsDisposed(Entity))
        {
            Entity = _world.CreateEntity();
        }

        ref var bullet = ref _bulletStash.Add(Entity);
        bullet.Authoring = this;

        ref var view = ref _viewStash.Add(Entity);
        view.Transform = transform;

        ref var movedDistance = ref _movedDistance.Add(Entity);
        movedDistance.Max = maxDistance;
        movedDistance.Current = default;
    }

    public void OnSpawned(Vector3 localPosition, float maxDistance)
    {
        transform.position = localPosition;

        ConfigureComponents(maxDistance);
    }

    public void OnDespawned()
    {
        if (!_world.IsDisposed(Entity))
        {
            _world.RemoveEntity(Entity);
        }
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(Entity))
        {
            _world.RemoveEntity(Entity);
        }
    }
}
