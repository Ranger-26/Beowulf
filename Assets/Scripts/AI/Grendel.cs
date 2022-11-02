using System;
using System.Collections;
using Player;
using Unity.VisualScripting;
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

        private AudioSource _audioSource;
        
        [Header("Sounds")] 
        public AudioClip InitialRoarSound;

        public AudioClip AttackRoar;

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

        public bool Landed;
        
        private void Start()
        {
            //roar
            _audioSource = GetComponent<AudioSource>();
            Health = GetComponent<GrendelHealth>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private IEnumerator OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ground") && !Landed)
            {
                ShouldRotate = true;
                animator = GetComponent<Animator>();
                _audioSource.SafePlayOneShot(InitialRoarSound, "RoarInitial");
                //TODO:screen shake, particles
                Landed = true;
                yield return new WaitForSeconds(1f);
                Sounds.Instance.Play();
                SetState(GrendelState.Following);
            }
        }

        private void FixedUpdate()
        {
            //follow player until certain distance
            //if certain distance attack
            //if certain health, retreat
            //spit attack on chance
            if (target == null || State == GrendelState.Dead || State == GrendelState.None) return;
            float distance = (transform.position-target.position).sqrMagnitude;
            var position = transform.position;
            var position1 = target.position;
            Vector3 moveto = new Vector3(position1.x, 0, position1.z);
            position = Vector3.MoveTowards(position, moveto, MoveSpeed * Time.fixedDeltaTime);
            if (State == GrendelState.Following || (distance*distance > 25 && State == GrendelState.Attacking))
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
            if (State == GrendelState.Dead) return;
            switch (state)
            {
                case GrendelState.Following:
                    Debug.Log("Setting follow triggger");
                    State = GrendelState.Following;
                    animator.SetTrigger("Follow");
                    break;
                case GrendelState.Attacking:
                    _audioSource.SafePlayOneShot(AttackRoar, "GrendelAttack");
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
                    animator.SetTrigger("Die");
                    break;
            }
        }

        private float timer;

        private float totalTime;
        
        public float collisionDamageDelay = 3f;

        public float collisionDamage = 2f;
        public void OnCollisionStay(Collision collisionInfo)
        {
            if (State == GrendelState.Dead) return;
            
            totalTime += Time.deltaTime;
            if (collisionDamageDelay > 0.8f)
            {
                collisionDamageDelay -= Time.deltaTime;
            }
            //TODO: Increase damage based on delta time
            if (collisionInfo.collider.CompareTag("Player"))
            {
                Debug.Log("Player in grendel!");
                if (timer >= collisionDamageDelay)
                {
                    PlayerHealth.Instance.RemoveHealth(collisionDamage * totalTime);
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }

        public void OnCollisionExit(Collision other)
        {
            timer = 0;
        }
    }
}