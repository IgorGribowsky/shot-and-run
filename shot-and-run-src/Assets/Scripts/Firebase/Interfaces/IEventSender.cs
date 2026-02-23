namespace Assets.Scripts.Firebase.Interfaces
{
    public interface IEventSender
    {
        public void SendEvent(IFirebaseEvent firebaseEvent);
    }
}
