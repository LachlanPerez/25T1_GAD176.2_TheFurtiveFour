using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float rotationalSpeed = 45f;
    [SerializeField] private float maxRotation = 45f;

    private float startingY;
    private bool rotatingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float angleOffset = Mathf.Sin(Time.time * rotationalSpeed * Mathf.Deg2Rad) * maxRotation;
        float newY = startingY + angleOffset;

        Vector3 currentEuler = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(currentEuler.x, newY, currentEuler.z);
        
    }
}
