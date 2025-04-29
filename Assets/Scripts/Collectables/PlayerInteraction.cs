using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{

    public class PlayerInteraction : MonoBehaviour
    {
        public GameObject heldKeycard;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickupKeycard();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                DropKeycard();
                
            }
        }

        void TryPickupKeycard()
        {
            if (heldKeycard == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Keycard"))
                    {
                        heldKeycard = hit.collider.gameObject;
                        heldKeycard.SetActive(false); // Hide the keycard
                        Debug.Log("Keycard picked up!");
                    }
                }
            }


            // Normalize the keycard's position
            Vector3 normalizedPosition = transform.position.normalized;
            Debug.Log("Normalized Position: " + normalizedPosition);

            // Calculate the magnitude of the keycard's position
            float magnitude = transform.position.magnitude;
            Debug.Log("Magnitude of Position: " + magnitude);

        }

        void DropKeycard()
        {
            if (heldKeycard != null)
            {
                heldKeycard.SetActive(true); // Show the keycard
                heldKeycard.transform.position = transform.position + transform.forward; // Drop in front of the player

                Rigidbody rb = heldKeycard.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(transform.forward * 5f, ForceMode.Impulse); // Apply a forward force
                }

                heldKeycard = null;
                Debug.Log("Keycard dropped!");
            }
        }




    }

}




