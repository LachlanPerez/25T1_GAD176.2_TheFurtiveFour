using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapons
{
    public class Pistol : Weapons
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public Transform bulletSpawnPoint;
        [SerializeField] public float bulletSpeed = 20f;

        public override void Shoot()
        {
            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Forward movement: spawn point forward + bullet's own forward
                    Vector3 direction = bulletSpawnPoint.forward + bullet.transform.forward;
                    rb.velocity = direction.normalized * bulletSpeed;
                }
            }
        }

        public override void Reload()
        {
            Debug.Log("Reloading Pistol!");
        }
    }
}
