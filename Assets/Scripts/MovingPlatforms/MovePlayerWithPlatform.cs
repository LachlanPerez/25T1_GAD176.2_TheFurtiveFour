using StarterAssets;
using UnityEngine;
namespace TheFurtiveFour.MovingPlatforms
{
    public class MovePlayerWithPlatform : MonoBehaviour
    {
        [SerializeField] private Transform currentPlatform;
        private Vector3 lastPlatformPosition;
        [SerializeField] private MovingPlatform movingPlatform;
        private ThirdPersonController thirdPersonController;
        private Animator animator;
        // for comments write WHAT it is doing and WHY did you choose that type/function
        private void Start()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
            animator = GetComponent<Animator>();
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                if (currentPlatform != hit.collider.transform)
                {
                    currentPlatform = hit.collider.transform;
                    movingPlatform = currentPlatform.GetComponent<MovingPlatform>();
                    lastPlatformPosition = currentPlatform.position;
                }
            }
            else
            {
                currentPlatform = null;
            }
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentPlatform = null;
                thirdPersonController.enabled = true;
                Debug.Log("Jumped Off Platform");
            }
        }

        private void LateUpdate()
        {
            if (currentPlatform != null && movingPlatform != null)
            {
                if (movingPlatform.isMoving)
                {
                    thirdPersonController.enabled = false;
                    animator.SetFloat("Speed", 0f);
                    animator.SetFloat("MotionSpeed", 0f);

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        currentPlatform = null;
                        thirdPersonController.enabled = true;
                    }
                }
                else if (!movingPlatform.isMoving)
                {
                    thirdPersonController.enabled = true;
                }

                transform.position += movingPlatform.deltaMovement;
            }


        }


    }

}
