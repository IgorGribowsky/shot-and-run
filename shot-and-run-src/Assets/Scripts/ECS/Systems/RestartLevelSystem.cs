using Scellecs.Morpeh;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class RestartLevelSystem : ISystem
    {
        public World World { get; set; }

        private Stash<UnitsCount> _unitsCountStash;

        private Entity _armyEntity;

        [Inject] private LevelManager _levelManager;

        public void OnAwake()
        {
            _unitsCountStash = World.GetStash<UnitsCount>();

            var armyFilter = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .With<ArmyStats>()
                .Build();

            _armyEntity = armyFilter.First();
        }

        public void OnUpdate(float deltaTime)
        {
            var unitsCount = _unitsCountStash.Get(_armyEntity).Value; 

            if (unitsCount <= 0)
            {
                _levelManager.LoadCurrentLevel();
            }
        }

        public void Dispose() { }
    }
}
