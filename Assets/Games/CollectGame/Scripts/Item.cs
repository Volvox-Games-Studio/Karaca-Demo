using System;
using Helpers;
using UnityEngine;

namespace Games.CollectGame
{
    public class Item : MonoBehaviour, ICollidable, IHitGround
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private GameObject puffParticle;
        [SerializeField] private int _id;

        public event Action<Item> OnCollideWithBasket;
        public event Action<Item> OnItemDestroy;

        private void Start()
        {
            GameTime.OnGameStoped += OnGameStopped;
        }

        private void OnDestroy()
        {
            GameTime.OnGameStoped -= OnGameStopped;
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
            DestroySelf();
        }

        public void HitGround(Ground ground)
        {
            DestroySelf();
        }
        

        private void DestroySelf()
        {
            OnItemDestroy?.Invoke(this);
            var puff = Instantiate(puffParticle);
            puff.transform.position = transform.position;
            Destroy(puff, 2f);
            Destroy(gameObject);
        }

        public int GetId()
        {
            return _id;
        }
    }
}