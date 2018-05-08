using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace ZombieShooter2D
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.
        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        public float minDistance = 0.5f;    // The distance the mouse cursor must be from the player for them to rotate.

        //#if !MOBILE_INPUT
            //int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
            //float camRayLength = 100f;          // The length of the ray from the camera into the scene.
        //#endif

        void Awake()
        {
            //#if !MOBILE_INPUT
                // Create a layer mask for the floor layer.
                //floorMask = LayerMask.GetMask("Floor");
            //#endif

           // Set up references.
           anim = GetComponentInChildren<Animator>();
           //playerRigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Store the input axes.
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            // Move the player around the scene.
            Move(h, v);

            // Turn the player to face the mouse cursor.
            Turning();

            // Animate the player.
            Animating(h, v);
        }

        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, v, 0f);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * -speed * Time.deltaTime;

            // Move the player in the direction they are facing.
            transform.Translate(movement);
        }

        void Turning()
        {
        #if !MOBILE_INPUT
            // Mouse position in the world. It's important to give it some distance from the camera. 
            // If the screen point is calculated right from the exact position of the camera, then it will
            // just return the exact same position as the camera, which is no good.
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

            // Mouse cursor must be a sufficient distance from the player to allow turning.
            // Pythagoras to calculate distance between player and mouse cursor.
            float flatDistance = (transform.position.x-mouseWorldPosition.x) * (transform.position.x - mouseWorldPosition.x) 
                + (transform.position.y - mouseWorldPosition.y) * (transform.position.y - mouseWorldPosition.y);
            float distance = Mathf.Sqrt(flatDistance);

            // If distance between mouse cursor and player is sufficient.
            if(distance > minDistance)
            {
                // Angle between mouse and this object.
                float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

                // Carry out the rotation.
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
            }

            #else
                Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

                if(turnDir != Vector3.zero)
                {
                    // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                    Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                    // Ensure the vector is entirely along the floor plane.
                    playerToMouse.y = 0f;

                    // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                    Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                    // Set the player's rotation to this new rotation.
                    playerRigidbody.MoveRotation(newRotatation);
                }

            #endif
        }

        float AngleBetweenPoints(Vector2 a, Vector2 b) // Calculate the angle between two xy vectors, a & b.
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }
    }
}
