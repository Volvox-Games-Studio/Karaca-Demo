using System;
using TMPro;
using UnityEngine;

namespace Games.CollectGame
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private TMP_Text _text;
        
        
        private void Start()
        {
            GameController.OnGameOver += OnGameOver;
        }

        private void OnDestroy()
        {
            GameController.OnGameOver -= OnGameOver;
        }

        private void OnGameOver(bool isWin)
        {
            _gameOverUI.SetActive(true);
            
            if (isWin)
            {
                _text.text = "KAZANDINIZ!";
            }
            else
            {
                _text.text = "KAYBETTİNİZ!";
            }
        }
    }
}