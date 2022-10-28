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
        
        private void Start () {
            _rig = GetComponent<Rigidbody> ();
            //Need to freez rotation so the player do not flip over
            _rig.freezeRotation = true;
        }
        private void Update () {
            //Cleanerway to get input
            _input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
            if (Input.GetButtonDown ("Jump") && Grounded) {
                _rig.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        private void FixedUpdate () {
            //Keep the movement vector aligned with the player rotation
            _movementVector =
                Orientation.transform.TransformDirection(new Vector3(_input.x * _speed, _rig.velocity.y, _input.y * _speed));
            //Apply the movement vector to the rigidbody without effecting gravity
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