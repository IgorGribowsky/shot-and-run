using Assets.Scripts.Domen.Enums;
using Assets.Scripts.Domen.Factories;
using Assets.Scripts.Domen.Helpers;
using Assets.Scripts.ECS.Systems;
using Assets.Scripts.Zenject.Pools;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    public GameObject Menu;

    public GameObject UnitPrefab;
    public GameObject BarrelPrefab;
    public GameObject ArchPrefab;
    public GameObject BossPrefab;
    public GameObject BulletPrefab;

    public override void InstallBindings()
    {
        //World
        Container.Bind<World>()
            .FromMethod(_ => World.Create())
            .AsSingle();

        //Systems
        Container.BindInterfacesAndSelfTo<CameraMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArmyMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArchMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BossMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BarrelMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletMovementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnitShootingSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArchCollectionSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArmyCapacitySyncSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BonusCanvasDisplayingSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthCanvasDisplayingSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ArmyCountCanvasDisplayingSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyCollisionSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BossDeathSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BarrelCollectionSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletLifetimeSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ClearCollisionEventSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestartLevelSystem>().AsSingle();

        //Pools
        Container.BindMemoryPool<UnitAuthoring, UnitPool>()
            .WithInitialSize(100)
            .FromComponentInNewPrefab(UnitPrefab)
            .UnderTransformGroup("UnitsPool");
        Container.BindMemoryPool<BulletAuthoring, BulletPool>()
            .WithInitialSize(200)
            .FromComponentInNewPrefab(BulletPrefab)
            .UnderTransformGroup("BulletsPool");

        //Prefabs
        Container.Bind<GameObject>().WithId(TrackObjectType.Barrel).FromInstance(BarrelPrefab);
        Container.Bind<GameObject>().WithId(TrackObjectType.Arch).FromInstance(ArchPrefab);
        Container.Bind<GameObject>().WithId(TrackObjectType.Boss).FromInstance(BossPrefab);

        //Services
        Container.BindInterfacesAndSelfTo<BonusCalculator>().AsSingle();
        Container.BindInterfacesAndSelfTo<TrackObjectFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnitFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<CanvasBindingService>().AsSingle();

        Container.Bind<MenuController>().FromInstance(Menu.GetComponent<MenuController>()).AsSingle().NonLazy();
    }
}