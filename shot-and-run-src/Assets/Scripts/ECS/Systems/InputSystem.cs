using Assets.Scripts.Domen.Controller;
using Scellecs.Morpeh;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class InputSystem : ISystem
    {
        public World World { get; set; }

        private Stash<MovementInput> _movementInputStash;

        private Entity _controllerEntity;

        [Inject] private IController _controller;

        public void OnAwake()
        {
            var filter = World.Filter
                .With<Controller>()
                .With<MovementInput>()
                .Build();

            _controllerEntity = filter.First();

            _movementInputStash = World.GetStash<MovementInput>();
        }

        public void OnUpdate(float deltaTime)
        {
            var direction = _controller.GetDirection();
            ref var movementInput = ref _movementInputStash.Get(_controllerEntity);
            movementInput.Direction = direction;
        }

        public void Dispose() { }
    }
}
