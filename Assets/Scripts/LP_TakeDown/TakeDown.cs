using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapon
{
    public class TakeDown : MonoBehaviour
    {
        [SerializeField] Transform player;  // Reference to the player

        [SerializeField] float pistolRange = 5f;  // Range for pistol
        [SerializeField] float swordRange = 3f;   // Range for sword
        [SerializeField] float spearRange = 6f;   // Range for spear
        [SerializeField] float fistRange = 2f;    // Range for fists

        [SerializeField] WeaponType weaponType = WeaponType.Fists;
        [SerializeField] private Weapon weapon; // Assign the appropriate weapon script (Sword, Spear, Fist, etc.)
        [SerializeField] private Pistol pistol;


        public enum WeaponType
        {
            Pistol,
            Sword,
            Spear,
            Fists
        }

        void Update()
        {
            TryTakedown();
        }

        void TryTakedown()
        {
            Vector3 toPlayer = player.position - transform.position;
            float distance = toPlayer.magnitude;
            toPlayer.Normalize();

            Debug.Log($"Distance to player: {distance}");

            if (Vector3.Angle(transform.forward, toPlayer) > 90f)
            {
                transform.forward = Vector3.Lerp(transform.forward, toPlayer, Time.deltaTime * 5f);
                Debug.Log("Enemy is turning to face player...");
                return;
            }

            bool canTakedown = false;

            switch (weaponType)
            {
                case WeaponType.Pistol:
                    if (distance < pistolRange)
                    {
                        Debug.Log("Within pistol range.");
                        canTakedown = true;
                    }
                    break;
                case WeaponType.Sword:
                    if (distance < swordRange)
                    {
                        Debug.Log("Within sword range.");
                        canTakedown = true;
                    }
                    break;
                case WeaponType.Spear:
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up, toPlayer, out hit, spearRange))
                    {
                        if (hit.transform == player)
                        {
                            Debug.Log("Within spear range");
                            canTakedown = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Spear did not hit player.");
                    }
                    break;
                case WeaponType.Fists:
                    if (distance < fistRange)
                    {
                        Debug.Log("Within fist range.");
                        canTakedown = true;
                    }
                    break;
            }

            if (canTakedown)
            {
                // Call the weapon's Hit() logic if it's assigned
                if (weapon != null)
                {
                    weapon.Hit();
                    pistol.Shoot();
                }
                else
                {
                    Debug.LogWarning("Weapon component not assigned!");
                }

                PerformTakedown();
            }
            else
            {
                Debug.Log("Conditions not met for takedown");
            }
        }

        void PerformTakedown()
        {
            Debug.Log("TAKEDOWN SUCCESSFUL!");
        }
    }
}
