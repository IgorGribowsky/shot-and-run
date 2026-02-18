using Assets.Scripts.Domen.Helpers;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class ArchCollectionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _collidedArchsFilter;
        private Filter _activeWavesFilter;
        private Entity _armyEntity;

        private Stash<Inactive> _inactivesStash;
        private Stash<WaveIdentity> _waveIdentitiesStash;
        private Stash<CollisionEvent> _collisionsStash;
        private Stash<Unit> _unitsStash;
        private Stash<UnitsCount> _unitsCountsStash;
        private Stash<Bonus> _bonusesStash;
        private Stash<ArmyCountCanvas> _armyCountCanvasStash;

        [Inject] private IBonusCalculator _bonusCalculator;

        public void OnAwake()
        {
            _collidedArchsFilter = World.Filter
                .With<Arch>()
                .With<Bonus>()
                .With<WaveIdentity>()
                .With<CollisionEvent>()
                .Build();

            _activeWavesFilter = World.Filter
                .With<Wave>()
                .With<WaveIdentity>()
                .Without<Inactive>()
                .Build();

            _armyEntity = World.Filter
                .With<Army>()
                .With<UnitsCount>()
                .With<ArmyCountCanvas>()
                .Build()
                .First();

            _inactivesStash = World.GetStash<Inactive>();
            _waveIdentitiesStash = World.GetStash<WaveIdentity>();
            _collisionsStash = World.GetStash<CollisionEvent>();
            _unitsStash = World.GetStash<Unit>();
            _unitsCountsStash = World.GetStash<UnitsCount>();
            _bonusesStash = World.GetStash<Bonus>();
            _armyCountCanvasStash = World.GetStash<ArmyCountCanvas>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var arch in _collidedArchsFilter)
            {
                var collisionData = _collisionsStash.Get(arch);

                if (collisionData.Others == null) continue;

                foreach (var otherEntity in collisionData.Others)
                {
                    if (World.IsDisposed(otherEntity)) continue;

                    if (!_unitsStash.Has(otherEntity)) continue;

                    var archWaveId = _waveIdentitiesStash.Get(arch).Value;
                    bool isWaveActivatedThisFrame = false;

                    foreach (var wave in _activeWavesFilter)
                    {
                        if (_inactivesStash.Has(wave)) continue;

                        var waveId = _waveIdentitiesStash.Get(wave).Value;
                        if (archWaveId == waveId)
                        {
                            isWaveActivatedThisFrame = true;

                            _inactivesStash.Add(wave);
                            break;
                        }
                    }

                    if (isWaveActivatedThisFrame)
                    {
                        ref var bonus = ref _bonusesStash.Get(arch);
                        ref var armyCount = ref _unitsCountsStash.Get(_armyEntity);

                        var newValue = _bonusCalculator.CalculateNewValue(armyCount.Value, bonus.BonusSign, bonus.Value);
                        armyCount.Value = newValue;

                        ref var armyCountCanvas = ref _armyCountCanvasStash.Get(_armyEntity);
                        armyCountCanvas.IsTextUpdated = true;

                        break;
                    }
                }
            }
        }

        public void Dispose() { }
    }
}