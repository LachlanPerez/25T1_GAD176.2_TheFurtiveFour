using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float visionAngle = 45f;

    private LayerMask obstacleMask;
    [SerializeField] protected Transform player;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;


        if (distanceToPlayer <= visionRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle <= visionAngle / 2f)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask))
                {
                    Debug.Log("Player detected by raycast!");
                }
            }
        }

        Debug.DrawRay(transform.position, directionToPlayer.normalized * distanceToPlayer, Color.yellow);
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftBoundary * visionRange);
        Gizmos.DrawRay(transform.position, rightBoundary * visionRange);

    }
}
