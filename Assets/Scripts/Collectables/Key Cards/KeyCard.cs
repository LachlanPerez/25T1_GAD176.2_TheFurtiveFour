using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactable
{
    
public class KeyCard : Interactable
    {
             
    
    public override void Interact()
    {            
                
             Debug.Log("KeyCard has been Collected!");

            // Disable KeyCard when collected
            gameObject.SetActive(false);                  
                                           
    }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Interact();
            }
        }


    }

}
