using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class TargetItemUI : MonoBehaviour
    {
        [SerializeField] private Image[] fillImages;
        [SerializeField] private Sprite[] phaseTwoSprites;
        [SerializeField] private Image myImage;
        [SerializeField] private int _id;
        [SerializeField] private int _maxCollectCount;
        [SerializeField] private GameObject _completedImages;
        [SerializeField] private GameObject _passivedImages;
        [SerializeField] private TMP_Text _collectedText;
        
        
        private int _collectedCount;
        
        
        private void Start()
        {
            ItemSpawner.OnItemCollected += OnItemCollected;
            ItemSpawner.OnPhaseCompleted += OnPhaseCompleted;
        }

        private void OnPhaseCompleted()
        {
            StartCoroutine(InnerRoutine());

            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(2f);

                _collectedText.text = "0/10";
                myImage.sprite = phaseTwoSprites[_id];
                SetCollectedCount(0);
                _id += 6;
                HideCompleted();
            }
        }

        private void OnDestroy()
        {
            ItemSpawner.OnItemCollected -= OnItemCollected;
            ItemSpawner.OnPhaseCompleted += OnPhaseCompleted;
        }

        private void OnItemCollected(int obj)
        {
            if (obj == _id)
            {
                SetCollectedCount(_collectedCount+1);
                var count = _collectedCount;
                if (count > 10)
                {
                    count = 10;
                }

                _collectedText.text = count + "/10";
            }
        }

        private void SetCollectedCount(int count)
        {
            _collectedCount = count;
            
            DoFill();

            if (_collectedCount == 10)
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
    }
}