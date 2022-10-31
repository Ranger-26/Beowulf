using System;
using AI;
using UnityEngine;

namespace Player
{
    public class PlayerFistTrigger : MonoBehaviour
    {
        public int BaseDamage = 20;

        public PlayerRigidbodyController Controller;

        private void Start()
        {
            Controller = GetComponentInParent<PlayerRigidbodyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Grendel")) return;
            
            if (!Controller.IsPunching())
            {
                Debug.Log("Touching grendel but not punching");
            }
            else
            {
                var distance = Mathf.Abs(Vector3.Distance(transform.position, other.transform.position));
                GrendelHealth.Instance.RemoveHealth((int)(BaseDamage/distance));
                Debug.Log($"Damaged grendel! {(int)(BaseDamage/distance)}");
            }
        }
    }
}