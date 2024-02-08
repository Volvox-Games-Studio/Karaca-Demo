using System;
using UnityEngine;

namespace Games.CollectGame
{
    public class Freezer : MonoBehaviour
    {
        public event Action OnFreeze;
        public event Action OnUnfreeze;


        private float m_TimeLeft;


        private void Update()
        {
            if (!IsFrozen()) return;

            var timeLeft = Mathf.Max(0f, m_TimeLeft - Time.deltaTime);
            SetTimeLeft(timeLeft);

            if (!IsFrozen())
            {
                OnUnfreeze?.Invoke();
            }
        }


        public void FreezeForSecond(float duration)
        {
            if (!IsFrozen())
            {
                OnFreeze?.Invoke();
            }
            
            SetTimeLeft(duration);
        }
        
        public bool IsFrozen()
        {
            return m_TimeLeft > 0f;
        }


        private void SetTimeLeft(float value)
        {
            m_TimeLeft = value;
        }
    }
}