using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Door
{
    
    public class PressurePlate : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the inspector
    public float openHeight; // Height to move the door up to open
    public float moveSpeed; // Speed at which the door opens
    private Vector3 originalPosition;
    private bool isPressed = false;

    void Start()
    {
        if (door != null)
        {
            originalPosition = door.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
        }
    }

    void Update()
    {
        if (isPressed)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        Vector3 targetPosition = originalPosition + new Vector3(0, openHeight = Random.Range(15, 20), 0);
        door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, moveSpeed = Random.Range(20, 22) * Time.deltaTime);
    }

    private void CloseDoor()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, originalPosition, moveSpeed = Random.Range(10, 15) * Time.deltaTime);
    }
}



}
