using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapon
{
    public class Pistol : Weapon
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public Transform bulletSpawnPoint;
        [SerializeField] public float bulletSpeed = 4;
        [SerializeField] public int clipSize = 1;
        [SerializeField] public float reloadTime = 5;

        private int currentAmmo;
        private bool isReloading = false;

        private void Start()
        {
            currentAmmo = clipSize;
            Debug.Log("Starting with full ammo: " + currentAmmo);
        }

        public virtual void Shoot()
        {
            if (isReloading)
            {
                Debug.Log("Can't shoot while reloading!");
                return;
            }

            if (currentAmmo > 0)
            {
                // Instantiate and fire bullet
                if (bulletPrefab != null && bulletSpawnPoint != null)
                {
                    Vector3 spawnPos = bulletSpawnPoint.position + bulletSpawnPoint.forward * 0.1f;
                    GameObject bullet = Instantiate(bulletPrefab, spawnPos, bulletSpawnPoint.rotation);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
                        Destroy(bullet, 1); // destroy bullet after 5 seconds
                        Debug.Log("Pistol shot! Ammo left: " + (currentAmmo - 1));
                    }
                }

                // Decrease ammo
                currentAmmo--;

                // Trigger reload if ammo runs out
                if (currentAmmo <= 0)
                {
                    Debug.Log("Out of ammo, reloading...");
                    Reload();
                }
            }
            else
            {
                Debug.Log("No bullets left! Reloading...");
                Reload();
            }
        }

        public virtual void Reload()
        {
            Debug.Log("Reloading Pistol...");
            if (!isReloading)
            {
                isReloading = true;
                Debug.Log("Starting reload...");
                Invoke(nameof(FinishReload), reloadTime);
            }
        }

        private void FinishReload()
        {
            currentAmmo = clipSize;
            isReloading = false;
            Debug.Log("Pistol reloaded! Ammo restored to: " + currentAmmo);
        }
    }
}
