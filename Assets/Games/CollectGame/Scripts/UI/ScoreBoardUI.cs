using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame.UI
{
    public class ScoreBoardUI : MonoBehaviour
    {
        [SerializeField] private RewardContainer rewardContainer;
        [SerializeField] private Image rewardProgressImage;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject scoreObject;
        
        
        private Reward? m_NextReward;
        private IHaveScore m_Score;
        private float m_RewardProgress;
        

        private void Awake()
        {
            m_Score = scoreObject.GetComponent<IHaveScore>();

            m_Score.OnScoreChanged += OnScoreChanged;

            RewarderUI.OnContinue += OnRewarderContinue;
        }

        private void OnDestroy()
        {
            m_Score.OnScoreChanged -= OnScoreChanged;
            
            RewarderUI.OnContinue -= OnRewarderContinue;
        }

        private void Update()
        {
            SmoothLerpRewardProgress();
        }


        private void OnScoreChanged(int value)
        {
            var reward = GetRewardByScore(value);
            var nextReward = GetNextRewardByScore(value);
            var score = reward.GetValueOrDefault().score;
            var nextScore = nextReward.GetValueOrDefault().score;
            var isAchieveNextReward = reward.HasValue && reward == m_NextReward;
            var rewardProgress = isAchieveNextReward ? 1f : Mathf.InverseLerp(score, nextScore, value);

            scoreText.text = $"Score: {value}";
            
            m_NextReward = nextReward;
            SetRewardProgress(rewardProgress);
        }

        private void OnRewarderContinue()
        {
            SetRewardProgress(0f);
        }


        private void SmoothLerpRewardProgress()
        {
            var t = Time.unscaledDeltaTime * 25f;
            rewardProgressImage.fillAmount = Mathf.Lerp(rewardProgressImage.fillAmount, m_RewardProgress, t);
        }
        
        private void SetRewardProgress(float value)
        {
            m_RewardProgress = value;
        }

        private Reward? GetRewardByScore(int score)
        {
            return rewardContainer.GetRewardByScore(score);
        }
        
        private Reward? GetNextRewardByScore(int score)
        {
            return rewardContainer.GetNextRewardByScore(score);
        }



        
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (scoreObject && !scoreObject.TryGetComponent(out IHaveScore _))
            {
                scoreObject = null;
                UnityEditor.Undo.RecordObject(this, $"Validate {name} values");
                Debug.LogWarning($"{this} doesn't implement {nameof(IHaveScore)}!");
            }
        }

#endif
    }
}