using System;
using System.Collections.Generic;
using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Firebase.Interfaces;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Assets.Scripts.Firebase
{
    public class RemoteConfigStore : IRemoteConfigStore
    {
        public float DifficultyModifier { get; set; } = 1.0f;

        public void InitializeRemoteConfig()
        {
            var defaults = new Dictionary<string, object>
            {
                { FirebaseConstants.RemoteConfig.DifficultyMultiplier, 1.0f }
            };

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(task => {
                    FetchData();
                });
        }

        private void FetchData()
        {
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(FirebaseConstants.RemoteConfigCacheExpiration).ContinueWithOnMainThread(task => {
                if (task.IsCompletedSuccessfully)
                {
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                    DifficultyModifier = (float)FirebaseRemoteConfig.DefaultInstance.GetValue(FirebaseConstants.RemoteConfig.DifficultyMultiplier).DoubleValue;
                }
            });
        }
    }
}
