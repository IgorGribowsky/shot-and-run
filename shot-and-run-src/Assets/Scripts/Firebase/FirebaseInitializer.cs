using Assets.Scripts.Firebase;
using Assets.Scripts.Firebase.Interfaces;
using Firebase;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FirebaseInitializer : MonoBehaviour
{
    public bool FirebaseInitialized { get; set; } = false;

    [Inject] private IRemoteConfigStore _remoteConfig;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
    }

    private void OnDependencyStatusReceived(Task<DependencyStatus> task)
    {
        try
        {
            if (!task.IsCompletedSuccessfully)
            {
                throw new Exception($"Could not resolve all Firebase dependencies: {task.Exception}");
            }

            var status = task.Result;

            if (status != DependencyStatus.Available)
            {
                throw new Exception($"Could not resolve all Firebase dependencies: {status}");
            }

            FirebaseInitialized = true;
            Debug.Log("Firebase started!");
            _remoteConfig.InitializeRemoteConfig();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
