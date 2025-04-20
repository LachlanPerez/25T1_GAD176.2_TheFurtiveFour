using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheFurtiveFour.EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        public GameObject player;
        public bool isOnAlert;
        public bool isOnCautious;

        [SerializeField] private TextMeshProUGUI enemyNameText;
        [SerializeField] private EnemySightDetection sightDetection;
        [SerializeField] private EnemyNoiseDetection noiseDetection;

        [SerializeField] private bool isRotating;
        [SerializeField] private bool isMoving;
        [SerializeField] private float walkingSpeed = 2.5f;
        [SerializeField] private float sprintingSpeed = 5f;
        [SerializeField] private float stoppingDistance = 2f;
        [SerializeField] private float waitTimeAtPlayerLastKnownLocation = 3f;
        
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            sightDetection = transform.GetComponent<EnemySightDetection>();
            noiseDetection = transform.GetComponent<EnemyNoiseDetection>();
            enemyNameText = GetComponentInChildren<TextMeshProUGUI>();
            enemyNameText.text = name;

            if (player != null)
            {
                sightDetection.player = player;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyNameText != null)
            {
                FacePlayer();
            }

            if (sightDetection != null && sightDetection.DetectPlayer())
            {
                // do move
                MoveToPlayersLastKnownLocation(player.transform.position, stoppingDistance);
            }

            if (noiseDetection != null && noiseDetection.DetectPlayer() && !isMoving && !isRotating)
            {
                // do investigate
                StartCoroutine(RotateThenMoveToLastKnownPosition());
            }
        }
        private void FacePlayer()
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            enemyNameText.transform.rotation = Quaternion.LookRotation(-directionToPlayer);
        }
        private void ChasePlayer()
        {

        }
        private void FindPlayer()
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;

            directionToPlayer.y = 0;

            directionToPlayer.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);

            if (noiseDetection.GetDistanceToPlayer() > stoppingDistance)
            {
                MoveToPlayersLastKnownLocation(noiseDetection.lastPlayerPosition, stoppingDistance);
            }
            else
            {
                Debug.Log("Enemy reached Player's last known location");
            }

        }
        private void MoveToPlayersLastKnownLocation(Vector3 target, float stopDistance)
        {
            Vector3 direction = (target - transform.position);
            direction.y = 0f;

            if (direction.magnitude <= stopDistance) 
                return;

            Vector3 move = direction.normalized * walkingSpeed * Time.deltaTime;
            transform.position += move;
        }
        private IEnumerator IdleTimeAfterMoving()
        {
            Debug.Log("Enemy observes Players last known location");

            yield return new WaitForSeconds(waitTimeAtPlayerLastKnownLocation);
            Debug.Log("Enemy finished observing area");
        }
        private IEnumerator WaitToMoveAgain()
        {
            yield return new WaitForSeconds(1f);
        }
        private IEnumerator RotateThenMoveToLastKnownPosition()
        {
            isRotating = true;

            Vector3 flatTarget = new Vector3(noiseDetection.lastPlayerPosition.x, transform.position.y, noiseDetection.lastPlayerPosition.z);
            Vector3 direction = (flatTarget - transform.position).normalized;

            Debug.Log(name + " rotating");

            while (true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, noiseDetection.turnSpeed * Time.deltaTime);

                float angle = Quaternion.Angle(transform.rotation, targetRotation);
                if (angle < 1) break;

                yield return null;
            }

            isRotating = false;
            isMoving = true;

            Debug.Log(name + " moving");

            while (Vector3.Distance(transform.position, noiseDetection.lastPlayerPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (noiseDetection.lastPlayerPosition - transform.position).normalized;
                moveDirection.y = 0;
                transform.position += moveDirection * walkingSpeed * Time.deltaTime;

                if (moveDirection != Vector3.zero)
                {
                    Quaternion moveRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, Time.deltaTime * noiseDetection.turnSpeed);

                }
                yield return null; 
            }

            Debug.Log(name + " reached last heard location");
            isMoving = false;
        }
    }
}

