using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Tracks the user's gaze and an optional object's position on a cylinder.
/// Ensure that the object's movement is aligned with the cylinder.
/// If the object is not set, the script will only track the user's gaze.
/// </summary>
public class TrackOnCylinder : ITracker
{
    [Tooltip("The user's transform (e.g. the camera).")]
    public Transform userTransform;

    [Tooltip("The transform at the center of the cylinder.")]
    public Transform center;

    [Tooltip("(Optional) The moving object to track.")]
    public Transform trackedObject;

    [Space(10)]

    [Tooltip("The layer mask for the collision detection.")]
    public LayerMask collisionMask;

    [Tooltip("The maximum distance to check for collisions.")]
    public float maxDistance = 10f;

    [Tooltip("True when the tracked object is moving.")]
    public bool currentlyTracking = false;

    [Tooltip("The logger object used to log data.")]
    public LogToCSV logger;

    [Tooltip("The exercise script which stores the time the most recent rep started.")]
    public Exercise exercise;

    [Tooltip("The value of the user's gaze while being tracked.")]
    public Vector2 userGaze;

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
            return new Vector2(trackedObject.position.x, trackedObject.position.z);
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
        // If no transform set, assume we are attached to the cylinder
        if (center == null)
        {
            center = transform;
        }
    }

    void FixedUpdate()
    {
        if (currentlyTracking)
        {
            Vector3 startPosition = userTransform.position;
            Vector3 rayDirection = userTransform.forward;

            Vector3 hitPoint;
            RaycastHit hit;
            if (Physics.Raycast(startPosition, rayDirection, out hit, maxDistance, collisionMask))
            {
                hitPoint = hit.point;
                Debug.DrawRay(startPosition, rayDirection * hit.distance, Color.yellow);
            }
            else
            {
                hitPoint = startPosition + rayDirection * Mathf.Infinity;
                Debug.DrawRay(startPosition, rayDirection * maxDistance, Color.red);
            }

            Vector3 objectPosition = trackedObject == null ? Vector3.zero : trackedObject.position;
            Vector2 gazePolar = WorldToPolar(center, hitPoint);
            Vector2 objectPolar = WorldToPolar(center, objectPosition);

            userGaze = new Vector2(hit.point.x, hit.point.z);

            // this script calls log method for gaze events
            logger.LogGaze(Time.time, Time.time - exercise.timeAnchor, objectPolar.x, objectPolar.y,
                gazePolar.x, gazePolar.y, userTransform.position, userTransform.eulerAngles);
            // Debug.Log("Gaze: " + gazePolar + " | Object: " + objectPolar);
        }
    }

    /// <summary>
    /// Converts world position to relative position, ignoring scale.
    /// </summary>
    /// <param name="transform">The transform to use as the origin.</param>
    /// <param name="position">The world position to convert.</param>
    /// <returns>The relative position.</returns>
    private static Vector3 InverseTransformWithoutScale(Transform transform, Vector3 position)
    {
        return Quaternion.Inverse(transform.rotation) * (position - transform.position);
    }

    /// <summary>
    /// Convert a world position to polar coordinates relative to the root transform at the center of a cylinder.
    /// </summary>
    /// <param name="center">The root transform at the center of the cylinder.</param>
    /// <param name="position">The world position to convert.</param>
    /// <returns>The polar coordinates of the position. The x component is the angle in degrees (left: -90, right: +90), and the y component is the height.</returns>
    public static Vector2 WorldToPolar(Transform center, Vector3 position)
    {
        Vector3 localPosition = InverseTransformWithoutScale(center, position);
        Vector2 polarPosition = new Vector2
        {
            x = -Vector2.SignedAngle(Vector2.up, new Vector2(localPosition.x, localPosition.z)),
            y = localPosition.y,
        };
        return polarPosition;
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
