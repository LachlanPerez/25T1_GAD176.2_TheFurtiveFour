using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecurityCamera
    {
    public class CameraDetection : MonoBehaviour
    {
        [SerializeField] private float visionRange = 10f;
        [SerializeField] private float visionAngle = 45f;

        private LayerMask obstacleMask;  // Mask to detect obstacles between camera and player
        [SerializeField] protected Transform player;

        // Update is called once per frame
        void Update()
        {
            // Calculate the direction from the camera to the player
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;


            if (distanceToPlayer <= visionRange)
            {
                // Calculate the angle between the camera's forward direction and the direction to the player
                float angle = Vector3.Angle(transform.forward, directionToPlayer);

                if (angle <= visionAngle / 2f)
                {
                    // Check if there's a clear line of sight
                    if (!Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask)) // Detects if player has been detected
                    {
                        Debug.Log("Player detected by raycast!");
                    }
                }
            }

            // Draw debug ray in Scene view to visualize the line to the player
            Debug.DrawRay(transform.position, directionToPlayer.normalized * distanceToPlayer, Color.yellow);
        }

        private void OnDrawGizmosSelected()
        {
            if (player == null) return; // null check

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionRange); // Draw a wireframe sphere to visualize the camera's vision range

            Vector3 forward = transform.TransformDirection(Vector3.forward); // Get the camera's forward direction

            // Calculate boundaries of the vision cone
            Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;
            Vector3 rightBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;

            // Draw rays showing the boundaries of the vision cone
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, leftBoundary * visionRange);
            Gizmos.DrawRay(transform.position, rightBoundary * visionRange);

        }
    }
}