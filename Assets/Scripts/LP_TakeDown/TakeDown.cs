using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour
{
    [SerializeField] Transform player;  // Reference to the player
    [SerializeField] float pistolRange = 5f;  // Range for pistol
    [SerializeField] float swordRange = 3f;   // Range for sword
    [SerializeField] float spearRange = 6f;   // Range for spear
    [SerializeField] float fistRange = 2f;    // Range for fists

    private WeaponType weaponType;  // Type of weapon currently equipped
    private bool weaponDetected = false;  // Flag to ensure weapon is only detected once

    // Define an enum named WeaponType
    public enum WeaponType
    {
        // Each item in the enum represents a different weapon type
        Pistol,  // By default, Pistol is assigned the value 0
        Sword,   // Sword gets the value 1
        Spear,   // Spear gets the value 2
        Fists    // Fists gets the value 3
    }

    // Called every frame
    void Update()
    {
        if (!weaponDetected)  // Detect the weapon only once
        {
            DetectWeapon();  // Check what weapon the enemy is holding
        }

        TryTakedown();  // Try to perform a takedown if conditions are met
    }

    // Function to detect the weapon by checking the child objects' names
    void DetectWeapon()
    {
        foreach (Transform child in transform)
        {
            string weaponName = child.name.ToLower();  // Convert name to lowercase for easier comparison

            // Check if the child name contains 'pistol', 'sword', or 'spear'
            if (weaponName.Contains("pistol"))
            {
                weaponType = WeaponType.Pistol;  // Set weapon type to Pistol
                Debug.Log("Found Pistol!");
                weaponDetected = true;  // Set the flag to true so we don't keep detecting
                return;
            }
            else if (weaponName.Contains("sword"))
            {
                weaponType = WeaponType.Sword;  // Set weapon type to Sword
                Debug.Log("Found Sword!");
                weaponDetected = true;
                return;
            }
            else if (weaponName.Contains("spear"))
            {
                weaponType = WeaponType.Spear;  // Set weapon type to Spear
                Debug.Log("Found Spear!");
                weaponDetected = true;
                return;
            }
        }

        // If no weapon is found, assume fists are being used
        weaponType = WeaponType.Fists;
        weaponDetected = true;
        Debug.Log("No weapon found, using Fists.");
    }

    // Function to attempt a takedown if the conditions are right
    void TryTakedown()
    {
        // Calculate the direction to the player
        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;  // Get the distance to the player
        toPlayer.Normalize();  // Normalize the direction to make it a unit vector

        Debug.Log($"Distance to player: {distance}");

        // Check if the enemy is facing the player
        if (Vector3.Angle(transform.forward, toPlayer) > 90f)
        {
            // If the enemy is facing away, turn to face the player
            transform.forward = Vector3.Lerp(transform.forward, toPlayer, Time.deltaTime * 5f);
            Debug.Log("Enemy is turning to face player...");
            return;  // Stop the takedown attempt, the enemy needs to face the player first
        }

        // Check if the enemy is close enough to perform the takedown
        bool canTakedown = false;

        switch (weaponType)  // Check the weapon type and decide if takedown is possible
        {
            case WeaponType.Pistol:
                if (distance < pistolRange)  // Check if player is within pistol range
                {
                    Debug.Log("Within pistol range.");
                    canTakedown = true;
                }
                break;
            case WeaponType.Sword:
                if (distance < swordRange)  // Check if player is within sword range
                {
                    Debug.Log("Within sword range.");
                    canTakedown = true;
                }
                break;
            case WeaponType.Spear:
                RaycastHit hit;
                // Simple raycast to check if the spear hits the player
                if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, spearRange))
                {
                    if (hit.transform == player)  // If ray hits player
                    {
                        Debug.Log("Spear hit player!");
                        canTakedown = true;
                    }
                }
                else
                {
                    Debug.Log("Spear did not hit player.");
                }
                break;
            case WeaponType.Fists:
                if (distance < fistRange)  // Check if player is within fist range
                {
                    Debug.Log("Within fist range.");
                    canTakedown = true;
                }
                break;
        }

        // Perform takedown if conditions met
        if (canTakedown)
        {
            PerformTakedown();
        }
        else
        {
            Debug.Log("Conditions not met for takedown.");
        }
    }

    // Function to perform the takedown
    void PerformTakedown()
    {
        Debug.Log("TAKEDOWN SUCCESSFUL!");
    }
}
