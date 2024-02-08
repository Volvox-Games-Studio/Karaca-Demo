using Helpers;
using UnityEngine;

namespace Games.CollectGame
{
    public class Ground : MonoBehaviour
    {
        private static readonly Collider[] ColliderBuffer = new Collider[10];


        [SerializeField] private new BoxCollider collider;
        

        private void Update()
        {
            var count = Physics.OverlapBoxNonAlloc(GetCenter(), GetSize() * 0.5f, ColliderBuffer, GetRotation());

            for (var i = 0; i < count; i++)
            {
                if (ColliderBuffer[i].TryGetComponent(out IHitGround target))
                {
                    target.HitGround(this);
                }
            }
        }


        private Vector3 GetCenter()
        {
            var center = collider.center;
            var scale = collider.transform.lossyScale;

            center.x *= scale.x;
            center.y *= scale.y;
            center.z *= scale.z;
            
            return collider.transform.position + center;
        }

        private Vector3 GetSize()
        {
            var scale = collider.transform.lossyScale;
            var size = collider.size;

            return new Vector3(scale.x * size.x, scale.y * size.y, scale.z * size.z);
        }

        private Quaternion GetRotation()
        {
            return collider.transform.rotation;
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