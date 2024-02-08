using System;
using UnityEngine;

namespace Helpers
{
    public static class GameTime
    {
        public static event Action OnPaused;
        public static event Action OnResumed;


        private static float ms_OldTimeScale = Time.timeScale;
        private static bool ms_IsPaused;
        
        
        public static void Pause()
        {
            ms_OldTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            ms_IsPaused = true;
            OnPaused?.Invoke();
        }

        public static void Resume()
        {
            Time.timeScale = ms_OldTimeScale;
            ms_IsPaused = false;
            OnResumed?.Invoke();
        }

        public static bool IsPaused()
        {
            return ms_IsPaused;
        }
    }
}