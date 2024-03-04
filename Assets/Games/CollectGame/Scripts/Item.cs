using System;
using Helpers;
using UnityEngine;

namespace Games.CollectGame
{
    public class Item : MonoBehaviour, ICollidable, IHitGround
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private GameObject puffParticle;
        [SerializeField] private GameObject starParticle;
        [SerializeField] private int _id;
        [SerializeField] private GameObject iceCollectedSound;
        
        
        public static Action OnHitGround;
        public event Action<Item> OnCollideWithBasket;
        public event Action<Item> OnItemDestroy;

        private void Start()
        {
            GameTime.OnGameStoped += OnGameStopped;
            GameController.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool obj)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            GameTime.OnGameStoped -= OnGameStopped;
            GameController.OnGameOver -= OnGameOver;
        }

        private void OnGameStopped()
        {
            Destroy(gameObject);
        }

        public Bounds GetBounds()
        {
            return collider.bounds;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Collide(Basket basket)
        {
            OnCollideWithBasket?.Invoke(this);
            DestroySelf(true);
        }

        public void HitGround(Ground ground)
        {
            if (_id != -1)
            {
                OnHitGround?.Invoke();
            }
            
            DestroySelf(false);
        }
        

        private void DestroySelf(bool isBasket)
        {
            OnItemDestroy?.Invoke(this);
            if (isBasket)
            {
                if (_id == -1)
                {
                    var iceSound = Instantiate(iceCollectedSound);
                    Destroy(iceSound,2f);
                }
                else
                {
                    CollectedParticle.ShowStatic();    
                }
            }
            else
            {
                var puff = Instantiate(puffParticle);
                puff.transform.position = transform.position;
                Destroy(puff, 2f);
            }
            Destroy(gameObject);
        }

        public int GetId()
        {
            return _id;
        }
    }
}