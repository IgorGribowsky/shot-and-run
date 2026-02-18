using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class BarrelAuthoring : MonoBehaviour, IEntityAuthoring
{
    private Stash<Barrel> _barrelStash;
    private Stash<View> _viewStash;
    private Stash<Enemy> _enemyStash;

    [Inject] private World _world;

    public Entity Entity { get; set; }

    private void Awake()
    {
        _barrelStash = _world.GetStash<Barrel>();
        _viewStash = _world.GetStash<View>();
        _enemyStash = _world.GetStash<Enemy>();

        ConfigureComponents();
    }

    public void ConfigureComponents()
    {
        if (_world.IsDisposed(Entity))
        {
            Entity = _world.CreateEntity();
        }

        _barrelStash.Add(Entity);
        _enemyStash.Add(Entity);

        ref var view = ref _viewStash.Add(Entity);
        view.Transform = transform;

    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(Entity))
        {
            _world.RemoveEntity(Entity);
        }
    }
}
