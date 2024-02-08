using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CollectGame
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Item[] itemPrefabs;


        private Camera m_MainCamera;


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
            return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        }
        
        private void SpawnItem(Item prefab)
        {
            var spawnPoint = GetSpawnPoint(prefab);
            var item = Instantiate(prefab, transform);
            
            item.SetPosition(spawnPoint);
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