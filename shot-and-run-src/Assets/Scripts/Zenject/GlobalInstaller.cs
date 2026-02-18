using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public GameObject LevelManagerPrefab;

    public override void InstallBindings()
    {
        var go = Container.InstantiatePrefab(LevelManagerPrefab);
        Container.Bind<LevelManager>().FromInstance(go.GetComponent<LevelManager>()).AsSingle().NonLazy();
    }
}