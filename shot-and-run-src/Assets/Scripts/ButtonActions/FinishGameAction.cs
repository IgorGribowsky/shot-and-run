public class FinishGameAction : ButtonActionBase
{
    public override void Act()
    {
        LevelManager.CurrentLevel = -1;
        LevelManager.LoadCurrentLevel();
    }
}
