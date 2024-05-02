using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class TargetItemUI : MonoBehaviour
    {
        [SerializeField] private Image[] fillImages;
        [SerializeField] private int _id;
        [SerializeField] private int _maxCollectCount;
        [SerializeField] private GameObject _completedImages;
        [SerializeField] private GameObject _lockedImages;
        [SerializeField] private TMP_Text _collectedText;
        [SerializeField] private int _myPhase;
        
        
        private int _collectedCount;
        
        
        private void Start()
        {
            ItemSpawner.OnItemCollected += OnItemCollected;
            ItemSpawner.OnPhaseCompleted += OnPhaseCompleted;
        }
        
        private void OnDestroy()
        {
            ItemSpawner.OnItemCollected -= OnItemCollected;
            ItemSpawner.OnPhaseCompleted -= OnPhaseCompleted;
        }

        private void OnPhaseCompleted(int phase)
        {
            if (phase + 1 == _myPhase)
            {
                CloseLockedImage();
            }
        }

        private void CloseLockedImage()
        {
            _lockedImages.SetActive(false);
        }
        
        private void OnItemCollected(int obj)
        {
            if (obj == _id)
            {
                SetCollectedCount(_collectedCount+1);
                var count = _collectedCount;
                if (count > 20)
                {
                    count = 20;
                }

                _collectedText.text = count + "/20";
            }
        }

        private void SetCollectedCount(int count)
        {
            _collectedCount = count;
            
            DoFill();

            if (_collectedCount == 20)
            {
                ShowCompleted();
            }
        }

        private void DoFill()
        {
            foreach (var image in fillImages)
            {
                image.fillAmount = (float)_collectedCount / _maxCollectCount;
            }
        }

        private void ShowCompleted()
        {
            _completedImages.SetActive(true);
        }

        private void HideCompleted()
        {
            _completedImages.SetActive(false);
        }
        
        private void SetColectedCount(int count)
        {
            _collectedCount = count;
            
            DoFill();

            if (_collectedCount == 20)
            {
                ShowCompleted();
            }
        }
    }
}