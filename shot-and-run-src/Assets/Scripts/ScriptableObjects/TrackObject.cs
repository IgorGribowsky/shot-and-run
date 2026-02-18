using Assets.Scripts.Domen.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackObject", menuName = "Scriptable Objects/TrackObject")]
public class TrackObject : ScriptableObject
{
    public TrackObjectType Type = TrackObjectType.Arch;

    public BonusSign BonusSign = BonusSign.Plus;

    public int Value = 2;
}
