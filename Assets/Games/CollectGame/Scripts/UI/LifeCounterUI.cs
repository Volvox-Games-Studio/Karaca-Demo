using Helpers;
using UnityEngine;

namespace Games.CollectGame.UI
{
    public class LifeCounterUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] lives;        
        
        private void Awake()
        {
            GamePrefs.OnLifeCountChanged += OnLifeCountChanged;
        }

        private void OnDestroy()
        {
            GamePrefs.OnLifeCountChanged -= OnLifeCountChanged;
        }

        
        private void OnLifeCountChanged(int value)
        {
            SetLifeCount(value);
        }


        private void SetLifeCount(int value)
        {
            for (var i = 0; i < lives.Length; i++)
            {
                var isActive = i < value;
                lives[i].SetActive(isActive);
            }
        }
    }
}