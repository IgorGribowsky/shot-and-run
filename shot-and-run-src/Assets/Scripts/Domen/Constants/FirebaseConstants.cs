
using System;

namespace Assets.Scripts.Domen.Constants
{
    public static class FirebaseConstants
    {
#if UNITY_EDITOR
        public static readonly TimeSpan RemoteConfigCacheExpiration = TimeSpan.FromMinutes(1);
#else
        public static readonly TimeSpan RemoteConfigCacheExpiration = TimeSpan.FromHours(1);
#endif

        public static class AnalyticsEvents
        {
            public const string LevelCompleted = "level_completed";
            public const string PlayerLost = "player_lost";
        }

        public static class AnalyticsParams
        {
            public const string Level = "level_id";
            public const string Score = "score_value";
            public const string Wave = "wave_id";
        }

        public static class RemoteConfig
        {
            public const string DifficultyMultiplier = "difficulty_multiplier";
        }
    }
}
