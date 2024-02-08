using System;
using UnityEngine;

namespace Helpers
{
    public static class GamePrefs
    {
        private const string LifeCountKey = "Life_Count";


        public static event Action<int> OnLifeCountChanged;
        

        public static int GetLifeCount()
        {
            return PlayerPrefs.GetInt(LifeCountKey, 0);
        }
        
        public static void SetLifeCount(int value)
        {
            PlayerPrefs.SetInt(LifeCountKey, value);
            OnLifeCountChanged?.Invoke(value);
        }

        public static void RemoveLifeCount(int value)
        {
            var lifeCount = GetLifeCount();
            lifeCount = Mathf.Max(0, lifeCount - value);
            SetLifeCount(lifeCount);
        }
    }
}