using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private float _health;

        public static PlayerHealth Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            AddHealth(100);
        }

        public static event Action<float> OnHealthChange;

        public static event Action OnPlayerDie;

        public void RemoveHealth(float amount)
        {
            if (_health < 0) return;
            _health -= amount;
            OnHealthChange?.Invoke(_health);
            Debug.Log($"New health: {_health}");
            if (_health <= 0)
            {
                OnPlayerDie?.Invoke();
            }
        }

        public void AddHealth(int amount)
        {
            _health += amount;
            if (_health > 100)
            {
                _health = 100;
            }
            OnHealthChange?.Invoke(_health);
            Debug.Log($"New health: {_health}");
        }
        
        
    }
}
