using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SemicircleMovement : MonoBehaviour
{
    [Tooltip("Event to log a half or full rep.")]
    public UnityEvent<string> logEvent;

    [Tooltip("Event for when a new full rep starts.")]
    public UnityEvent newRepEvent;

    [Tooltip("Event to increment the rep counter.")]
    public UnityEvent incrementRepEvent;

    [Tooltip("True if the object is currently moving.")]
    public bool isMoving = false;

    [Tooltip("True if the object moving to the right, false if the object is moving to the left.")]
    public bool directionRight = false;

    [Tooltip("The maximum angle to rotate the object.")]
    public float maxAngle = 180f;

    [Tooltip("The current accumulated angle of the object.")]
    private float currentAngle = 0f;

    [Tooltip("The object's rotation speed in degrees per second. Changes based on the difficulty level.")]
    public float rotationSpeed = 30f;

    [Tooltip("The target to rotate the object around.")]
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // If the current angle is greater than or equal to the max angle, reverse the rotation
            if (currentAngle >= maxAngle && directionRight)
            {
                directionRight = false;
                logEvent.Invoke("B_Start");
            }

            // If the current angle is less than or equal to 0, reverse the rotation
            if (currentAngle <= 0f && !directionRight)
            {
                directionRight = true;
                incrementRepEvent.Invoke();
                logEvent.Invoke("A_Start");
                newRepEvent.Invoke();
            }

            float rotationStep = Time.deltaTime * rotationSpeed;

            // Move object in correct direction
            if (directionRight)
            {
                transform.RotateAround(target.position, Vector3.up, rotationStep);
                currentAngle += rotationStep;
            }
            else
            {
                transform.RotateAround(target.position, Vector3.up, -rotationStep);
                currentAngle -= rotationStep;
            }
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    public void OnDestroy()
    {
        isMoving = false;
    }

    public void SetSpeedEasy()
    {
        rotationSpeed = 30f;
    }

    public void SetSpeedMedium()
    {
        rotationSpeed = 60f;
    }

    public void SetSpeedDifficult()
    {
        rotationSpeed = 90f;
    }
}
