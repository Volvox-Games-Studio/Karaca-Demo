using System;
using Helpers;
using UnityEngine;

namespace Games.CollectGame
{
    public class CollectedParticle : MonoBehaviour
    {
       
        [SerializeField] private GameObject _particle;
        
        
        public static CollectedParticle Instance;


        private void Start()
        {
            Instance = this;
        }
        

        private void ShowParticle()
        {
            var particle = Instantiate(_particle, transform);
            Destroy(particle, 1f);
        }

        public static void ShowStatic()
        {
            Instance.ShowParticle();
        }
    }
}