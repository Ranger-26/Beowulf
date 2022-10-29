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
            //
        }

        public IEnumerator OnTriggerExit(Collider other)
        {
            yield return new WaitForSeconds(3f);
            _grendel.SetState(GrendelState.Following);
        }
    }
}