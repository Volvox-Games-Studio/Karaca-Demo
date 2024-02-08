using System;
using UnityEngine;

namespace Games.CollectGame
{
    public class Rewarder : MonoBehaviour
    {
        [SerializeField] private RewardContainer rewardContainer;
        [SerializeField] private GameObject scoreObject;


        public Action<Reward?> OnRewardChanged;


        private Reward? m_Reward;
        private IHaveScore m_Score;
        

        private void Awake()
        {
            m_Score = scoreObject.GetComponent<IHaveScore>();
            
            m_Score.OnScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            m_Score.OnScoreChanged -= OnScoreChanged;
        }

        
        private void OnScoreChanged(int score)
        {
            var reward = GetRewardByScore(score);
            SetReward(reward);
        }


        private Reward? GetRewardByScore(int score)
        {
            return rewardContainer.GetRewardByScore(score);
        }

        private bool CanObtainBestRewardByScore(int score)
        {
            return rewardContainer.CanObtainBestRewardByScore(score);
        }

        private void SetReward(Reward? reward)
        {
            if (reward == m_Reward) return;

            m_Reward = reward;
            OnRewardChanged?.Invoke(reward);
        }

        
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (scoreObject && !scoreObject.TryGetComponent(out m_Score))
            {
                UnityEditor.Undo.RecordObject(this, $"Validate {name} values");
                scoreObject = null;
                Debug.LogWarning($"{this} doesn't implement {nameof(IHaveScore)}!");
            }
        }

#endif
    }
}