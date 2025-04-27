using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SecurityCamera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float rotationalSpeed = 45f;
        [SerializeField] private float maxRotation = 45f;

        private float startingY;
        private bool rotatingRight = true;


        // Start is called before the first frame update
        void Start()
        {
            // Store the initial Y-axis rotation of the camera when the scene starts.
            startingY = transform.localEulerAngles.y;
        }

        // Update is called once per frame
        void Update()
        {
            // Calculate an oscillating offset angle using a sine wave
            float angleOffset = Mathf.Sin(Time.time * rotationalSpeed * Mathf.Deg2Rad) * maxRotation;


            float newY = startingY + angleOffset;   // Combine the offset with the starting Y rotation to get the new Y rotation angle

            Vector3 currentEuler = transform.localEulerAngles;// Get the current rotation of the object

            // Apply the new Y rotation while keeping X and Z rotations unchanged
            transform.localEulerAngles = new Vector3(currentEuler.x, newY, currentEuler.z);

        }
    }
}