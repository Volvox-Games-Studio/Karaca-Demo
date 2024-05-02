using System;
using System.Collections;
using System.Runtime.InteropServices;
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
        [SerializeField] private Item[] phaseThreeItems;
        [SerializeField] private Item ice;
        [SerializeField] private Countdown cd;
        
        
        public static Action<int> OnItemCollected;
        public static Action<int> OnPhaseCompleted;

        private int[] collectedItemCounts = new int[6];

        
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
        
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SendGameStart();
#endif
        private void Start()
        {
        #if UNITY_WEBGL && !UNITY_EDITOR
            SendGameStart();
        #endif

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
                if (currentPhase == 1 && collectedItemCounts[0] >= 20 && collectedItemCounts[1] >= 20 )
                {
                    currentPhase++;
                    GameTime.Stop();
                    StartCoroutine(OnTargetCountArrived(1));
                }
                else if (currentPhase == 2 && collectedItemCounts[2] >= 20 && collectedItemCounts[3] >= 20 )
                {
                    currentPhase++;
                    GameTime.Stop();
                    StartCoroutine(OnTargetCountArrived(2));
                }
                else if (currentPhase == 3 && collectedItemCounts[4] >= 20 && collectedItemCounts[5] >= 20 )
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
            yield return new WaitForSeconds(2f);
            StartCoroutine(cd.GeriSayimiBaslat());
            yield return new WaitForSeconds(3f);
            
            while (!m_isGameStopped)
            {
                yield return new WaitForSeconds(m_waitTimeBetweenItems);
                var prefab = GetRandomPrefab();
                SpawnItem(prefab);
            }
        }

        private IEnumerator OnTargetCountArrived(int phase)
        {
            OnPhaseCompleted?.Invoke(phase);
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
                
                if (collectedItemCounts[randomNum] >= 20)
                {
                    return GetRandomPrefab();
                }
                
                return phaseOneItems[randomNum];    
            }
            
            if (currentPhase == 3)
            {
                var randomNum = Random.Range(0, phaseOneItems.Length);
                
                if (collectedItemCounts[randomNum+4] >= 20)
                {
                    return GetRandomPrefab();
                }
                
                return phaseThreeItems[randomNum];    
            }
            
            if (currentPhase == 2)
            {
                var randomNum = Random.Range(0, phaseOneItems.Length);
                
                if (collectedItemCounts[randomNum+2] >= 20)
                {
                    return GetRandomPrefab();
                }
                
                return phaseTwoItems[randomNum];    
            }

            return null;
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