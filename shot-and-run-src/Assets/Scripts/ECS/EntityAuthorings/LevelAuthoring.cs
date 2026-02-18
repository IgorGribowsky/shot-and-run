using Scellecs.Morpeh;
using UnityEngine;
using Zenject;
using Assets.Scripts.Domen.SerializableData;

public class LevelAuthoring : MonoBehaviour, IEntityAuthoring
{
    public GameObject Plate;
    public LevelData Level;

    private Entity _entity;

    private Stash<Level> _levelStash;
    private Stash<MovementBorders> _movementBordersStash;
    private Stash<BalanceConfig> _balanceConfigStash;
    private Stash<WavesInfo> _wavesInfoStash;

    [Inject] private World _world;

    public Entity Entity => _entity;

    //TODO MOVE TO CONSTANTS
    public const float MinZBorder = -1f;
    public const float MaxZBorder = 3f;

    private void Awake()
    {
        _levelStash = _world.GetStash<Level>();
        _movementBordersStash = _world.GetStash<MovementBorders>();
        _balanceConfigStash = _world.GetStash<BalanceConfig>();
        _wavesInfoStash = _world.GetStash<WavesInfo>();

        ConfigureComponents();
    }

    public void ConfigureComponents()
    {
        _entity = _world.CreateEntity();

        _levelStash.Add(_entity);

        ref var borders = ref _movementBordersStash.Add(_entity);
        borders.MinZ = MinZBorder;
        borders.MaxZ = MaxZBorder;

        var xScale = Level.TrackCount * 0.5f;
        var maxX = xScale * 5f - 0.25f;
        var minX = -maxX;
        Plate.transform.localScale = new Vector3(xScale, 1, 10);

        borders.MaxX = maxX;
        borders.MinX = minX;

        ref var balanceConfig = ref _balanceConfigStash.Add(_entity);
        balanceConfig.WaveRate = Level.WaveRate;
        balanceConfig.ObstacleSpeed = Level.ObstacleSpeed;
        balanceConfig.TrackCount = Level.TrackCount;

        ref var wavesInfo = ref _wavesInfoStash.Add(_entity);
        wavesInfo.Waves = Level.Waves;
    }

    private void OnDestroy()
    {
        if (!_world.IsDisposed(_entity))
        {
            _world.RemoveEntity(_entity);
        }
    }
}
