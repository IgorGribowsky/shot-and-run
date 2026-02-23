using Assets.Scripts.Firebase.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Firebase
{
    public class EventSender : IEventSender
    {
        [Inject] private FirebaseInitializer _firebaseInitializer;

        public void SendEvent(IFirebaseEvent firebaseEvent) 
        {
            if (_firebaseInitializer.FirebaseInitialized)
            {
                firebaseEvent.Send();

                Debug.Log($"Event {firebaseEvent.GetType()} sent!");
            }
        }
    }
}
