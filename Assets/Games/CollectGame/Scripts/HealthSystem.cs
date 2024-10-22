using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private Image[] healthImages;
        
        private int m_health = 3;
        
        private void Start()
        {
            Item.OnHitGround += OnItemHitGround;
            ItemSpawner.OnPhaseCompleted += PhaseCompleted;
        }

        private void OnDestroy()
        {
            Item.OnHitGround -= OnItemHitGround;
            ItemSpawner.OnPhaseCompleted -= PhaseCompleted;
        }

        private void PhaseCompleted(int phase)
        {
            if (m_health ==3) return;

            m_health++;
            AdjustImages(m_health);
        }

        private void OnItemHitGround()
        {
            m_health--;
            AdjustImages(m_health);
        }

        private void AdjustImages(int healt)
        {
            if (healt == 2)
            {
                healthImages[2].gameObject.SetActive(true);
                healthImages[2].gameObject.SetActive(true);
                healthImages[2].gameObject.SetActive(false);
            }
            else if (healt == 1)
            {
                healthImages[2].gameObject.SetActive(true);
                healthImages[2].gameObject.SetActive(false);
                healthImages[2].gameObject.SetActive(false);
            }
            else if (healt == 0)
            {
                healthImages[2].gameObject.SetActive(false);
                healthImages[2].gameObject.SetActive(false);
                healthImages[2].gameObject.SetActive(false);
                GameController.RaiseOnGameOver(false);
            }
            else if (healt ==3)
            {
                healthImages[2].gameObject.SetActive(false);
                healthImages[2].gameObject.SetActive(false);
                healthImages[2].gameObject.SetActive(false);
            }
        }
    }
}