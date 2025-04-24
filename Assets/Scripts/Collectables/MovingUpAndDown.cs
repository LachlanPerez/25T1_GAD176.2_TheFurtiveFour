using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpAndDown : MonoBehaviour
{


    public float rotationSpeed = 100f;
    public float moveSpeed = 1.25f;
    public float moveHeight = 0.15f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotates the object continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Move the object up and down slowly while the object is rotating
        float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }


}
