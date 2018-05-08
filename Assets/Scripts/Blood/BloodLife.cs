using UnityEngine;
using System.Collections;

namespace ZombieShooter2D
{ 
    public class BloodLife : MonoBehaviour
    {
        public float fadeSpeed = 1f; // The speed at which the blood will fade.
        public float fadeTime = 5f; // Time before the blood fades.
        float timer = 0f; // Time since blood object was created.
        SpriteRenderer spriteRenderer; // A reference to the objects Sprite Renderer component.
        
        // Use this for initialization
	    void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
	    }
	
	    // Update is called once per frame
	    void Update()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            if(timer >= fadeTime)
            {
                // Fade blood object to clear.
                spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.clear, fadeSpeed * Time.deltaTime);
            }

            if(spriteRenderer.color == Color.clear)
            {
                Destroy(gameObject, 0f);
            }
        }
    }
}
