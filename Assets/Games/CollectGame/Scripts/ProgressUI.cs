﻿using System;
using UnityEngine;

namespace Games.CollectGame
{
    public class ProgressUI : MonoBehaviour
    {
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private RectTransform _progressObj;
        [SerializeField] private float maxValue;
        
        private int[] itemCounts = new int[12];
        
        private void Start()
        {
            ItemSpawner.OnItemCollected += OnItemCollected;
        }

        private void OnDestroy()
        {
            ItemSpawner.OnItemCollected -= OnItemCollected;
        }

        private void OnItemCollected(int obj)
        {
            for (var i = 0; i < itemSpawner.GetCollectedItemsCount().Length; i++)
            {
                if (itemSpawner.GetCollectedItemsCount()[i] > 10)
                {
                    itemCounts[i] = 10;
                }
                else
                {
                    itemCounts[i] = itemSpawner.GetCollectedItemsCount()[i];
                }
            }
            
            AdjustVisual(CalculateRatio(itemCounts));
        }

        private void AdjustVisual(float ratio)
        {
            float newX = Mathf.Lerp(0, maxValue, ratio);
            Vector3 newPos = _progressObj.anchoredPosition;
            newPos.x = newX;
            _progressObj.anchoredPosition = newPos;
        }

        private float CalculateRatio(int[] counts)
        {
            var total = 0;

            foreach (var count in counts)
            {
                total += count;
            }

            return (float)total / 120;
        }
    }
}