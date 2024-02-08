using UnityEngine;

namespace Games.CollectGame
{
    public static class CameraExtensions
    {
        public static float GetScreenWorldWidth(this Camera camera)
        {
            return 2f * camera.orthographicSize * camera.aspect;
        }
    }
}