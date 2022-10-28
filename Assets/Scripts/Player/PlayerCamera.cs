using System;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public float xSensitivity;
        public float ySensitivity;

        public Transform Orientation;

        private float xRotation;
        private float yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

            yRotation += mouseX;
             
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}