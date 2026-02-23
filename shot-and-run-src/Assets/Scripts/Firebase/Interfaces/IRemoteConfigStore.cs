namespace Assets.Scripts.Firebase.Interfaces
{
    public interface IRemoteConfigStore
    {
        public float DifficultyModifier { get; set; }

        public void InitializeRemoteConfig();
    }
}
