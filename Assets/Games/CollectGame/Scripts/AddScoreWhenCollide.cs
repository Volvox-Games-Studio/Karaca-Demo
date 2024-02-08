using UnityEngine;

namespace Games.CollectGame
{
    public class AddScoreWhenCollide : MonoBehaviour, ICollidable
    {
        [SerializeField] private int score;
        
        
        public void Collide(Basket basket)
        {
            basket.AddScore(score);
        }
    }
}