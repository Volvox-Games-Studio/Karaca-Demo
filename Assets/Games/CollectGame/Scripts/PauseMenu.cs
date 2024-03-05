using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Transform _mainTransform;
        [SerializeField] private Button _continueButton;

        private float time = 0;
        
        private void Awake()
        {
            _pauseButton.interactable = false;
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(Stop);
            _continueButton.onClick.AddListener(Continue);
        }

        private void Update()
        {
            time += Time.deltaTime;

            if (time > 5)
            {
                _pauseButton.interactable = true;
            }
        }

        private void Continue()
        {
            _mainTransform.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void Stop()
        {
            _mainTransform.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}