using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    [Header("Game Objects and Components")]
    public GameObject player;
    [SerializeField] private Rigidbody rigidBody;
    // used to detect player and to draw gizmo lines when selected
    [Header("Drone Stats")]
    [SerializeField] private float sightRange = 7.5f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private float angleToPlayer;
    // used only to detect player using vector3 math and raycasts
    [SerializeField] private Vector3 rayCastOrigin; // eye level of enemy character
    [SerializeField] private Vector3 directionToPlayer;
    [SerializeField] private Vector3 directionNormalized;
    [SerializeField] private float value;
    [SerializeField] private float maxMagnitudeValue;
    [SerializeField] private bool turnOn;

    [SerializeField] private Color sightRangeColour = Color.red;
    [SerializeField] private Color fieldOfViewColour = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");    
        rigidBody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        if (turnOn)
        {
            rigidBody.AddForce((player.transform.position - transform.position).normalized * value);

            if (rigidBody.velocity.magnitude >= maxMagnitudeValue)
            {
                rigidBody.AddForce(-rigidBody.velocity);
            }
        }
    }
    private void DetectPlayer()
    {
        if (Physics.Raycast(rayCastOrigin, directionToPlayer, out RaycastHit hit, sightRange))
        {
            if (hit.transform == player.transform)
            {
                Debug.DrawRay(rayCastOrigin, directionNormalized * sightRange, Color.red);

                DetectedPlayer();

            }
        }
    }
    private void DetectedPlayer()
    {
        Debug.Log(name + " detected Player!");
    }    
}
