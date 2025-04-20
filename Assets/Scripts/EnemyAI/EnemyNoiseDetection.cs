using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemyNoiseDetection : EnemyDetection
{
    [SerializeField] private float hearWalkingRange = 5f;
    [SerializeField] private float hearSprintingAndJumpingRange = 15f;
    [SerializeField] private float walkSpeedThreshold = 1.5f;
    [SerializeField] private float sprintSpeedThreshold = 3f;
    [SerializeField] private float jumpThreshold = 1f;

    public Vector3 lastPlayerPosition;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isJumping;
    public override bool DetectPlayer()
    {
        if (characterController == null)
        {
            characterController = player.GetComponent<CharacterController>();
        }
        if (characterController == null) return false;

        distanceToPlayer = GetDistanceToPlayer();

        if (distanceToPlayer > hearSprintingAndJumpingRange)
        {
            verticalSpeed = 0f;
            horizontalSpeed = 0f;
            return false;
        }

        if (lastPlayerPosition == Vector3.zero)
        {
            lastPlayerPosition = player.transform.position;
            return false;
        }

        Vector3 deltaPosition = (player.transform.position - lastPlayerPosition);
        Vector3 velocity = deltaPosition / Time.deltaTime;

        verticalSpeed = velocity.y;
        horizontalSpeed = new Vector3(velocity.x, 0f, velocity.z).magnitude;

        lastPlayerPosition = player.transform.position;

        isWalking = horizontalSpeed >= walkSpeedThreshold && distanceToPlayer <= hearWalkingRange;
        isSprinting = horizontalSpeed >= sprintSpeedThreshold && distanceToPlayer <= hearSprintingAndJumpingRange;
        isJumping = !characterController.isGrounded && verticalSpeed > jumpThreshold;



        if (player == null || characterController == null) return false;



        if (distanceToPlayer > hearSprintingAndJumpingRange) return false;





        if ((isWalking || isSprinting || isJumping) && (distanceToPlayer <= hearSprintingAndJumpingRange))
        {
            Debug.Log("Player Noise Detected");

            if (isWalking)
            {
                Debug.Log("walking");
            }
            if (isSprinting)
            {
                Debug.Log("sprinting");
            }
            if (isJumping)
            {
                Debug.Log("jumping");
            }
            return true;
        }


        return false;
    }
    public override void DetectedPlayer()
    {
        Debug.Log($"Player heard by " + name);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearWalkingRange);
        Gizmos.DrawWireSphere(transform.position, hearSprintingAndJumpingRange);
    }
}


