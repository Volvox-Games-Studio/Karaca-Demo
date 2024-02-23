using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private GameObject _closedImage;

        private Button m_button;


        private void Start()
        {
            m_button = GetComponent<Button>();
            
            m_button.onClick.AddListener(ChangeSound);
        }

        private void ChangeSound()
        {
            _audioManager.SetActive(!_audioManager.activeSelf);
            _closedImage.SetActive(!_closedImage.activeSelf);
        }
    }
}