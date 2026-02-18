using Assets.Scripts.Domen.Enums;
using UnityEngine;

namespace Assets.Scripts.Domen.Helpers
{
    public class BonusCalculator : IBonusCalculator
    {
        public int CalculateNewValue(int currentValue, BonusSign bonusSign, int bonusValue)
        {
            switch (bonusSign)
            {
                case BonusSign.Plus:
                    return currentValue + bonusValue;

                case BonusSign.Minus:
                    return Mathf.Max(1, currentValue - bonusValue);

                case BonusSign.Divide:
                    return Mathf.Max(1, currentValue / bonusValue);

                case BonusSign.Multiple:
                    return currentValue * bonusValue;

                default:
                    return currentValue;
            }
        }
    }
}
