using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if it hit the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player!");
            Destroy(gameObject); // Destroy the bullet on impact
        }
        else
        {
            Debug.Log("Bullet hit something else: " + collision.gameObject.name);
        }
    }
}
