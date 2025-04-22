using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemyNoiseDetection : EnemyDetection
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController;

    [Header("Enemy States")]
    [SerializeField] private bool outOfRange;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isJumping;

    [Header("Noise Detection Stats")]
    [SerializeField] private float hearWalkingRange = 5f;
    [SerializeField] private float hearSprintingAndJumpingRange = 15f;
    [SerializeField] private float walkSpeedThreshold = 1.5f;
    [SerializeField] private float sprintSpeedThreshold = 3f;
    [SerializeField] private float jumpThreshold = 1f;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;

    [Header("Player Position")]
    public Vector3 lastPlayerPosition;

    // method to detect player when they are moving or jumping within a given range 
    public override bool DetectPlayer()
    {
        if (characterController == null)
        {
            characterController = player.GetComponent<CharacterController>();
        }
        if (characterController == null) return false;

        distanceToPlayer = GetDistanceToPlayer();

        // when player is out of hearing range
        if (distanceToPlayer > hearSprintingAndJumpingRange)
        {
            verticalSpeed = 0f;
            horizontalSpeed = 0f;
            isWalking = false;
            isSprinting = false;
            isJumping = false;
            outOfRange = true;
            return false;
        }

        // when player is out of range
        if (outOfRange && lastPlayerPosition == Vector3.zero)
        {
            lastPlayerPosition = player.transform.position;
            outOfRange = false;
            return false;
        }

        Vector3 deltaPosition = (player.transform.position - lastPlayerPosition);
        Vector3 velocity = deltaPosition / Time.deltaTime;

        verticalSpeed = velocity.y;
        horizontalSpeed = new Vector3(velocity.x, 0f, velocity.z).magnitude;

        lastPlayerPosition = player.transform.position;

        if (horizontalSpeed < 0.1f)
        {
            horizontalSpeed = 0f;
        }

        // retrieves players movement speed and distance between player and enemy
        isWalking = horizontalSpeed >= walkSpeedThreshold && distanceToPlayer <= hearWalkingRange;
        isSprinting = (horizontalSpeed >= sprintSpeedThreshold && distanceToPlayer <= hearSprintingAndJumpingRange) && Input.GetKey(KeyCode.LeftShift);
        // retrieves player y axis and if they are off the ground
        isJumping = !characterController.isGrounded && verticalSpeed > jumpThreshold;

        if (player == null || characterController == null) return false;

        if (distanceToPlayer > hearSprintingAndJumpingRange) return false;

        // when player is in an enemys hearing distance for walking or sprinting and or jumping
        if ((isWalking || isSprinting || isJumping) && (distanceToPlayer <= hearSprintingAndJumpingRange))
        {
            lastPlayerPosition = player.transform.position;
            DetectedPlayer();

            return true;
        }


        return false;
    }

    // calls who heard player
    public override void DetectedPlayer()
    {
        Debug.Log($"Player heard by " + name);
    }

    // visualize hearing bounds when player walks or sprints
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearWalkingRange);
        Gizmos.DrawWireSphere(transform.position, hearSprintingAndJumpingRange);
    }
}


