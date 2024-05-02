using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button m_Button;

        public static event Action OnGameRestart;
        
        private void Start()
        {
            m_Button.onClick.AddListener(ReloadScene);
        }

        private void ReloadScene()
        {
            OnGameRestart?.Invoke();
            SceneManager.LoadScene(0);
        }
    }
}