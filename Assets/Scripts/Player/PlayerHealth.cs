using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private int _health;

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

        public static event Action<int> OnHealthChange;

        public static event Action OnPlayerDie;

        public void RemoveHealth(int amount)
        {
            _health -= amount;
            OnHealthChange?.Invoke(_health);
            Debug.Log($"New health: {_health}");
            if (_health <= 0)
            {
                OnPlayerDie?.Invoke();
                Destroy(gameObject);
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
