using UnityEngine;

namespace Games.CollectGame
{
    public class Item : MonoBehaviour, ICollidable, IHitGround
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private GameObject puffParticle;


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
            DestroySelf();
        }

        public void HitGround(Ground ground)
        {
            DestroySelf();
        }
        

        private void DestroySelf()
        {
            var puff = Instantiate(puffParticle);
            puff.transform.position = transform.position;
            Destroy(puff, 2f);
            Destroy(gameObject);
        }
    }
}