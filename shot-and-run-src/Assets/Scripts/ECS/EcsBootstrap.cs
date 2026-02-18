using Assets.Scripts.ECS.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class EcsBootstrap: MonoBehaviour
{
    private SystemsGroup _systems;

    [Inject] private World _world;
    [Inject] private InputSystem _inputSystem;
    [Inject] private CameraMovementSystem _cameraMovementSystem;
    [Inject] private UnitShootingSystem _unitShootingSystem;
    [Inject] private ArmyMovementSystem _armyMovementSystem;
    [Inject] private BossMovementSystem _bossMovementSysyem;
    [Inject] private BarrelMovementSystem _barrelMovementSystem;
    [Inject] private BulletMovementSystem _bulletMovementSystem;
    [Inject] private ArchCollectionSystem _archCollectionSystem;
    [Inject] private ArmyCapacitySyncSystem _armyCapacitySyncSystem;
    [Inject] private ArchMovementSystem _archMovementSystem;
    [Inject] private BonusCanvasDisplayingSystem _bonusCanvasDisplayingSystem;
    [Inject] private HealthCanvasDisplayingSystem _healthCanvasDisplayingSystem;
    [Inject] private ArmyCountCanvasDisplayingSystem _armyCountCanvasDisplayingSystem;
    [Inject] private SpawnSystem _spawnSystem;
    [Inject] private EnemyCollisionSystem _enemyCollisionSystem;
    [Inject] private BossDeathSystem _bossDeathSystem;
    [Inject] private BarrelCollectionSystem _barrelCollectionSystem;
    [Inject] private BulletLifetimeSystem _bulletLifetimeSystem;
    [Inject] private ClearCollisionEventSystem _clearCollisionEventSystem;
    [Inject] private RestartLevelSystem _restartLevelSystem;

    void Start()
    {
        _systems = _world.CreateSystemsGroup();

        _systems.AddSystem(_inputSystem);
        _systems.AddSystem(_cameraMovementSystem);
        _systems.AddSystem(_unitShootingSystem);
        _systems.AddSystem(_bossMovementSysyem);
        _systems.AddSystem(_barrelMovementSystem);
        _systems.AddSystem(_armyMovementSystem);
        _systems.AddSystem(_bulletMovementSystem);
        _systems.AddSystem(_archCollectionSystem);
        _systems.AddSystem(_armyCapacitySyncSystem);
        _systems.AddSystem(_archMovementSystem);
        _systems.AddSystem(_bonusCanvasDisplayingSystem);
        _systems.AddSystem(_healthCanvasDisplayingSystem);
        _systems.AddSystem(_armyCountCanvasDisplayingSystem);
        _systems.AddSystem(_spawnSystem);
        _systems.AddSystem(_enemyCollisionSystem);
        _systems.AddSystem(_bossDeathSystem);
        _systems.AddSystem(_barrelCollectionSystem);
        _systems.AddSystem(_bulletLifetimeSystem);
        _systems.AddSystem(_clearCollisionEventSystem);
        _systems.AddSystem(_restartLevelSystem);

        _systems.Initialize();
    }

    void Update()
    {
        _systems.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _systems.Dispose();
    }
}
