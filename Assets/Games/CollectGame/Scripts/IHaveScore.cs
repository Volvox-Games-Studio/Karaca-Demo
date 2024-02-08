using System;

namespace Games.CollectGame
{
    public interface IHaveScore
    {
        event Action<int> OnScoreChanged;
    }
}