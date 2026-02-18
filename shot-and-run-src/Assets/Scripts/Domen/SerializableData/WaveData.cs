using System;
using System.Collections.Generic;

namespace Assets.Scripts.Domen.SerializableData
{
    [Serializable]
    public class WaveData
    {
        public float Hp = 10;
        public List<TrackObject> TrackObjects = new List<TrackObject>();
    }
}
