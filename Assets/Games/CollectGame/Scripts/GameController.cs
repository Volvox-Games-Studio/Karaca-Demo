using System;
using UnityEngine;

namespace Games.CollectGame
{
    public static class GameController 
    {
        public static event Action<bool> OnGameOver;

        public static void RaiseOnGameOver(bool isWin)
        {
            OnGameOver?.Invoke(isWin);
        }
    }
}