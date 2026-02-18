using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class ControllerAuthoring : MonoBehaviour, IEntityAuthoring
{
    private Entity _entity;

    private Stash<Controller> _controllerStash;
    private Stash<MovementInput> _movementInputStash;

    [Inject] private World _world;

    public Entity Entity => _entity;

    private void Awake()
    {
        _controllerStash = _world.GetStash<Controller>();
        _movementInputStash = _world.GetStash<MovementInput>();

        ConfigureComponents();
    }

    public void ConfigureComponents()
    {
        _entity = _world.CreateEntity();

        _controllerStash.Add(_entity);

        ref var movementImput = ref _movementInputStash.Add(_entity);
        movementImput.Direction = new Vector2();
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }
    }
}
