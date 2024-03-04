using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private Button _closedImage;
        [SerializeField] private Button _openedImage;

        private bool isOpen = true;

        private void Start()
        {
            _closedImage.onClick.AddListener(ChangeSound);
            _openedImage.onClick.AddListener(ChangeSound);
            
            _closedImage.gameObject.SetActive(false);
        }

        private void ChangeSound()
        {
            isOpen = !isOpen;
            
            _audioManager.SetActive(isOpen);
            _closedImage.gameObject.SetActive(!isOpen);
            _openedImage.gameObject.SetActive(isOpen);
        }
    }
}