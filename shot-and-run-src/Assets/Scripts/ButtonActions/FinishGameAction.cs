public class FinishGameAction : ButtonActionBase
{
    public override void Act()
    {
        LevelManager.CurrentLevel = 0;
        LevelManager.LoadCurrentLevel();
    }
}
