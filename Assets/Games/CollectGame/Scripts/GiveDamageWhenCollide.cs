using UnityEngine;

namespace Games.CollectGame
{
    public class GiveDamageWhenCollide : MonoBehaviour, ICollidable
    {
        [SerializeField] private int damage;
        
        
        public void Collide(Basket basket)
        {
            basket.TakeDamage(damage);
        }
    }
}