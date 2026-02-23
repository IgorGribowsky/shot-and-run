using Assets.Scripts.Firebase;
using UnityEngine;
using Zenject;

public class FirebaseInstaller : MonoInstaller
{
    public GameObject FirebaseInitializerPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<RemoteConfigStore>().AsSingle();

        var go = Container.InstantiatePrefab(FirebaseInitializerPrefab);
        Container.Bind<FirebaseInitializer>().FromInstance(go.GetComponent<FirebaseInitializer>()).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<EventSender>().AsSingle();
    }
}