using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CollectGame
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Item[] itemPrefabs;
        [SerializeField] private Item ice;
        [SerializeField] private int _targetCountForNext;

        public static Action<int> OnTargetItemCollected;
        
        private Camera m_MainCamera;
        private int currentTarget = 0;
        private int m_iceRation = 10;

        
        private void Awake()
        {
            m_MainCamera = Camera.main;
        }
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                var prefab = GetRandomPrefab();
                SpawnItem(prefab);
            }
        }
        
        private Item GetRandomPrefab()
        {
            var random = Random.Range(0, 100);

            if (random < m_iceRation)
            {
                return ice;
            }

            if (currentTarget == 5)
            {
                var r2 = Random.Range(0, 2);
                if (r2 == 0)
                {
                    return itemPrefabs[5];
                }

                return itemPrefabs[0];
            }
            
            return itemPrefabs[Random.Range(currentTarget, currentTarget+2)];
        }
        
        private void SpawnItem(Item prefab)
        {
            var spawnPoint = GetSpawnPoint(prefab);
            var item = Instantiate(prefab, transform);

            item.OnCollideWithBasket += OnItemCollideWithBasket;
            item.OnItemDestroy += OnItemDestroy;
            
            item.SetPosition(spawnPoint);
        }

        private void OnItemDestroy(Item obj)
        {
            obj.OnCollideWithBasket -= OnItemCollideWithBasket;
            obj.OnItemDestroy -= OnItemDestroy;
        }

        private void OnItemCollideWithBasket(Item item)
        {
            Debug.Log("ITEM COLLIDED " + item.GetId());
            Debug.Log("CURRENT TARGET " + itemPrefabs[currentTarget].GetId());
            
            if (item.GetId() == itemPrefabs[currentTarget].GetId())
            {
                Debug.Log("INVOKELADIM");
                OnTargetItemCollected?.Invoke(item.GetId());
            }
        }

        private Vector3 GetSpawnPoint(Item prefab)
        {
            var screenWidth = GetScreenWidth();
            var xMax = screenWidth * 0.5f;
            var xMin = screenWidth * -0.5f;
            var halfSizeX = prefab.GetBounds().size.x;

            xMax -= halfSizeX;
            xMin += halfSizeX;

            var x = Random.Range(xMin + 0.25f, xMax - 0.5f);
            var y = transform.position.y;

            return new Vector3(x, y);
        }

        private float GetScreenWidth()
        {
            return m_MainCamera.GetScreenWorldWidth();
        }
    }
}