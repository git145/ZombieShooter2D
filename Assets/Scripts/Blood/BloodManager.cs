using UnityEngine;
using System.Collections;

namespace ZombieShooter2D
{
    public class BloodManager : MonoBehaviour
    {

        public float maxBlood = 50f; // The maximum number of blood objects allowed in play at one time (used to prevent memory
                                     // issues).
        GameObject[] bloodCount; // An array representing the number of blood objects currently in play.
        public float bloodCountLength; // The number of blood objects currently in play.

        // Update is called once per frame
        void Update()
        {
            bloodCount = GameObject.FindGameObjectsWithTag("Blood");
            bloodCountLength = bloodCount.Length;
        }
    }
}
