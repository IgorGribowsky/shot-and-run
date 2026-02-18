using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Domen.Enums;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Domen.Factories
{
    public class TrackObjectFactory : ITrackObjectFactory
    {
        [Inject]
        private DiContainer _container;

        [Inject(Id = TrackObjectType.Barrel)]
        private GameObject _barrelPrefab;

        [Inject(Id = TrackObjectType.Arch)]
        private GameObject _archPrefab;

        [Inject(Id = TrackObjectType.Boss)]
        private GameObject _bossPrefab;

        public GameObject CreateByType(TrackObjectType type, int tracksCount, int trackNum)
        {
            GameObject prefab = type switch
            {
                TrackObjectType.Barrel => _barrelPrefab,
                TrackObjectType.Arch => _archPrefab,
                TrackObjectType.Boss => _bossPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Type {type} not valid")
            };

            float xLeftShift = -2.5f * (tracksCount - 1);
            float xPos;
            float yPos;

            if (type == TrackObjectType.Boss)
            {
                xPos = xLeftShift + (tracksCount / 2.0f - 0.5f) * GameConstants.XTrackWidth;
                yPos = _bossPrefab.transform.position.y;
            }
            else if (type == TrackObjectType.Barrel)
            {
                xPos = xLeftShift + trackNum * GameConstants.XTrackWidth - GameConstants.BarrelHalfWidth;
                yPos = GameConstants.YSpawnPosition;
            }
            else
            {
                xPos = xLeftShift + trackNum * GameConstants.XTrackWidth;
                yPos = GameConstants.YSpawnPosition;
            }

            return _container.InstantiatePrefab(prefab, new Vector3(xPos, yPos, GameConstants.ZSpawnPosition), prefab.transform.rotation, null);
        }
    }
}
