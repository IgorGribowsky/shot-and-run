using Assets.Scripts.Domen.Constants;
using Assets.Scripts.Firebase.Interfaces;
using Firebase.Analytics;

namespace Assets.Scripts.Firebase.Events
{
    public struct LevelCompletedEvent : IFirebaseEvent
    {
        private readonly int _level;

        private readonly int _score;

        public LevelCompletedEvent(int level, int score)
        {
            _level = level;
            _score = score;
        }

        public void Send()
        {
            FirebaseAnalytics.LogEvent(FirebaseConstants.AnalyticsEvents.LevelCompleted,
                new Parameter(FirebaseConstants.AnalyticsParams.Level, _level),
                new Parameter(FirebaseConstants.AnalyticsParams.Score, _score));
        }
    }
}
