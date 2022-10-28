using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private int _health;

        public static event Action<int> OnHealthChange;

        public static event Action OnPlayerDie; 

        
        public void RemoveHealth(int amount)
        {
            _health -= amount;
            OnHealthChange?.Invoke(amount);
            if (_health <= 0)
            {
                OnPlayerDie?.Invoke();
            }
        }
    }
}
