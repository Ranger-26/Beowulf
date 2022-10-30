using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        private float rotX;
        private float rotY;

        public Camera Camera;
    

        void Update()
        {
            rotX = Input.GetAxis("Mouse Y");
            rotY = Input.GetAxis("Mouse X");
            Vector3 rotation = transform.localEulerAngles;
            rotation.y += rotY;
            transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.up);


            Vector3 camRotation = Camera.transform.localEulerAngles;
            camRotation.x -= rotX;
            Camera.transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.right);
        }
    }
}