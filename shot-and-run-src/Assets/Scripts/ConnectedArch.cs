using System.Collections.Generic;
using UnityEngine;

public class ConnectedArch : MonoBehaviour
{
    public List<BonusGain> ArchBonuses = new List<BonusGain>();

    public void InactiveAllConnectedBonuses()
    {
        foreach (var bonus in ArchBonuses)
        {
            bonus.BonusIsAvailable = false;
        }
    }
}
