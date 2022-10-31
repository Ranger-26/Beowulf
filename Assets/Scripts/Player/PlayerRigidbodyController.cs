using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerRigidbodyController : MonoBehaviour
    {
        public float _speed = 6;
        public float _jumpForce = 6;
        private Rigidbody _rig;
        private Vector2 _input;
        private Vector3 _movementVector;
        public Transform Orientation;
        public bool Grounded;

        private Animator _animator;
        
        private void Start () {
            _rig = GetComponent<Rigidbody> ();
            _animator = GetComponent<Animator>();
            _rig.freezeRotation = true;
        }
        private void Update () {
            _input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            
            if (Input.GetButtonDown ("Jump") && Grounded) {
                _animator.SetTrigger("Jump");
                _rig.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        private void FixedUpdate () {
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
                    _animator.SetTrigger("RunBackwards");
                }
                else if (_input.y > 0)
                {
                    _animator.SetTrigger("Run");
                }
                else if (_input.x < 0)
                {
                    Debug.Log(_input.x);
                    _animator.SetTrigger("RunLeft");
                }
                else if (_input.x > 0)
                {
                    Debug.Log(_input.x);
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