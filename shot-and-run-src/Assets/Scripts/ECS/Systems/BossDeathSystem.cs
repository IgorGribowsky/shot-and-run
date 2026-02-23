using Assets.Scripts.Firebase.Events;
using Assets.Scripts.Firebase.Interfaces;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class BossDeathSystem : ISystem
    {
        public World World { get; set; }

        private Filter _bossFilter;

        private Stash<Health> _healthStash;
        private Stash<View> _viewStash;
        private Stash<UnitsCount> _unitsCountStash;

        private Entity _armyEntity;

        [Inject] private IEventSender _eventSender;
        [Inject] private LevelManager _levelManager;
        [Inject] private MenuController _menuController;

        public void OnAwake()
        {
            _bossFilter = World.Filter
                .With<Boss>()
                .With<Health>()
                .Build();
            _armyEntity = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .Build()
                .First();

            _unitsCountStash = World.GetStash<UnitsCount>();

            _healthStash = World.GetStash<Health>();
            _viewStash = World.GetStash<View>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var boss in _bossFilter)
            {
                ref var health = ref _healthStash.Get(boss);
                if (health.Value <= 0)
                {
                    ref var view = ref _viewStash.Get(boss);
                    Object.Destroy(view.Transform.gameObject);

                    var score = _unitsCountStash.Get(_armyEntity).Value;

                    _eventSender.SendEvent(new LevelCompletedEvent(_levelManager.CurrentLevel, score));
                    _menuController.SetField("score", score.ToString());
                    _menuController.Show();
                }
            }
        }

        public void Dispose() { }
    }
}
