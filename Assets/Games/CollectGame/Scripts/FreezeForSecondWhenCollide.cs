using UnityEngine;

namespace Games.CollectGame
{
    public class FreezeForSecondWhenCollide : MonoBehaviour, ICollidable
    {
        [SerializeField] private float duration;


        public void Collide(Basket basket)
        {
            basket.FreezeForSecond(duration);
        }
    }
}