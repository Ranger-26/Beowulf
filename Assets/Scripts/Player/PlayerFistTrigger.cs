using System;
using AI;
using UnityEngine;

namespace Player
{
    public class PlayerFistTrigger : MonoBehaviour
    {
        public int BaseDamage = 20;

        public PlayerRigidbodyController Controller;

        public AudioClip PunchHitSound;
        
        private void Start()
        {
            Controller = GetComponentInParent<PlayerRigidbodyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Grendel")) return;
            
            
            if (Controller.IsPunching())
            {
                var distance = Mathf.Abs(Vector3.Distance(transform.position, other.transform.position));
                PlayerEffects.Instance.AudioSource.SafePlayOneShot(PunchHitSound, "PunchHitSound");
                GrendelHealth.Instance.RemoveHealth((int)(BaseDamage/distance));
                Debug.Log($"Damaged grendel! {(int)(BaseDamage/distance)}");
            }
        }
    }
}