using System;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CollectGame
{
    public class Basket : MonoBehaviour, IHaveScore
    {
        [SerializeField] private float tiltStrength = 250f;
        [SerializeField] private float tiltSpeed = 25f;
        
        
        private Freezer m_Freezer;
        private Collider m_Collider;
        private Camera m_MainCamera;
        private int m_Score;
        private int m_Health;
        private float m_DeltaX;
        private bool m_isGameStopped;


        public event Action<int> OnScoreChanged;
        public event Action OnDead;


        private void Awake()
        {
            m_Freezer = GetComponent<Freezer>();
            m_Collider = GetComponent<Collider>();
            m_MainCamera = Camera.main;
        }

        private void Start()
        {
            SetScore(0);
            SetHealth(1);
            
            GameTime.OnGameStoped += OnGameStoped;
            GameTime.OnGameContinue += OnGameContinue;
        }

        private void OnGameContinue()
        {
            m_isGameStopped = false;
        }

        private void OnGameStoped()
        {
            m_isGameStopped = true;
        }

        private void OnDestroy()
        {
            
        }

        private void Update()
        {
            Move();
            Tilt();
        }

        
        public void AddScore(int value)
        {
            value = value * Random.Range(100, 125);
            var point = m_Score + value;
            SetScore(point);
        }

        public void TakeDamage(int damage)
        {
            if (IsDead()) return;

            var newHealth = Mathf.Max(0, m_Health - damage);
            SetHealth(newHealth);

            if (IsDead())
            {
                Die();
            }
        }

        public void FreezeForSecond(float duration)
        {
            m_Freezer.FreezeForSecond(duration);
        }
        
        public bool IsDead()
        {
            return m_Health <= 0;
        }
        
        public bool IsFrozen()
        {
            return m_Freezer.IsFrozen();
        }

        public bool IsMoving()
        {
            return Mathf.Abs(m_DeltaX) > Mathf.Epsilon;
        }

        public int GetScore()
        {
            return m_Score;
        }


        private void Die()
        {
            GamePrefs.RemoveLifeCount(1);
            OnDead?.Invoke();
        }
        
        private void Move()
        {
            m_DeltaX = 0f;
            
            if (!CanMove()) return;

            var touchPoint = GetTouchPoint();
            var position = transform.position;
            var maximumDistanceX = GetMaxDistanceX();
            var x = Mathf.Clamp(touchPoint.x, -maximumDistanceX, maximumDistanceX);
            m_DeltaX = x - position.x;

            position.x = x;
            transform.position = position;
        }

        private void Tilt()
        {
            var tiltAngle = m_DeltaX * tiltStrength;
            var t = Time.deltaTime * tiltSpeed;
            var angle = Mathf.LerpAngle(transform.eulerAngles.z, tiltAngle, t);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }

        private Vector3 GetTouchPoint()
        {
            return m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private float GetMaxDistanceX()
        {
            var halfScreenWidth = GetScreenWidth() * 0.5f;
            var halfSizeX = GetBounds().size.x * 0.5f;

            return halfScreenWidth - halfSizeX;
        }

        private float GetScreenWidth()
        {
            return m_MainCamera.GetScreenWorldWidth();
        }

        private Bounds GetBounds()
        {
            return m_Collider.bounds;
        }

        private void SetScore(int value)
        {
            m_Score = value;
            OnScoreChanged?.Invoke(value);
        }

        private void SetHealth(int value)
        {
            m_Health = value;
        }

        private bool CanMove()
        {
            if (m_isGameStopped) return false;

            if (GameTime.IsPaused()) return false;
            
            if (!Input.GetMouseButton(0)) return false;
            
            if (IsDead()) return false;
            
            if (IsFrozen()) return false;

            return true;
        }
    }
}
