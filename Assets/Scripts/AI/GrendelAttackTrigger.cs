using System;
using System.Collections;
using Player;
using UnityEngine;

namespace AI
{
    public class GrendelAttackTrigger : MonoBehaviour
    {
        private Grendel _grendel;

        private void Start()
        {
            _grendel = GetComponentInParent<Grendel>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _grendel.SetState(GrendelState.Attacking);
                PlayerHealth.Instance.RemoveHealth(5);
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_grendel.State != GrendelState.Attacking)
                {
                    _grendel.SetState(GrendelState.Attacking);
                    PlayerHealth.Instance.RemoveHealth(2);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            _grendel.SetState(GrendelState.Following);
        }
    }
}