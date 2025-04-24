using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactable
{
public class KeyCard : Interactable
{
        GameObject GameObject;

    public override void Interact()
    {
        Debug.Log("Key card collected.");

        // collecting the key card
        // Hide the key card
        GameObject.SetActive(false); 
    }



}

}
