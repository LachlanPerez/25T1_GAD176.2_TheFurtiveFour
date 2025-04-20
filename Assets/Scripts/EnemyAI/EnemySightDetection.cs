using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;
using static UnityEngine.UI.Image;

public class EnemySightDetection : EnemyDetection
{
    // used to detect player and to draw gizmo lines when selected
    [SerializeField] private float sightRange = 7.5f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private Color sightRangeColour = Color.red;
    [SerializeField] private Color fieldOfViewColour = Color.yellow;

    // used only to detect player using vector3 math and raycasts
    [SerializeField] private Vector3 rayCastOrigin; // eye level of enemy character
    [SerializeField] private Vector3 directionToPlayer;
    [SerializeField] private Vector3 directionNormalized;
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

            rayCastOrigin = transform.GetChild(0).transform.position;

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
        Debug.Log("Player seen by " + name);
    }
    public void OnDrawGizmosSelected() // used to visualise field of view and sight range in scene view
    {
        
        /// draws field of view cone

        Gizmos.color = fieldOfViewColour;
        Vector3 forward = transform.forward;
        float halfAngle = fieldOfView / 2f;
        float coneAngleStep = fieldOfView / 30;
        // draws ray from start till end of sight range
        Gizmos.DrawRay(transform.position, forward * sightRange); 
        Vector3 conePreviousPoint = transform.position + Quaternion.Euler(0, -halfAngle, 0) * forward * sightRange;
        // used to create and draw a half circle to represent enemy field of view
        for (int i = 1; i <= 30; i++)
        {
            float coneAngle = -halfAngle + coneAngleStep * i;
            Vector3 coneNextPoint = transform.position + Quaternion.Euler(0, coneAngle, 0) * forward * sightRange;
            Gizmos.DrawLine(conePreviousPoint, coneNextPoint);
            conePreviousPoint = coneNextPoint;
        }
        // used to only draw the left middle and right lines
        Vector3 leftDirection = Quaternion.Euler(0, -halfAngle, 0) * forward;
        Vector3 rightDirection = Quaternion.Euler(0, halfAngle, 0) * forward;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * sightRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * sightRange);

        /// draws sight range radius

        Gizmos.color = sightRangeColour;
        float angleStep = 360 / 64;
        Vector3 previousPoint = transform.position + Quaternion.Euler(0, 0, 0) * Vector3.forward * sightRange;
        // used to create and draw a circle to represent max enemy sight range
        for (int i = 1; i <= 64;  i++)
        {
            float angle = i * angleStep;
            Vector3 nextPoint = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * sightRange;
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        
    }
}
