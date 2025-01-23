using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes an object follow the user's gaze, with a specified delay, offset, and deadzone.
/// </summary>
public class TagAlong : MonoBehaviour
{
    [Tooltip("The user's transform (e.g. the camera).")]
    public Transform userTransform;

    [Space(10)]

    [Tooltip("How fast the object moves towards the target position.")]
    public float followSpeed = 3f;

    [Tooltip("How far the user has to look/move away from the object to make it move.")]
    public float deadzone = 0.1f;
    
    [Space(10)]

    [Tooltip("If true, uses the initial offset relative to the user on start. Otherwise, uses the specified offset.")]
    public bool useInitialOffset = false;
    
    [Tooltip("Where you want the object to be, relative to the user.")]
    public Vector3 offset = new Vector3(0, 0, 1); // By default, the object is 1 unit in front of the user

    [Space(10)]

    [Tooltip("Whether the object should follow the user's yaw (left-right) rotation.")]
    public bool followYaw = true;

    [Tooltip("Whether the object should follow the user's pitch (up-down) rotation.")]
    public bool followPitch = false;

    [Space(10)]

    [Tooltip("Set this to true to keep the object's height fixed in the world space.")]
    public bool fixedHeight = false;

    [Tooltip("By default, the object's forward vector points towards the user. Set this to true to flip it.")]
    public bool flipForward = false;

    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Vector3 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;

        if (useInitialOffset)
        {
            offset = transform.position - userTransform.position;
        }

        // Set initial position based on offset
        transform.rotation = GetLookAtRotation();
        transform.position = GetCurrentTarget();
    }

    private Quaternion GetLookAtRotation()
    {
        Quaternion rotation = Quaternion.LookRotation((userTransform.position - transform.position) * (flipForward ? -1 : 1), Vector3.up);

        // Reset rotation angles based on enabled axes
        rotation.eulerAngles = new Vector3(followPitch ? rotation.eulerAngles.x : initialRotation.x,
                                           followYaw ? rotation.eulerAngles.y : initialRotation.y,
                                           rotation.eulerAngles.z);

        return rotation;
    }

    private Vector3 GetCurrentTarget()
    {
        Vector3 target = userTransform.position + Quaternion.Euler(followPitch ? userTransform.eulerAngles.x : 0,
                                                                   followYaw ? userTransform.eulerAngles.y : 0,
                                                                   0) * offset;
        if (fixedHeight)
        {
            target.y = initialPosition.y;
        }
        return target;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = GetLookAtRotation();
        Vector3 newTarget = GetCurrentTarget();

        // Only update the target position if it's far enough from the current position
        if ((Vector3.Distance(currentTarget, newTarget) > deadzone) ||
            (Vector3.Distance(fixedHeight ? Flatten(transform.position) : transform.position,
                              fixedHeight ? Flatten(newTarget) : newTarget) > deadzone)) // Second condition is to check for large movements
        {
            currentTarget = newTarget;
        }

        // Move the object towards the target position
        transform.position = SlerpAround(transform.position, currentTarget, userTransform.position, followSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Slerps (spherical lerp) between two vectors, but around a specified origin.
    /// </summary>
    /// <param name="from">The start vector.</param>
    /// <param name="to">The end vector.</param>
    /// <param name="origin">The origin around which to slerp.</param>
    /// <param name="t">The interpolation parameter (time).</param>
    /// <returns>The slerped vector.</returns>
    private static Vector3 SlerpAround(Vector3 from, Vector3 to, Vector3 origin, float t)
    {
        return Vector3.Slerp(from - origin, to - origin, t) + origin;
    }

    /// <summary>
    /// Flattens a vector by setting its y component to 0.
    /// </summary>
    /// <param name="v">The vector to flatten.</param>
    /// <returns>The flattened vector.</returns>
    private static Vector3 Flatten(Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
}