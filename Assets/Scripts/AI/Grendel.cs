using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class Grendel : MonoBehaviour
    {
        public Transform target;

        public Animator animator;

        public GrendelState State;

        public float MoveSpeed = 5f;

        public GrendelHealth Health;
        private void Start()
        {
            //roar
            animator = GetComponent<Animator>();
            Health = GetComponent<GrendelHealth>();
            SetState(GrendelState.Following);
        }

        private void Update()
        {
            //follow player until certain distance
            //if certain distance attack
            //if certain health, retreat
            //spit attack on chance

            if (State == GrendelState.Following)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (Math.Abs(distance) > 5)
                {
                    var position = transform.position;
                    var position1 = target.position;
                    position = Vector3.MoveTowards(position, position1, MoveSpeed * Time.deltaTime);
                    transform.position = position;
                    Quaternion q = Quaternion.LookRotation(position1 - position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, 2);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Player collided with grendel!");
                //hurt player if player touches grendel
            }
        }
        
        public void SetState(GrendelState state)
        {
            switch (state)
            {
                case GrendelState.Following:
                    State = GrendelState.Following;
                    animator.SetTrigger("Follow");
                    break;
                case GrendelState.Attacking1:
                    State = GrendelState.Attacking1;
                    Debug.Log("Setting attack 1 triggger");
                    animator.SetTrigger("Attack1");
                    break;
                case GrendelState.Attacking2:
                    Debug.Log("Setting attack 2 triggger");
                    State = GrendelState.Attacking2;
                    animator.SetTrigger("Attack2");
                    break;
                case GrendelState.Dead:
                    State = GrendelState.Dead;
                    animator.SetTrigger("Dead");
                    break;
            }
        }
    }
}