using System.Collections.Generic;
using UnityEngine;

public class ConnectedArch : MonoBehaviour
{
    public List<Bonus> ArchBonuses = new List<Bonus>();

    public void InactiveAllConnectedBonuses()
    {
        foreach (var bonus in ArchBonuses)
        {
            bonus.BonusIsAvailable = false;
        }
    }
}
