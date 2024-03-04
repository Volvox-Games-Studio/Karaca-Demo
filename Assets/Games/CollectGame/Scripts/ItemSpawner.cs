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
        [SerializeField] private Item[] phaseOneItems;
        [SerializeField] private Item[] phaseTwoItems;
        [SerializeField] private Item ice;

        public static Action<int> OnItemCollected;
        public static Action OnPhaseCompleted;

        private int[] collectedItemCounts = new int[12];

        
        private int currentPhase = 1;
        private Camera m_MainCamera;
        private int m_iceRation = 10;
        private float m_waitTimeBetweenItems = 1.25f;
        private bool m_isGameStopped;
        private Coroutine itemRoutine;
        

        private void Awake()
        {
            m_MainCamera = Camera.main;
            GameTime.OnGameStoped += OnGameStopped;
            GameTime.OnGameContinue += OnGameContinue;
            GameController.OnGameOver += OnGameOVer;
            itemRoutine = StartCoroutine(ItemRoutine());
        }
        
        private void Start()
        {
            //itemRoutine = StartCoroutine(ItemRoutine());
        }
        
        private void OnDestroy()
        {
            GameTime.OnGameStoped -= OnGameStopped;
            GameTime.OnGameContinue -= OnGameContinue;
            GameController.OnGameOver -= OnGameOVer;
        }
        
        private void Update()
        {
            if (!m_isGameStopped)
            {
                if (currentPhase == 1 && collectedItemCounts[0] >= 10 && collectedItemCounts[1] >= 10 && collectedItemCounts[2] >= 10 && collectedItemCounts[3] >= 10 && collectedItemCounts[4] >= 10 && collectedItemCounts[5] >= 10)
                {
                    currentPhase++;
                    GameTime.Stop();
                    StartCoroutine(OnTargetCountArrived());
                }
                else if (currentPhase == 2 && collectedItemCounts[6] >= 10 && collectedItemCounts[7] >= 10 && collectedItemCounts[8] >= 10 && collectedItemCounts[9] >= 10 && collectedItemCounts[10] >= 10 && collectedItemCounts[11] >= 10)
                {
                    GameController.RaiseOnGameOver(true);
                    GameTime.Stop();
                }
            }
        }

        private void OnGameOVer(bool obj)
        {
            m_isGameStopped = true;
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
        
        private IEnumerator ItemRoutine()
        {
            yield return new WaitForSeconds(1f);
            
            while (!m_isGameStopped)
            {
                yield return new WaitForSeconds(m_waitTimeBetweenItems);
                var prefab = GetRandomPrefab();
                SpawnItem(prefab);
            }
        }

        private IEnumerator OnTargetCountArrived()
        {
            OnPhaseCompleted?.Invoke();
            yield return new WaitForSeconds(3f);
            m_iceRation += 5;
            m_waitTimeBetweenItems -= .1f;
            GameTime.Continue();
        }

        private Item GetRandomPrefab()
        {
            var random = Random.Range(0, 100);

            if (random < m_iceRation)
            {
                return ice;
            }

            if (currentPhase == 1)
            {
                var randomNum = Random.Range(0, phaseOneItems.Length);
                
                if (collectedItemCounts[randomNum] >= 10)
                {
                    return GetRandomPrefab();
                }
                
                return phaseOneItems[randomNum];    
            }
            
            var randomNum2 = Random.Range(0, phaseTwoItems.Length);

            if (collectedItemCounts[randomNum2 +6] == 10)
            {
                return GetRandomPrefab();
            }

            return phaseTwoItems[randomNum2];
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
            switch (item.GetId())
            {
                case 0:
                    collectedItemCounts[0]++;
                    break;
                case 1:
                    collectedItemCounts[1]++;
                    break;
                case 2:
                    collectedItemCounts[2]++;
                    break;
                case 3:
                    collectedItemCounts[3]++;
                    break;
                case 4:
                    collectedItemCounts[4]++;
                    break;
                case 5:
                    collectedItemCounts[5]++;
                    break;
                case 6:
                    collectedItemCounts[6]++;
                    break;
                case 7:
                    collectedItemCounts[7]++;
                    break;
                case 8:
                    collectedItemCounts[8]++;
                    break;
                case 9:
                    collectedItemCounts[9]++;
                    break;
                case 10:
                    collectedItemCounts[10]++;
                    break;
                case 11:
                    collectedItemCounts[11]++;
                    break;
                
                default:
                    break;
            }
            
            OnItemCollected?.Invoke(item.GetId());
            m_waitTimeBetweenItems -= .001f;
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

        public int[] GetCollectedItemsCount()
        {
            return collectedItemCounts;
        }
    }
}