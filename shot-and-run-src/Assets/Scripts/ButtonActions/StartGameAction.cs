public class StartGameAction : ButtonActionBase
{
    public override void Act()
    {
        LevelManager.SetFirstLevel();
    }
}
