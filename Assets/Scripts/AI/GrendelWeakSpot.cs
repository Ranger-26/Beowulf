using System;
using UnityEngine;

namespace AI
{
    public class GrendelWeakSpot : MonoBehaviour
    {
        public int Damage;
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Grendel being damaged.");
                GrendelHealth.Instance.RemoveHealth(Damage);
                other.gameObject.GetComponent<Rigidbody>().AddForce(10 * Vector3.up, ForceMode.Impulse);
            }
        }
    }
}