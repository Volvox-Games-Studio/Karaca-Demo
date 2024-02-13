using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class TargetItemUI : MonoBehaviour
    {
        [SerializeField] private Image[] fillImages;
        [SerializeField] private int _id;
        [SerializeField] private int _maxCollectCount;
        
        private int _collectedCount;
        
        
        private void Start()
        {
            ItemSpawner.OnTargetItemCollected += OnTargetItemCollected;
        }

        private void OnDestroy()
        {
            ItemSpawner.OnTargetItemCollected -= OnTargetItemCollected;
        }

        private void OnTargetItemCollected(int obj)
        {
            Debug.Log("GELEN ID: " + obj + " BENİM ID " + _id);
            if (obj == _id)
            {
                Debug.Log("SETLEDİM");
                SetCollectedCount(_collectedCount+1);
            }
        }

        private void SetCollectedCount(int count)
        {
            _collectedCount = count;
            
            DoFill();
        }

        private void DoFill()
        {
            foreach (var image in fillImages)
            {
                image.fillAmount = (float)_collectedCount / _maxCollectCount;
            }
        }
    }
}