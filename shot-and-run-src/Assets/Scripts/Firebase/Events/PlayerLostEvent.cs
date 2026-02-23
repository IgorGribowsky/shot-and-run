using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Firebase.Interfaces;
using Firebase.Analytics;

namespace Assets.Scripts.Firebase.Events
{
    public struct PlayerLostEvent : IFirebaseEvent
    {
        private readonly int _level;

        private readonly int _waveid;

        public PlayerLostEvent(int level, int waveid)
        {
            _level = level;
            _waveid = waveid;
        }

        public void Send()
        {
            FirebaseAnalytics.LogEvent(FirebaseConstants.AnalyticsEvents.PlayerLost,
                new Parameter(FirebaseConstants.AnalyticsParams.Level, _level),
                new Parameter(FirebaseConstants.AnalyticsParams.Wave, _waveid));
        }
    }
}
