using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class TrapProjectile : MonoBehaviour
    {
        [SerializeField] private float lifetime = 5f;

        private Rigidbody rb;

        private void Awake()
        {
            // Get the Rigidbody component when the object wakes up
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            // Automatically destroy the projectile after some time
            Destroy(gameObject, lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {

            Destroy(gameObject); // Destroy projectile when it hits something
        }


        public void Launch(Vector3 velocity)
        {
            if (rb != null)
            {
                rb.velocity = velocity;
            }
        }
    }
}
