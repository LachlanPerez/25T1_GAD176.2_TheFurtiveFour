using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemySightDetection : EnemyDetection
{
    public LayerMask layerMask;

    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private bool playerInSightRange = false;
    [SerializeField] private bool playerInFieldOfView = false;

    [SerializeField] private Vector3 rayCastOrigin; // eye level of enemy character
    [SerializeField] private Vector3 directionToPlayer;
    [SerializeField] private Vector3 directionNormalized;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float angleToPlayer;

    public override bool DetectPlayer()
    {


        directionToPlayer = player.transform.position - transform.position;

        distanceToPlayer = directionToPlayer.magnitude;
        
        angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= sightRange && angleToPlayer <= fieldOfView / 2f)
        {
            directionNormalized = directionToPlayer.normalized;

            rayCastOrigin = transform.position + Vector3.up * 1.7f;

            if (Physics.Raycast(rayCastOrigin, directionNormalized, out RaycastHit hit, sightRange, ~layerMask))
            {
                if (hit.transform == player.transform)
                {
                    Debug.DrawRay(rayCastOrigin, directionNormalized * sightRange, Color.red);
                    DetectedPlayer();

                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

                    return true;
                }
            }

        }

        return false;
    }
    public override void DetectedPlayer()
    {
        Debug.Log("Player detected by" + name);
    }
}
