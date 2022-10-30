using System;
using System.Collections;
using Player;
using UnityEngine;

namespace AI
{
    public class GrendelAttackTrigger : MonoBehaviour
    {
        private Grendel _grendel;

        public float Cooldown;
        
        private void Start()
        {
            _grendel = GetComponentInParent<Grendel>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _grendel.SetState(GrendelState.Attacking);
            }
        }

        public void OnTriggerStay(Collider other)
        {
            /*
            if (other.CompareTag("Player"))
            {
                if (_grendel.State != GrendelState.Attacking)
                {
                    _grendel.SetState(GrendelState.Attacking);
                }
            }
            */
        }

        public IEnumerator OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //add logic based on grendel health
                Grendel.Instance.ShouldRotate = false;
                yield return new WaitForSeconds(Cooldown);
                _grendel.SetState(GrendelState.Following);
                Grendel.Instance.ShouldRotate = true;
            }
        }
    }
}