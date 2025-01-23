using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TrackObjectSphere : MonoBehaviour
{
    public UnityEvent startMovementEvent;
    public GameObject user;
    public bool currentlyTracking = false;
    public SemicircleMovement semicircleMovement;

    // Start is called before the first frame update
    void Start()
    {
        var objRenderer = transform.GetComponent<Renderer>();
        objRenderer.material.SetColor("_Color", Color.blue);
    }

    void FixedUpdate()
    {
        var objRenderer = transform.GetComponent<Renderer>();
        int layerMask = 1 << 6; // bitmask to cast rays only against colliders in layer 6 ('Raycast').

        RaycastHit hit;
        if (Physics.Raycast(user.transform.position, user.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            // Debug.Log("Time: " + Time.time +
            //             " Cube X Position: " + obj.transform.position.x  + 
            //             " Cube Y Position: " + obj.transform.position.y +
            //             " User Gaze X Position:" + hit.point.x +
            //             " User Gaze Y Position:" + hit.point.y);
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (!semicircleMovement.isMoving && !currentlyTracking)
            {
                startMovementEvent.Invoke();
            }
            objRenderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            objRenderer.material.SetColor("_Color", Color.red);
        }
    }

    public void StartTracking()
    {
        currentlyTracking = true;
    }

    public void EndTracking()
    {
        currentlyTracking = false;
    }

    public void OnDestroy()
    {
        currentlyTracking = false;
    }
}
