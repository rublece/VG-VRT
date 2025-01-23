using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackOnPlane : ITracker
{
    public GameObject user;
    public bool currentlyTracking = false;
    public LogToCSV logger;
    public Exercise exercise;
    public Vector2 userGaze; // Used for performance tracking
    public Transform cube; // Used for performance tracking

    public override Vector2 CurrentGaze
    {
        get
        {
            return userGaze;
        }
    }

    public override Vector2 CurrentObj
    {
        get
        {
            return new Vector2(cube.position.x, cube.position.y);
        }
    }

    public override bool CurrentlyTracking
    {
        get
        {
            return currentlyTracking;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyTracking)
        {
            int layerMask = 1 << 7; // bitmask to cast rays only against colliders in layer 7 ('Plane').
            RaycastHit hit;

            Physics.Raycast(user.transform.position, user.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);

            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            // Debug.Log("Time: " + Time.time +
            //             " Cube X Position: " + transform.position.x +
            //             " Cube Y Position: " + transform.position.y +
            //             " User Gaze X Position: " + hit.point.x +
            //             " User Gaze Y Position: " + hit.point.y);

            userGaze = new Vector2(hit.point.x, hit.point.y);
            Debug.Log("User Gazeeeeeeee: " + userGaze);

            // this script calls log method for gaze events
            logger.LogGaze(Time.time, Time.time - exercise.timeAnchor, transform.position.x, transform.position.y,
                hit.point.x, hit.point.y, user.transform.position, user.transform.eulerAngles);
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

    void OnDestroy()
    {
        currentlyTracking = false;
    }
}
