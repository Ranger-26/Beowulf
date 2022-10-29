using System;
using UnityEngine;

namespace AI
{
    public class GrendelHealth : MonoBehaviour
    {
        private Grendel _grendel;
        
        private int _health;

        public static event Action<int, int> OnGrendelHurt;

        public static event Action OnGrendelDie;

        private void Start()
        {
            _grendel = GetComponent<Grendel>();
            OnGrendelDie += OnDie;
        }

        private void OnDestroy()
        {
            OnGrendelDie -= OnDie;
        }

        public void RemoveHealth(int damage, int newamount)
        {
            _health -= damage;
            OnGrendelHurt?.Invoke(damage, _health);
            if (_health <= 0)
            {
                OnGrendelDie?.Invoke();
            }
        }

        public void OnDie()
        {
            //play die sound effect
            _grendel.SetState(GrendelState.Dead);
        }
    }
}