using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button m_Button;

        private void Start()
        {
            m_Button.onClick.AddListener(ReloadScene);
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}