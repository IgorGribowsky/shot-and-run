public class NextLevelAction : ButtonActionBase
{
    public override void Act()
    {
        LevelManager.SetNextLevel();
    }
}
