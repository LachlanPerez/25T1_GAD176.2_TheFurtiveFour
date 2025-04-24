using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactable
{
    
    
    public class Interactable : MonoBehaviour
{

    public virtual void Interact()

    {
        Debug.Log("Interacting with an object.");
    }

        public void OnCollisionEnter(Collision collision)
        {
            Interact();
            
        }

    }




}


