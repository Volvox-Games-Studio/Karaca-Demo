using System;
using Helpers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class GameStartUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject hand;
        
            
        private void Awake()
        {
            GameTime.Stop();
        }

        private void Start()
        {
            GameTime.Stop();
            
            startButton.onClick.AddListener(() =>
            {
                GameTime.Continue();
                gameObject.SetActive(false);
                hand.SetActive(true);
            });
        }
        
        
    }
}