using System;
using UnityEngine;

namespace AI
{
    public class Grendel : MonoBehaviour
    {
        public Transform target;

        public Animator animator;
        
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, .1f);
            animator.Play("Walk");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("attck");
                animator.Play("Attack");
            }
        }
    }
}