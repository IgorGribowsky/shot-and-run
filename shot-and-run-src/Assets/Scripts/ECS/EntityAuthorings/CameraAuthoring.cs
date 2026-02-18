using Assets.Scripts.Domen.Enums;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class CameraAuthoring : MonoBehaviour, IEntityAuthoring
{
    private Entity _entity;

    private Stash<CameraEntity> _cameraStash;
    private Stash<View> _viewStash;

    [Inject] private World _world;

    public Entity Entity => _entity;

    private void Awake()
    {
        _cameraStash = _world.GetStash<CameraEntity>();
        _viewStash = _world.GetStash<View>();

        ConfigureComponents();
    }

    public void ConfigureComponents()
    {
        _entity = _world.CreateEntity();

        ref var camera = ref _cameraStash.Add(_entity);
        camera.Phase = CameraPhase.PreRotation;

        ref var view = ref _viewStash.Add(_entity);
        view.Transform = transform;
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }
    }
}
