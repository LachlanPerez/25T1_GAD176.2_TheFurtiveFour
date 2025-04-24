using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{

    public class Door : Interactable
    {
        public KeyCard keyCard;

        public override void Interact()
        {
            if (keyCard != null && !keyCard.gameObject.activeSelf)
            {
                Debug.Log("Door opened.");
                // Logic to open the door
                gameObject.SetActive(false); // Example: Hide the door
            }
            else
            {
                Debug.Log("Door is locked. Find the key card.");
            }
        }
    }


}
