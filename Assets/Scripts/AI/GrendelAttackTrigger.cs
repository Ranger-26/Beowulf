using System;
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
            //attack with swipe
            //knockback player
        }

        public void OnTriggerStay(Collider other)
        {
            //
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}