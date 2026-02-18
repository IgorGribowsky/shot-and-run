using System;
using System.Collections.Generic;

namespace Assets.Scripts.Domen.SerializableData
{
    [Serializable]
    public class LevelData
    {
        public float WaveRate = 10f;

        public float ObstacleSpeed = 5;

        public int TrackCount = 2;

        public List<WaveData> Waves = new List<WaveData>();
    }
}
