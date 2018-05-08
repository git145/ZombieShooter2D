using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieShooter2D
{
    public class Reset : MonoBehaviour
    {
        public void RestartLevel()
        {
            // Reload the level that is currently loaded.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
