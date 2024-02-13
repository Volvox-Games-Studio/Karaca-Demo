using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame
{
    public class TargetItemUI : MonoBehaviour
    {
        [SerializeField] private Image[] fillImages;
        [SerializeField] private int _id;
        [SerializeField] private int _maxCollectCount;
        [SerializeField] private Transform _onCompleteMoveTransform;
        
        
        private int _collectedCount;
        
        
        private void Start()
        {
            ItemSpawner.OnTargetItemCollected += OnTargetItemCollected;
            ItemSpawner.OnTargetCompleted += OnTargetCompleted;
        }

        private void OnTargetCompleted(int obj)
        {
            if (obj != _id)
            {
                return;
            }

            var startPos = transform.position;
            var startScale = transform.localScale;
            transform.DOMove(_onCompleteMoveTransform.position, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                transform.DOMove(startPos, 1f);
                transform.DOScale(startScale, 1f);
            });
            transform.DOScale(Vector3.one * 2, 1f);
        }

        private void OnDestroy()
        {
            ItemSpawner.OnTargetItemCollected -= OnTargetItemCollected;
            ItemSpawner.OnTargetCompleted -= OnTargetCompleted;
        }

        private void OnTargetItemCollected(int obj)
        {
            if (obj == _id)
            {
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