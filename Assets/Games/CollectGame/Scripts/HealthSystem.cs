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
                healthImages[2].color = Color.black;
            }
            else if (healt == 1)
            {
                healthImages[1].color = Color.black;
            }
            else if (healt == 0)
            {
                healthImages[0].color = Color.black;
                GameController.RaiseOnGameOver(false);
            }
        }
    }
}