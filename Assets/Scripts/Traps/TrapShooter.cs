using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class TrapShooter : BaseTrap
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint; // where projectile spawns
        [SerializeField] private Vector3 fireDirection = Vector3.forward;
        [SerializeField] private float projectileForce = 500f;

        protected override void Fire()
        {
            // Spawn the projectile at firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Calculate direction with rotation applied
            Vector3 rotatedDirection = firePoint.rotation * fireDirection.normalized;

            // Apply force to the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(rotatedDirection * projectileForce);
            }

            Debug.Log("Projectile fired!");
        }
    }
}
