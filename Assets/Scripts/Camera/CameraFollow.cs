using UnityEngine;
using System.Collections;

namespace ZombieShooter2D
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
        Vector3 transformPosition;          // A vector representing the cameras initial position.
        public float transformZ = -20f;     // The camera's position in the z direction.
        Vector3 offset;                     // The initial offset from the target.

        void Start ()
        {
            transformPosition.Set(target.position.x, target.position.y, transformZ);  // (player's x, player's y position, 
            // camera's z position)
            
            // Set the initial camera position.
            transform.position = transformPosition;
            
            // Calculate the initial offset.
            offset = transform.position - target.position;
        }

        void FixedUpdate ()
        {
            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
