using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTracker : MonoBehaviour
{
    public Exercise exercise;
    public WinScreen winScreen;
    public float totalDifference = 0.0f;
    public bool isTracking = false;
    public ITracker tracker;
    public int updateCounter = 0;
    public double upperConstraint = 1.5f;
    public String rating = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            updateCounter++;

            if (exercise.hasWon == true)
            {
                EndTracking();
            }
            else
            {
                if (tracker != null && tracker.CurrentlyTracking == true)
                {
                    totalDifference += Vector2.Distance(tracker.CurrentGaze, tracker.CurrentObj);
                    Debug.Log("Current Difference:          " + Vector2.Distance(tracker.CurrentGaze, tracker.CurrentObj));
                    Debug.Log("Current Gaze:        " + tracker.CurrentGaze);
                    Debug.Log("Current Object:      " + tracker.CurrentObj);
                }
            }
        }
    }

    public void StartTracking()
    {
        isTracking = true;
    }

    public void EndTracking()
    {
        isTracking = false;
    }

    void OnDestroy()
    {
        isTracking = false;
        Debug.Log("Total Difference Before: " + totalDifference);
        totalDifference = totalDifference / updateCounter;
        totalDifference = Mathf.Clamp((1.5f - totalDifference) / 1.5f, 0, 1) * 100;
        Debug.Log("Total Difference After: " + totalDifference);

        if (totalDifference >= 90.0f)
        {
            rating = "A";
        }
        else if (totalDifference >= 80.0f && totalDifference <= 89.99f)
        {
            rating = "B";
        }
        else if (totalDifference >= 70.0f && totalDifference <= 79.99f)
        {
            rating = "C";
        }
        else if( totalDifference >= 60.0f && totalDifference <= 69.99f)
        {
            rating = "D";
        }
        else
        {
            rating = "F";
        }
        winScreen.SetText(Math.Round(totalDifference, 2) + "% Correct" + " Rating: " + rating);

        Debug.Log("Total Difference DESTROYEDDDDDDDDDDDDDDDDDDDDDDDDDD: " + totalDifference);
    } 
}
