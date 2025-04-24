using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace TheFurtiveFour.EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {

        [Header("Enemy States")]
        public EnemyState currentState = EnemyState.Patrolling; // default enemy state
        public enum EnemyState { Patrolling, Investigating, Chasing }
        
        private bool isRotating = false;
        private bool isMoving = false;

        [Header("Game Objects")]
        public GameObject player;

        [Header("Scripts")]
        [SerializeField] private EnemySightDetection sightDetection;
        [SerializeField] private EnemyNoiseDetection noiseDetection;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI enemyNameText;

        [Header("Enemy Stats")]
        [SerializeField] private float walkingSpeed = 2.5f;
        [SerializeField] private float sprintingSpeed = 5f;
        [SerializeField] private float stoppingDistance = 2f;
        [SerializeField] private float waitAtLocation = 2f;
        [SerializeField] private float loseMemoryDuration = 5f;
        private float lastTimeSawPlayer = Mathf.NegativeInfinity; // used as a negative number constant 

        [Header("Vector Points")]
        [SerializeField] private Vector3 pointA;
        [SerializeField] private Vector3 pointB;
        private Vector3 patrolTarget;
        private Vector3 lastKnownLocation; // players last known location



        private void Start()
        {
            // gets components and objects
            player = GameObject.FindWithTag("Player");
            sightDetection = GetComponent<EnemySightDetection>();
            noiseDetection = GetComponent<EnemyNoiseDetection>();
            enemyNameText = GetComponentInChildren<TextMeshProUGUI>();
            enemyNameText.text = name;

            // sets default states
            patrolTarget = pointA;
            currentState = EnemyState.Patrolling;

            if (player != null)
            {
                sightDetection.player = player;
            }

            StartCoroutine(PatrolRoutine());
        }

        private void Update()
        {
            // used for when player is detected by sight
            FacePlayer();

            #region Enemy Sees Player 
            if (sightDetection.DetectPlayer())
            {
                lastKnownLocation = player.transform.position;
                lastTimeSawPlayer = Time.time;

                // sets state to chasing player
                if (currentState != EnemyState.Chasing)
                {
                    StopAllCoroutines();
                    currentState = EnemyState.Chasing;
                }
            }
            #endregion

            #region enemy hears player and is not chasing them
            

            if (noiseDetection.DetectPlayer() && currentState != EnemyState.Chasing)
            {
                Vector3 newNoisePosition = noiseDetection.lastPlayerPosition;

                // moves to new position is new noise is made and heard
                if (Vector3.Distance(newNoisePosition, lastKnownLocation) > 1f)
                {
                    lastKnownLocation = newNoisePosition;

                    StopAllCoroutines();
                    currentState = EnemyState.Investigating;
                    StartCoroutine(RotateThenMoveToLastKnownPosition());
                }
            }

            #endregion

            // used to switch between three enemy states, love enums <3
            switch (currentState)
            {
                case EnemyState.Chasing:
                    MoveTo(lastKnownLocation, sprintingSpeed);

                    if (Time.time - lastTimeSawPlayer > loseMemoryDuration)
                    {
                        currentState = EnemyState.Patrolling;
                        StartCoroutine(PatrolRoutine());
                    }
                    break;

                case EnemyState.Investigating:

                    break;
            }
        }

        // used to move to player or last heard noise
        
        private void MoveTo(Vector3 target, float speed)
        {
            Vector3 direction = (target - transform.position);
            direction.y = 0;

            if (direction.magnitude <= stoppingDistance) return;

            direction.Normalize();

            Vector3 moveDirection = AvoidObstacles(direction);

            transform.position += moveDirection * speed * Time.deltaTime;

            if (moveDirection != Vector3.zero)
            {
                Quaternion moveRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, Time.deltaTime * 5f);
            }
        }

        // used to avoid obstacles when moving to a location or to the player
        private Vector3 AvoidObstacles(Vector3 desiredDirection)
        {
            float rayDistance = 2f;
            float avoidStrength = 0.75f;
            Vector3 origin = transform.position + Vector3.up * 0.5f;

            
            if (!Physics.Raycast(origin, desiredDirection, rayDistance))
                return desiredDirection;

            float[] angles = { 30f, -30f, 60f, -60f, 90f, -90f };
            foreach (float angle in angles)
            {
                Vector3 newDir = Quaternion.AngleAxis(angle, Vector3.up) * desiredDirection;
                if (!Physics.Raycast(origin, newDir, rayDistance * 0.75f))
                {
                    return Vector3.Lerp(desiredDirection, newDir, avoidStrength);
                }
            }


            if (Physics.Raycast(origin, desiredDirection, out RaycastHit hit, rayDistance))
            {
                Vector3 wallNormal = hit.normal;
                Vector3 slideDirection = Vector3.Cross(wallNormal, Vector3.up).normalized;


                return (desiredDirection + slideDirection * 0.5f).normalized;
            }


            return desiredDirection * 0.5f;
        }

        // used to face player
        private void FacePlayer()
        {
            if (!player) return;

            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;
            enemyNameText.transform.rotation = Quaternion.LookRotation(-directionToPlayer);
        }

        // used for enemies to walk back and forth patrol spots
        private IEnumerator PatrolRoutine()
        {
            while (currentState == EnemyState.Patrolling)
            {
                while (Vector3.Distance(transform.position, patrolTarget) > stoppingDistance)
                {
                    MoveTo(patrolTarget, walkingSpeed);
                    yield return null;
                }

                // waits at one end first then moves to the next
                yield return new WaitForSeconds(waitAtLocation);

                patrolTarget = patrolTarget == pointA ? pointB : pointA;
            }
        }

        // used to rotaty enemy aligning with the player last known position then moving to that location using MoveTo
        private IEnumerator RotateThenMoveToLastKnownPosition()
        {
            isRotating = true;

            Vector3 flatTarget = new Vector3(lastKnownLocation.x, transform.position.y, lastKnownLocation.z);
            Vector3 direction = (flatTarget - transform.position).normalized;

            while (true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, noiseDetection.turnSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
                    break;
                yield return null;
            }

            isRotating = false;
            isMoving = true;

            float stuckTimer = 0f;
            Vector3 lastPosition = transform.position;

            while (Vector3.Distance(transform.position, lastKnownLocation) > stoppingDistance)
            {
                MoveTo(lastKnownLocation, walkingSpeed);
                yield return null;


                float movedDistance = Vector3.Distance(transform.position, lastPosition);
                if (movedDistance < 0.05f)
                {
                    stuckTimer += Time.deltaTime;
                    if (stuckTimer > 3f)
                    {
                        Debug.LogWarning(name + " got stuck investigating. Returning to safe position.");
                        break;
                    }
                }
                else
                {
                    stuckTimer = 0f; // resets stuck timer
                }

                lastPosition = transform.position;
            }

            isMoving = false;

            yield return new WaitForSeconds(waitAtLocation);

            // restarts patrolling state
            currentState = EnemyState.Patrolling;
            StartCoroutine(PatrolRoutine());
        }

        // used to visualise pathfinding player location
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Vector3 origin = transform.position + Vector3.up * 0.5f;
            Vector3 forward = transform.forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(origin, forward * 2f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(origin, Quaternion.AngleAxis(45, Vector3.up) * forward * 1f);
            Gizmos.DrawRay(origin, Quaternion.AngleAxis(-45, Vector3.up) * forward * 1f);
        }
    }
}
