using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITracker: MonoBehaviour
{
    public abstract Vector2 CurrentGaze { get; }
    public abstract Vector2 CurrentObj { get; }
    public abstract bool CurrentlyTracking { get; }
}
