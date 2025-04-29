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
    public static DroneAI instance;
    public bool playerDetected = false;

    [SerializeField] private float detectionRadius = 7.5f;
    [SerializeField] private float angleToPlayer;
    [SerializeField] private float forceValue = 4f;
    [SerializeField] private float maxMagnitudeValue = 8f;
    [SerializeField] private bool turnOnDrone;
    [SerializeField] private float drag = 1f;

    // used only to detect player using vector3 math and raycasts
    [SerializeField] private Vector3 rayCastOrigin; // bottom of enemy drone
    [SerializeField] private Vector3 directionToPlayer;
    [SerializeField] private Vector3 directionNormalized;


    [SerializeField] private Color detectionRadiusColor = Color.red;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");    
        rigidBody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerDetected)
        {
            rigidBody.AddForce((player.transform.position - transform.position).normalized * forceValue);

            if (rigidBody.velocity.magnitude >= maxMagnitudeValue)
            {
                rigidBody.AddForce(-rigidBody.velocity);
            }


        }

        ApplyDrag();
    }
    private void ApplyDrag()
    {
        
        rigidBody.velocity *= (1f - drag * Time.fixedDeltaTime);
    }
    private void DetectPlayer()
    {
        if (Physics.Raycast(rayCastOrigin, directionToPlayer, out RaycastHit hit, detectionRadius))
        {
            if (hit.transform == player.transform)
            {
                Debug.DrawRay(rayCastOrigin, directionNormalized * detectionRadius, Color.red);

                DetectedPlayer();

            }
        }
    }
    private void DetectedPlayer()
    {
        Debug.Log(name + " detected Player!");
    }    
}
