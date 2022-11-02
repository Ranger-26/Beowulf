using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerRigidbodyController : MonoBehaviour
    {
        public float _speed = 6;
        public float _jumpForce = 6;
        private Rigidbody _rig;
        [SerializeField]
        private Vector2 _input;
        private Vector3 _movementVector;
        public Transform Orientation;
        public bool Grounded;

        private Animator _animator;
        
        [SerializeField]
        private bool _canPunch;

        public float PunchCooldown = 2f;
        
        private void Start () {
            _rig = GetComponent<Rigidbody> ();
            _animator = GetComponent<Animator>();
            _rig.freezeRotation = true;
            _canPunch = true;
        }

        private void Update () {
            if (PlayerHealth.Instance != null && PlayerHealth.Instance.IsDead) return;
            _input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            
            if (Input.GetButtonDown ("Jump") && Grounded) {
                _animator.SetTrigger("Jump");
                _rig.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _canPunch)
            {
                _animator.Play("PunchLeft");
                StartCoroutine(ResetPunchRoutine());
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && _canPunch)
            {
                _animator.Play("PunchRight");
                StartCoroutine(ResetPunchRoutine());
            }
        }

        public bool IsPunching()
        {
            return _animator.GetCurrentAnimatorStateInfo(1).IsName("PunchLeft") ||
                   _animator.GetCurrentAnimatorStateInfo(1).IsName("PunchRight");
        }
        
        public IEnumerator ResetPunchRoutine()
        {
            _canPunch = false;
            yield return new WaitForSeconds(PunchCooldown);
            _canPunch = true;
        }
        
        private void FixedUpdate() {
            _movementVector =
                Orientation.transform.TransformDirection(new Vector3(_input.x * _speed, _rig.velocity.y, _input.y * _speed));
            if (_input.x == 0 && _input.y == 0)
            {
                _animator.ResetTrigger("Run");
                _animator.ResetTrigger("RunBackwards");
                _animator.ResetTrigger("RunLeft");
                _animator.ResetTrigger("RunRight");
                _animator.SetTrigger("Idle");
            }
            else
            {
                _animator.ResetTrigger("Idle");
                if (_input.y < 0)
                {
                    _animator.ResetTrigger("Run");
                    _animator.ResetTrigger("RunLeft");
                    _animator.ResetTrigger("RunRight");
                    _animator.SetTrigger("RunBackwards");
                }
                else if (_input.y > 0)
                {
                    _animator.ResetTrigger("RunBackwards");
                    _animator.ResetTrigger("RunLeft");
                    _animator.ResetTrigger("RunRight");
                    _animator.SetTrigger("Run");
                }
                else if (_input.x < 0)
                {
                    Debug.Log("Setting trigger to run left.");
                    _animator.ResetTrigger("RunBackwards");
                    _animator.ResetTrigger("Run");
                    _animator.ResetTrigger("RunRight");
                    _animator.SetTrigger("RunLeft");
                }
                else if (_input.x > 0)
                {
                    _animator.ResetTrigger("RunBackwards");
                    _animator.ResetTrigger("RunLeft");
                    _animator.ResetTrigger("Run");
                    _animator.SetTrigger("RunRight");
                }
            }
            _rig.velocity = new Vector3 (_movementVector.x, _rig.velocity.y, _movementVector.z);
        }
        


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                Grounded = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Ground"))
            {
                Grounded = false;
            }
        }
    }
}