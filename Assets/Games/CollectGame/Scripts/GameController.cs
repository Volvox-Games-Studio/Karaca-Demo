using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Games.CollectGame
{
    public static class GameController 
    {
        public static event Action<bool> OnGameOver;

        
   

    #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern int ReturnScore(int x);
    #endif
        
        public static void RaiseOnGameOver(bool isWin)
        {
            OnGameOver?.Invoke(isWin);

            
        #if UNITY_WEBGL && !UNITY_EDITOR
            if (isWin)
            {
                ReturnScore(1);
            }
            else
            {
                ReturnScore(0);   
            }
#endif
        }
    }
}