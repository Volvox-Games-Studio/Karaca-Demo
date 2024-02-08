using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame.UI
{
    public class RewarderUI : MonoBehaviour
    {
        [SerializeField] private Rewarder rewarder;
        [SerializeField] private GameObject offerPanel;
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private TMP_Text offerField;
        [SerializeField] private TMP_Text rewardField;
        [SerializeField] private Button takeItButton;
        [SerializeField] private Button continueButton;


        public static event Action OnContinue;
        

        private Reward m_Reward;
        

        private void Awake()
        {
            takeItButton.onClick.AddListener(OnClickTakeItButton);
            continueButton.onClick.AddListener(OnClickContinueButton);
            
            rewarder.OnRewardChanged += OnRewardChanged;
        }

        private void OnDestroy()
        {
            takeItButton.onClick.RemoveListener(OnClickTakeItButton);
            continueButton.onClick.RemoveListener(OnClickContinueButton);
            
            rewarder.OnRewardChanged -= OnRewardChanged;
        }


        private void OnClickTakeItButton()
        {
            CloseOfferPanel();
            ClaimReward(m_Reward);
        }

        private void OnClickContinueButton()
        {
            CloseOfferPanel();
            GameTime.Resume();
            OnContinue?.Invoke();
        }
        
        private void OnRewardChanged(Reward? reward)
        {
            if (!reward.HasValue) return;
            
            SetReward(reward.Value);
            OpenOfferPanel();
            GameTime.Pause();
        }


        private void ClaimReward(Reward reward)
        {
            OpenRewardPanel();
        }
        
        private void OpenOfferPanel()
        {
            offerPanel.SetActive(true);
        }

        private void CloseOfferPanel()
        {
            offerPanel.SetActive(false);
        }

        private void OpenRewardPanel()
        {
            rewardPanel.SetActive(true);
        }

        private void SetReward(Reward reward)
        {
            rewardField.text = $"{reward.name} aldınız!";

            offerField.text = reward.isLast
                ? $"{reward.name} kazandınız!"
                : $"{reward.name} kazandınız!\nTamam mı devam mı?";

            continueButton.gameObject.SetActive(!reward.isLast);;
        }
    }
}