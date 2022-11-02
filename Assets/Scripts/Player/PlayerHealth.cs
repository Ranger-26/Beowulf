using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float Health { get; private set; }

        public PlayerRigidbodyController PlayerCtlr;
        
        public static PlayerHealth Instance;

        public bool IsDead;
        
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
            PlayerCtlr = GetComponent<PlayerRigidbodyController>();
            AddHealth(100);
        }

        public static event Action<float> OnHealthChange;

        public static event Action OnPlayerDie;

        public void RemoveHealth(float amount)
        {
            if (Health < 0) return;
            Health -= amount;
            OnHealthChange?.Invoke(Health);
            Debug.Log($"New health: {Health}");
            if (Health <= 0)
            {
                IsDead = true;
                OnPlayerDie?.Invoke();
            }
        }

        public void AddHealth(int amount)
        {
            Health += amount;
            if (Health > 100)
            {
                Health = 100;
            }
            OnHealthChange?.Invoke(Health);
            Debug.Log($"New health: {Health}");
        }
        
        
    }
}
