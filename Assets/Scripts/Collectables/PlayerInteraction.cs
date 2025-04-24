using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{

    public class PlayerInteraction : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }


}
