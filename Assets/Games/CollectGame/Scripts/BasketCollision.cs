using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Games.CollectGame
{
    public class BasketCollision : MonoBehaviour
    {
        private static readonly Collider[] ColliderBuffer = new Collider[10];
        
        
        [SerializeField] private Basket basket;


        private readonly List<ICollidable> m_Collidables = new();


        private void Update()
        {
            if (basket.IsDead()) return;
            
            if (basket.IsFrozen()) return;
            
            var count = Physics.OverlapBoxNonAlloc(GetCenter(), GetSize() * 0.5f, ColliderBuffer, GetRotation());

            for (var i = 0; i < count; i++)
            {
                ColliderBuffer[i].GetComponents(m_Collidables);

                foreach (var collidable in m_Collidables)
                {
                    collidable.Collide(basket);
                }
            }
        }


        private Vector3 GetCenter()
        {
            return transform.position;
        }

        private Vector3 GetSize()
        {
            return transform.lossyScale;
        }

        private Quaternion GetRotation()
        {
            return transform.rotation;
        }



#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            GizmosExtra.DrawWireCube(GetCenter(), GetRotation(), GetSize());
        }

#endif
    }
}