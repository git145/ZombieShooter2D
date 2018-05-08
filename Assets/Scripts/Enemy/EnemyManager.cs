using UnityEngine;

namespace ZombieShooter2D
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
        public float maxEnemies = 50f;          // The maximum number of enemies allowed in play at one time (used to prevent memory
                                                // issues.
        GameObject[] enemyCount;                // An array representing the number of enemies currently in play.
        public float enemyCountLength;          // The number of enemies currently in play.

        void Start()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
        }

        void Update()
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
            enemyCountLength = enemyCount.Length;
        }

        void Spawn()
        {
            // If the player has no health left...
            if(playerHealth.currentHealth > 0f && enemyCountLength < maxEnemies)
            {
                // Find a random index between zero and one less than the number of spawn points.
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
        }
    }
}
