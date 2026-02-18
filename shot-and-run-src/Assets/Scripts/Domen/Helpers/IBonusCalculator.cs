using Assets.Scripts.Domen.Enums;

namespace Assets.Scripts.Domen.Helpers
{
    public interface IBonusCalculator
    {
        public int CalculateNewValue(int currentValue, BonusSign bonusSign, int bonusValue);
    }
}
