using UnityEngine;
using System.Collections;

namespace ZombieShooter2D
{
    public class EnemyMovement : MonoBehaviour
    {
        Transform playerTransform;          // The object that that the enemy will be following.
        public float speed = 2f;            // The speed with which the enemy will follow the player.
        //public float minDistance = 1f;    // The minimum distance that the enemy must be from the player before it attempts to move.
        //float range;
        //float rangeFactor;
        GameObject player;                  // Reference to the player GameObject.
        Animator anim;                      // A reference to the enemy's animator component.
        SpriteRenderer spriteRenderer;      // A reference to the enemy's SpriteRenderer component.

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>(); // Find the EnemyHealth script on the enemy.
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); //  Find the PlayerHealthScript on the player.
            playerTransform = player.transform; // Lock onto the position of the player.

            Animating(enemyHealth, playerHealth);

            if(enemyHealth.currentHealth != 0 && playerHealth.currentHealth != 0
                && transform.position != playerTransform.position) // If the enemy has health, the player has health 
                                                                   // and the enemy does not have the same location as the player.
            {
                Turning(); // Rotate the enemy to face the player.
                Movement(); // Enemy follows player.
            }

            else if(playerHealth.currentHealth == 0) // If the player is dead.
            {
                spriteRenderer.sortingOrder = 1; // Put the enemy above the player.
            }
        }

        void Turning() 
        {
            // Rotate the enemy to face the player.
            // Calculate the angle between the enemy and the player.
            float angle = AngleBetweenPoints(transform.position, playerTransform.position);

            // Carry out the rotation.
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        }

        void Movement ()
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        void Animating (EnemyHealth enemyHealth, PlayerHealth playerHealth)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = enemyHealth.currentHealth != 0 && playerHealth.currentHealth != 0
            && transform.position != playerTransform.position; // The boolean is true if the enemy has health, the player has health 
            // and the enemy does not have the same location as the player.

            // Tell the animator whether or not the enemy is walking.
            anim.SetBool("IsWalking", walking);
        }

        float AngleBetweenPoints(Vector2 a, Vector2 b) // Calculate the angle between two xy vectors, a & b.
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}