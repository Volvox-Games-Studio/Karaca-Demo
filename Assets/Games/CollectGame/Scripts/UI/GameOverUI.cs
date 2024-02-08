using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Games.CollectGame.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private RewardContainer rewardContainer;
        [SerializeField] private Basket basket;
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text lostRewardField;
        [SerializeField] private Button tryAgainButton;


        private void Awake()
        {
            tryAgainButton.onClick.AddListener(OnClickTryAgainButton);
            
            basket.OnDead += OnDead;
        }

        private void OnDestroy()
        {
            basket.OnDead -= OnDead;
        }


        private void OnClickTryAgainButton()
        {
            SceneManager.LoadScene(0);
            GameTime.Resume();
        }
        
        private void OnDead()
        {
            LazyCoroutines.WaitForSeconds(1.5f, () =>
            {
                OpenPanel();
                GameTime.Pause();
            });
        }


        private void OpenPanel()
        {
            var reward = GetRewardByScore(basket.GetScore());
            
            SetLostReward(reward);
            panel.SetActive(true);
        }

        private Reward? GetRewardByScore(int score)
        {
            return rewardContainer.GetRewardByScore(score);
        }

        private void SetLostReward(Reward? reward)
        {
            lostRewardField.text = reward.HasValue
                ? $"{reward.Value.name} kaybettiniz!"
                : "Hiçbir şey kazanamadınız!";
        }
    }
}