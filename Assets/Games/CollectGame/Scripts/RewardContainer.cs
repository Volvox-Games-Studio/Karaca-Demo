using System;
using System.Linq;
using UnityEngine;

namespace Games.CollectGame
{
    [CreateAssetMenu]
    public class RewardContainer : ScriptableObject
    {
        [SerializeField] private Reward[] rewards;


        public Reward? GetRewardByScore(int score)
        {
            var bestScore = int.MinValue;
            Reward? bestReward = null;

            foreach (var reward in rewards)
            {
                if (reward.score > score) continue;
                
                if (reward.score < bestScore) continue;

                bestScore = reward.score;
                bestReward = reward;
            }

            return bestReward;
        }

        public Reward? GetNextRewardByScore(int score)
        {
            foreach (var reward in rewards)
            {
                if (reward.score > score) return reward;
            }

            return null;
        }

        public bool CanObtainBestRewardByScore(int score)
        {
            var bestScore = rewards
                .Select(reward => reward.score)
                .Max();

            return score >= bestScore;
        }


#if UNITY_EDITOR

        private void OnValidate()
        {
            if (rewards.Length <= 0) return;
            
            var bestScore = int.MinValue;
            var bestIndex = -1;
            
            for (var i = 0; i < rewards.Length; i++)
            {
                if (rewards[i].score < bestScore) continue;

                bestScore = rewards[i].score;
                bestIndex = i;
            }

            for (var i = 0; i < rewards.Length; i++)
            {
                var reward = rewards[i];
                reward.isLast = i == bestIndex;
                rewards[i] = reward;
            }
        }

#endif
    }


    [Serializable]
    public struct Reward
    {
        public string name;
        public int score;
        public bool isLast;


        public override string ToString()
        {
            return $"[{name}] for {score} score";
        }
        
        public bool Equals(Reward other)
        {
            return name == other.name && score == other.score;
        }

        public override bool Equals(object obj)
        {
            return obj is Reward other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, score);
        }

        public static bool operator ==(Reward lhs, Reward rhs)
        {
            return lhs.name == rhs.name && lhs.score == rhs.score;
        }

        public static bool operator !=(Reward lhs, Reward rhs)
        {
            return !(lhs == rhs);
        }
    }
}