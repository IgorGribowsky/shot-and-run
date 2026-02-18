using Assets.Scripts.Domen.SerializableData;
using Scellecs.Morpeh;
using System.Collections.Generic;

public struct WavesInfo : IComponent
{
    public List<WaveData> Waves;

    public int CurrentWave;
}
