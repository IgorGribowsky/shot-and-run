using UnityEngine;

namespace Assets.Scripts.Domen.Constants
{
    public static class CanvasConstants
    {
        public const string Tag = "CanvasText";

        public static Vector3 ArmyCountCanvasOffset = new Vector3(0, 2.5f, 0);
        public static Vector3 BonusCanvasOffsetArch = new Vector3(0, 0.5f, -0.75f);
        public static Vector3 BonusCanvasOffsetForBarrel = new Vector3(1.5f, 0.5f, -1.25f);
        public static Vector3 HealthCanvasOffsetForBarrel = new Vector3(1.5f, 2.25f, 0);
        public static Vector3 HealthCanvasOffsetForBoss = new Vector3(0, 6, -6);
        public static Vector3 HealthCanvasOffsetForArch = new Vector3(0, 2.25f, 0);
    }
}
