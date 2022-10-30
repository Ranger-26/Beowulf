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

        public float RotateSpeed = 5f;
        
        public GrendelHealth Health;

        public static Grendel Instance;

        private Rigidbody _rigidbody;

        public bool ShouldRotate;
        
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
            _rigidbody = GetComponent<Rigidbody>();
            ShouldRotate = true;
            SetState(GrendelState.Following);
        }

        private void FixedUpdate()
        {
            //follow player until certain distance
            //if certain distance attack
            //if certain health, retreat
            //spit attack on chance
            if (target == null) return;
            float distance = (transform.position-target.position).sqrMagnitude;
            var position = transform.position;
            var position1 = target.position;
            position = Vector3.MoveTowards(position, position1, MoveSpeed * Time.fixedDeltaTime);
            if (State == GrendelState.Following || distance*distance > 25)
            {

                // 
                 //if ()
                 //{
                    
                    _rigidbody.MovePosition(position);
                    Quaternion q = Quaternion.RotateTowards(transform.rotation,
                        Quaternion.LookRotation(position1 - position, Vector3.up), RotateSpeed * Time.fixedDeltaTime);
                    _rigidbody.MoveRotation(q);
                 //}
                // else
            }
            else if (State == GrendelState.Attacking && ShouldRotate)
            {
                Quaternion q = Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(position1 - position, Vector3.up), RotateSpeed * 1.2f * Time.fixedDeltaTime);
                _rigidbody.MoveRotation(q);
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