using Assets.Scripts.Domen.Enums;
using UnityEngine;

namespace Assets.Scripts.Domen.Factories
{
    public interface ITrackObjectFactory
    {
        public GameObject CreateByType(TrackObjectType type, int tracksCount, int trackNum);
    }
}
