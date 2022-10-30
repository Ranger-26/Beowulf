using System;
using Player;
using UnityEngine;

namespace AI
{
    public class GrendelHandAttack : MonoBehaviour
    {
        public int BaseHandDamage = 50;
        public void OnTriggerEnter(Collider other)
        {
            if (Grendel.Instance.State != GrendelState.Attacking)
            {
                return;
            }
            if (other.CompareTag("Player"))
            { 
                //damage, sound
                
                var transform1 = other.transform;
                var distance = Mathf.Abs(Vector3.Distance(transform.root.position, transform1.position));
                PlayerHealth.Instance.RemoveHealth(BaseHandDamage/distance);
                //other.gameObject.GetComponent<Rigidbody>().AddForce(-50 * transform1.forward + Vector3.up, ForceMode.Impulse);
            }
        }
    }
}