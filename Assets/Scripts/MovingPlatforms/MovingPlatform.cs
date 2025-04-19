using UnityEngine;
using System.Collections;
namespace TheFurtiveFour.MovingPlatforms
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public float speed;
        public float waitTime;
        public bool isMoving { get; private set; }
        [SerializeField] private bool checkIsMoving;

        public Vector3 deltaMovement { get; private set; }
        private Vector3 target; // target destination for moving platform
        private Vector3 lastPosition; 


        void Start()
        {
            // start moving platform, set speed and idle time on both point a and b for platform
            isMoving = false;
            speed = 2f;
            waitTime = 5f;
            target = pointB;
            lastPosition = transform.position;
            StartCoroutine(MovePlatform());
        }

        void Update()
        {
            deltaMovement = transform.position - lastPosition;
            lastPosition = transform.position;
            checkIsMoving = isMoving;
        }

        private IEnumerator MovePlatform()
        {
            // used to loop move platform function in start
            while (true)
            {
                isMoving = true;
                Debug.Log("Platform Moving");

                while (Vector3.Distance(transform.position, target) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                    yield return null;
                }

                isMoving = false;
                Debug.Log("Platform Stopped");
                // waits for the amount of time the moving platform stops at a point to continue
                yield return new WaitForSeconds(waitTime);
                target = target == pointA ? pointB : pointA;
            }
        }
    }

}
