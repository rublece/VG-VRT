using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArchMovement : MonoBehaviour
{
    [Tooltip("Event to log a half or full rep.")]
    public UnityEvent<string> logEvent;

    [Tooltip("Event for when a new full rep starts.")]
    public UnityEvent newRepEvent;

    [Tooltip("Event to play a sound.")]
    public UnityEvent playSoundEvent;

    [Tooltip("Event to increment the rep counter.")]
    public UnityEvent incrementRepEvent;

    [Tooltip("True if the object is currently moving.")]
    public bool isMoving = false;

    [Tooltip("True if the object moving to the right, false if the object is moving to the left.")]
    public bool directionRight = true;

    [Tooltip("The value of the object's progress from one side to the other. Goes between 0 (left-side) to 1 (right-side).")]
    public float progress = 0;

    [Tooltip("The object's movement speed. Changes based on the difficulty level.")]
    public float movementSpeed = 0.5f;

    [Tooltip("The width of the parabola the object travels in.")]
    public float width = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(0, 6); // ignore collisions between layer 0 and layer 6
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // If direction has reached other side, change to opposite direction
            if (progress > 1) // reached half-rep
            {
                directionRight = false;
                progress = 1;
                isMoving = false;
                logEvent.Invoke("A_End");
            }
            else if (progress < 0) // reached full-rep
            {
                directionRight = true;
                progress = 0;
                isMoving = false;
                logEvent.Invoke("B_End");
                incrementRepEvent.Invoke();
            }

            // Move object in correct direction
            if (directionRight)
            {
                progress += Time.deltaTime * movementSpeed;
            }
            else
            {
                progress -= Time.deltaTime * movementSpeed;
            }
            
            // Update the object's position
            float x = progress * width;
            float y = Mathf.Sin(progress * Mathf.PI);
            transform.localPosition = new Vector3(x, y, 0f);
        }
    }

    public void StartRep()
    {
        Debug.Log("START REPAROOOOO");
        isMoving = true;
        if (directionRight)
        {
            logEvent.Invoke("A_Start");
            newRepEvent.Invoke();
        } 
        else
        {
            logEvent.Invoke("B_Start");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(!isMoving && collider.gameObject.tag == "Player")
        {
            StartRep();
            playSoundEvent.Invoke();
        }
    }

    public void SetSpeedEasy()
    {
        movementSpeed = 0.25f;
    }

    public void SetSpeedMedium()
    {
        movementSpeed = 0.5f;
    }

    public void SetSpeedDifficult()
    {
        movementSpeed = 0.75f;
    }
}
