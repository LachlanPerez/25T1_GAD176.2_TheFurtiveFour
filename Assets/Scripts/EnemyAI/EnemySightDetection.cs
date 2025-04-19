using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemySightDetection : EnemyDetection
{

    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private bool playerInSightRange = false;
    [SerializeField] private bool playerInFieldOfView = false;

    [SerializeField] private Vector3 rayCastOrigin; // eye level of enemy character
    [SerializeField] private Vector3 directionToPlayer;
    [SerializeField] private Vector3 directionNormalized;
    [SerializeField] private float eyeHeight = 1.7f;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float angleToPlayer;

    public override bool DetectPlayer()
    {
        directionToPlayer = player.transform.position - transform.position;

        directionToPlayer.y = 0;

        directionToPlayer.Normalize();
        
        angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= fieldOfView / 2f && directionToPlayer.magnitude <= sightRange)
        {
            directionNormalized = directionToPlayer.normalized;

            rayCastOrigin = transform.position + Vector3.up * eyeHeight;

            if (Physics.Raycast(rayCastOrigin, directionToPlayer, out RaycastHit hit, sightRange))
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
