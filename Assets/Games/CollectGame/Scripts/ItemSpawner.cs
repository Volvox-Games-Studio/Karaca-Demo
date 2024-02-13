using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CollectGame
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Item[] itemPrefabs;
        [SerializeField] private Item ice;

        public static Action<int> OnTargetItemCollected;
        public static Action<int> OnTargetCompleted; 
        
        
        private Camera m_MainCamera;
        private int currentTarget = 0;
        private int collectedTarget;
        private int m_iceRation = 10;
        private float m_waitTimeBetweenItems = 1.25f;
        private bool m_isGameStopped;

        private Coroutine itemRoutine;

        private void Awake()
        {
            m_MainCamera = Camera.main;
            GameTime.OnGameStoped += OnGameStopped;
            GameTime.OnGameContinue += OnGameContinue;
        }

        private void OnDestroy()
        {
            GameTime.OnGameStoped -= OnGameStopped;
            GameTime.OnGameContinue -= OnGameContinue;
        }

        private void OnGameContinue()
        {
            m_isGameStopped = false;
            itemRoutine = StartCoroutine(ItemRoutine());
        }

        private void OnGameStopped()
        {
            m_isGameStopped = true;
            StopCoroutine(itemRoutine);
        }

        private void Start()
        {
            itemRoutine = StartCoroutine(ItemRoutine());
        }

        private IEnumerator ItemRoutine()
        {
            while (!m_isGameStopped)
            {
                yield return new WaitForSeconds(m_waitTimeBetweenItems);
                var prefab = GetRandomPrefab();
                SpawnItem(prefab);
            }
        }

        private void Update()
        {
            if (!m_isGameStopped)
            {
                if (collectedTarget >= 10)
                {
                    GameTime.Stop();
                    StartCoroutine(OnTargetCountArrived());
                }
            }
        }

        private IEnumerator OnTargetCountArrived()
        {
            OnTargetCompleted?.Invoke(currentTarget);
            yield return new WaitForSeconds(3f);
            collectedTarget = 0;
            currentTarget++;
            m_iceRation += 2;
            m_waitTimeBetweenItems -= 0.05f;
            GameTime.Continue();
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
            
            return itemPrefabs[Random.Range(currentTarget, currentTarget+1)];
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
                collectedTarget++;
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