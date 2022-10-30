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

        public static Grendel Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

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
                 float distance = (transform.position-target.position).sqrMagnitude;
                 if (distance*distance > 25)
                 {
                    var position = transform.position;
                    var position1 = target.position;
                    position = Vector3.MoveTowards(position, position1, MoveSpeed * Time.deltaTime);
                    transform.position = position;
                    transform.rotation = Quaternion.LookRotation(position1 - position, Vector3.up);
                 }
                 else
                 {  
                     //SetState(GrendelState.Attacking1);
                 }
            }
        }

        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                //hurt player if player touches grendel
            }
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            if (collisionInfo.collider.CompareTag("Player"))
            {
                //keep damaging player
            }
        }

        public void SetState(GrendelState state)
        {
            switch (state)
            {
                case GrendelState.Following:
                    Debug.Log("Setting follow triggger");
                    State = GrendelState.Following;
                    animator.SetTrigger("Follow");
                    break;
                case GrendelState.Attacking:
                    State = GrendelState.Attacking;
                    Debug.Log("Setting attack 1 triggger");
                    animator.SetTrigger("Attack1");
                    break;
                case GrendelState.Cooldown:
                    Debug.Log("Setting cooldown trigger!");
                    State = GrendelState.Cooldown;
                    animator.SetTrigger("Cooldown");
                    break;
                case GrendelState.Dead:
                    Debug.Log("Setting dead triggger");
                    State = GrendelState.Dead;
                    animator.SetTrigger("Dead");
                    break;
            }
        }
    }
}