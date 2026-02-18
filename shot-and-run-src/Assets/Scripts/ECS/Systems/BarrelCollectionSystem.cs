using Assets.Scripts.Domen.Helpers;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class BarrelCollectionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _barrelsFilter;
        private Entity _armyEntity;

        private Stash<Health> _healthStash;
        private Stash<UnitsCount> _unitsCountStash;
        private Stash<Bonus> _bonusStash;
        private Stash<ArmyCountCanvas> _armyCountCanvasStash;
        private Stash<View> _viewStash;

        [Inject] private IBonusCalculator _bonusCalculator;

        public void OnAwake()
        {
            _barrelsFilter = World.Filter
                .With<Barrel>()
                .With<Health>()
                .Build();

            _armyEntity = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .Build()
                .First();

            _unitsCountStash = World.GetStash<UnitsCount>();
            _healthStash = World.GetStash<Health>();
            _bonusStash = World.GetStash<Bonus>();
            _armyCountCanvasStash = World.GetStash<ArmyCountCanvas>();
            _viewStash = World.GetStash<View>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var barrel in _barrelsFilter)
            {
                ref var health = ref _healthStash.Get(barrel);
                if (health.Value <= 0)
                {
                    if (_bonusStash.Has(barrel))
                    {
                        GainBonus(barrel);
                    }

                    ref var view = ref _viewStash.Get(barrel);
                    Object.Destroy(view.Transform.gameObject);
                }
            }
        }

        private void GainBonus(Entity barrel)
        {
            ref var bonus = ref _bonusStash.Get(barrel);
            ref var unitsCount = ref _unitsCountStash.Get(_armyEntity);
            unitsCount.Value = _bonusCalculator.CalculateNewValue(unitsCount.Value, bonus.BonusSign, bonus.Value);

            ref var armyCountCanvas = ref _armyCountCanvasStash.Get(_armyEntity);
            armyCountCanvas.IsTextUpdated = true;
        }

        public void Dispose() { }
    }
}
