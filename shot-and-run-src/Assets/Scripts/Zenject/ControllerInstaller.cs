using Assets.Scripts.Domen.Controller;
using Assets.Scripts.ECS.Systems;
using UnityEngine;
using Zenject;

public class ControllerInstaller : MonoInstaller
{
    public Joystick AndroidJoystick;
    public bool UseAndroidController;

    public override void InstallBindings()
    {
        if (UseAndroidController)
        {
            UseAndroid();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            UseAndroid();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            UseWindows();
        }

        Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
    }

    private void UseAndroid()
    {
        Container.Bind<Joystick>().FromInstance(AndroidJoystick).AsSingle()
            .OnInstantiated<Joystick>((ctx, obj) => {
                obj.gameObject.SetActive(true);
            });

        Container.BindInterfacesAndSelfTo<AndroidController>().AsSingle();
    }

    private void UseWindows()
    {
        Container.BindInterfacesAndSelfTo<WindowsController>().AsSingle();
    }
}