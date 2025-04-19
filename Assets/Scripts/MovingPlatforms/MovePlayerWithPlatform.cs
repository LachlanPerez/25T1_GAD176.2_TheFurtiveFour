using StarterAssets;
using UnityEngine;
namespace TheFurtiveFour.MovingPlatforms
{
    public class MovePlayerWithPlatform : MonoBehaviour
    {
        [SerializeField] private Transform currentPlatform; // the current platform the player is on
        private Vector3 lastPlatformPosition; // grabs the current platforms latest x,y,z axes
        [SerializeField] private MovingPlatform movingPlatform; // calls the moving platform script
        private ThirdPersonController thirdPersonController; // calls the thirdpersoncontroller script
        private Animator animator; // grabs the player character animator 

        private void Start()
        {
            // gets player components
            thirdPersonController = GetComponent<ThirdPersonController>();
            animator = GetComponent<Animator>();
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // when player collides with moving platform game object with movingplatform tag
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                if (currentPlatform != hit.collider.transform)
                {
                    // gets moving platform transform, script component and position
                    currentPlatform = hit.collider.transform;
                    movingPlatform = currentPlatform.GetComponent<MovingPlatform>();
                    lastPlatformPosition = currentPlatform.position;
                    Debug.Log("Landed on Moving Platform");
                }
            }
            else
            {
                currentPlatform = null;
            }
        }
        private void Update()
        {
            // allows for the player to jump off platform when pressing space on platform
            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentPlatform = null;
                thirdPersonController.enabled = true;
                Debug.Log("Jumped Off Platform");
            }
        }

        private void LateUpdate()
        {
            // allows for the player character to move with the moving platform
            if (currentPlatform != null && movingPlatform != null)
            {
                if (movingPlatform.isMoving)
                {
                    // must allow the thirdperson script to disable otherwise player will not move with the platform
                    thirdPersonController.enabled = false;
                    // sets player to idle anim or shows player standing still while moving on platform
                    animator.SetFloat("Speed", 0f);
                    animator.SetFloat("MotionSpeed", 0f);

                    // animator and third person script return to normal if player jumps off platform
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        currentPlatform = null;
                        thirdPersonController.enabled = true;
                    }
                }
                else if (!movingPlatform.isMoving)
                {
                    // thirdpersoncontroller script enables once platform stops moving
                    thirdPersonController.enabled = true;
                }

                transform.position += movingPlatform.deltaMovement;
            }


        }


    }

}
