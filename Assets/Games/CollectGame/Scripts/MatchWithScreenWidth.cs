using UnityEngine;

namespace Games.CollectGame
{
    public class MatchWithScreenWidth : MonoBehaviour
    {
        private Camera m_MainCamera;


        private void Awake()
        {
            m_MainCamera = Camera.main;
            
            Match();
        }

        
        private void Match()
        {
            var scale = transform.localScale;
            var scaleX = GetScreenWidth();

            scale.x = scaleX;
            transform.localScale = scale;
        }

        private float GetScreenWidth()
        {
            return m_MainCamera.GetScreenWorldWidth();
        }
    }
}