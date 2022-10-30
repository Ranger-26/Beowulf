using System;
using System.Collections;
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
                _grendel.SetState(GrendelState.Attacking1);
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_grendel.State != GrendelState.Attacking1)
                {
                    _grendel.SetState(GrendelState.Attacking1);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            _grendel.SetState(GrendelState.Following);
        }
    }
}