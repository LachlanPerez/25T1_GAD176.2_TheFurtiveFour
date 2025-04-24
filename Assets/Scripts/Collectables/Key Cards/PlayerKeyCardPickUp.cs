using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyCardPickUp : MonoBehaviour
{
    // public GunScript gunScript; = change gun script to keycard script
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, playerCamera;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            //gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            //gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }


    private void Update()
    {
        // Check if player is in range and "E" is pressed to pickup the gun
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }
        // Drop the gun if its equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //making the weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //making the rigidbody kinematic and boxcollider a trigger

        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        // gunScript.enabled = true;
    }


    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // Set Parent to null
        transform.SetParent(null);

        //making the rigidbody not kinematic and boxcollider a trigger

        rb.isKinematic = false;
        coll.isTrigger = false;

        // Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Adding Force
        rb.AddForce(playerCamera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(playerCamera.up * dropUpwardForce, ForceMode.Impulse);

        //Adding random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //disable script
        // gunScript.enabled = false;


    }
}
