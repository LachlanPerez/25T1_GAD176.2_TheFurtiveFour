using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBurstAbility : MonoBehaviour
{
    [SerializeField] private ThirdPersonController MoveSpeed;


    public float speedBoost = 2f;  // Multiplier for speed
    public float duration = 5f;    // Duration of speed power-up 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player colliding
        {
            ThirdPersonController player = other.GetComponent<ThirdPersonController>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
        }
    }

    private IEnumerator ApplySpeedBoost(ThirdPersonController player)
    {
        player.MoveSpeed *= speedBoost; // Apply the speed boost
        gameObject.SetActive(false); // Hide power-up while active

        // Wait for duration
        yield return new WaitForSeconds(duration); 
        
        
        // Reset speed after time is up
        player.MoveSpeed /= speedBoost;
        // Remove power-up after use
        Destroy(gameObject); 

        
        
    }
}

