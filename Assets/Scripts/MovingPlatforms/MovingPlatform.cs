using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed;
    public float waitTime;
    public bool isMoving { get; private set; }
    [SerializeField] private bool checkIsMoving;

    private Vector3 target;
    private Vector3 lastPosition;

    public Vector3 deltaMovement { get; private set; }

    void Start()
    {
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
        while (true)
        {
            isMoving = true;

            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            isMoving = false;
            yield return new WaitForSeconds(waitTime);
            target = target == pointA ? pointB : pointA;
        }
    }
}
