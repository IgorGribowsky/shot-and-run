using Assets.Scripts.Domen.Enums;
using Assets.Scripts.Domen.Factories;
using Scellecs.Morpeh;
using System.Linq;
using Zenject;

namespace Assets.Scripts.ECS.Systems
{
    public class SpawnSystem : ISystem
    {
        public World World { get; set; }

        private Stash<BalanceConfig> _balanceConfigStash;
        private Stash<WavesInfo> _wavesInfoStash;
        private Stash<Wave> _waveStash;
        private Stash<WaveIdentity> _waveIdStash;
        private Stash<BonusCanvas> _bonusCanvasStash;
        private Stash<CameraEntity> _cameraStash;

        private Entity _levelEntity;
        private Entity _cameraEntity;

        private float _timer = 0;
        private bool _stop = false;

        [Inject] ITrackObjectFactory _trackObjectFactory;

        public void OnAwake()
        {
            _balanceConfigStash = World.GetStash<BalanceConfig>();
            _wavesInfoStash = World.GetStash<WavesInfo>();
            _waveStash = World.GetStash<Wave>();
            _waveIdStash = World.GetStash<WaveIdentity>();
            _bonusCanvasStash = World.GetStash<BonusCanvas>();
            _cameraStash = World.GetStash<CameraEntity>();

            _levelEntity = World.Filter
                .With<Level>()
                .With<BalanceConfig>()
                .With<WavesInfo>()
                .Build()
                .First();

            _cameraEntity = World.Filter
                .With<CameraEntity>()
                .Build()
                .First();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_stop)
            {
                return;
            }

            _timer -= deltaTime;

            if (_timer <= 0)
            {
                ref var balance = ref _balanceConfigStash.Get(_levelEntity);
                ref var wavesInfo = ref _wavesInfoStash.Get(_levelEntity);

                _timer = balance.WaveRate;

                Spawn(ref wavesInfo, ref balance);

                wavesInfo.CurrentWave++;

                if (wavesInfo.CurrentWave >= wavesInfo.Waves.Count)
                {
                    ref var camera = ref _cameraStash.Get(_cameraEntity);
                    camera.Phase = CameraPhase.Rotating;
                    _stop = true;
                }
            }
        }


        public void Spawn(ref WavesInfo wavesInfo, ref BalanceConfig balanceConfig)
        {
            var currentWave = wavesInfo.Waves[wavesInfo.CurrentWave];

            CreateWave(wavesInfo);

            for (int i = 0; i < balanceConfig.TrackCount; i++)
            {
                var trackSO = currentWave.TrackObjects.ElementAtOrDefault(i);
                if (trackSO != null)
                {
                    var gameObject = _trackObjectFactory.CreateByType(trackSO.Type, balanceConfig.TrackCount, i);
                    var entity = gameObject.GetComponent<IEntityAuthoring>().Entity;

                    if (gameObject.TryGetComponent<ArchAuthoring>(out var archAuthoring))
                    {
                        archAuthoring.SetWaveId(wavesInfo.CurrentWave);
                    }

                    if (trackSO.BonusSign != BonusSign.None && gameObject.TryGetComponent<BonusAuthoring>(out var bonusAuthoring))
                    {
                        bonusAuthoring.SetBonus(entity, trackSO.BonusSign, trackSO.Value);
                        ref var canvas = ref _bonusCanvasStash.Get(entity);
                        canvas.IsTextUpdated = true;
                    }

                    if (gameObject.TryGetComponent<HealthAuthoring>(out var healthAuthoring))
                    {
                        healthAuthoring.SetHealth(entity, currentWave.Hp);
                    }
                }
            }
        }

        private void CreateWave(WavesInfo wavesInfo)
        {
            var waveEntity = World.CreateEntity();
            _waveStash.Add(waveEntity);
            ref var waveIdentity = ref _waveIdStash.Add(waveEntity);
            waveIdentity.Value = wavesInfo.CurrentWave;
        }

        public void Dispose() { }
    }
}
