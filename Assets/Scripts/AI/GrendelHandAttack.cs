using System;
using UnityEngine;

namespace AI
{
    public class GrendelHandAttack : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (Grendel.Instance.State != GrendelState.Attacking)
            {
                return;
            }
            if (other.CompareTag("Player"))
            {
                Debug.Log("Attacking player!");
                //damage, sound

                var transform1 = other.transform;
                other.gameObject.GetComponent<Rigidbody>().AddForce(-5000 * transform1.forward);
            }
        }
    }
}