using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class UnitAuthoring : MonoBehaviour, IEntityAuthoring, IPoolable<Vector3, Transform>
{
    private Entity _entity;

    private Stash<Unit> _unitStash;
    private Stash<View> _viewStash;

    [Inject] private World _world;

    public Entity Entity => _entity;

    private void Awake()
    {
        _unitStash = _world.GetStash<Unit>();
        _viewStash = _world.GetStash<View>();
    }

    public void ConfigureComponents()
    {
        _entity = _world.CreateEntity();

        _unitStash.Add(_entity);

        ref var view = ref _viewStash.Add(_entity);
        view.Transform = transform;
    }

    public void OnSpawned(Vector3 localPosition, Transform parentTransform)
    {
        transform.SetParent(parentTransform);
        transform.localPosition = localPosition;

        ConfigureComponents();
    }

    public void OnDespawned()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }
    }
}
