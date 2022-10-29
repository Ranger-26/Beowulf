using System;
using UnityEngine;

namespace AI
{
    public class TriggerAttack : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Attacking player!");
            }
        }
    }
}