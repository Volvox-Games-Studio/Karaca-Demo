using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioManager;
        [SerializeField] private Button _closedImage;
        [SerializeField] private Button _openedImage;

        private bool isOpen = true;

        private void Start()
        {
            _closedImage.onClick.AddListener(ChangeSound);
            _openedImage.onClick.AddListener(ChangeSound);
            
            _closedImage.gameObject.SetActive(false);
            
            RestartButton.OnGameRestart+= GameOverUIOnOnGameRestart;
            
            foreach (var audio in _audioManager)
            {
                audio.mute = false;
            }
        }

        private void OnDestroy()
        {
            RestartButton.OnGameRestart -= GameOverUIOnOnGameRestart;
        }

        private void GameOverUIOnOnGameRestart()
        {
            foreach (var audio in _audioManager)
            {
                audio.mute = false;
            }
        }

        private void ChangeSound()
        {
            isOpen = !isOpen;

            foreach (var audio in _audioManager)
            {
                if (isOpen)
                {
                    audio.mute = false;
                }
                else
                {
                    audio.mute = true;
                }
            }
            
            _closedImage.gameObject.SetActive(!isOpen);
            _openedImage.gameObject.SetActive(isOpen);
        }
    }
}