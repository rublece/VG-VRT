using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Exercise : MonoBehaviour
{
    public UnityEvent winEvent;
    public UnityEvent startEvent;
    public UnityEvent toggleExplanationOff;
    public bool hasWon = false;
    public int repCounter; // cube starts at 1, sphere starts at 0
    public float timeAnchor = 0f;
    public TextMeshPro repDisplay;
    public LogToCSV logger;

    void Awake()
    {
        startEvent.Invoke();
        repDisplay.text = "Rep #" + 1;
    }

    // Update is called once per frame
    void Update()
    {   
        CheckWinCondition();
    }

    public void LogEvent(string eventName)
    {
        string logEvent = "rep" + repCounter + eventName;
        float relativeTime = Time.time - timeAnchor;    
        logger.LogStartOrEnd(Time.time, relativeTime, logEvent);
    }

    public void ResetTime()
    {
        timeAnchor = Time.time;
    }

    public void CheckWinCondition()
    {
        if (repCounter > 10)
        {
            hasWon = true;
            winEvent.Invoke();
        }
    }

    public void IncrementRep()
    {
        repCounter++;
        Debug.Log(repCounter);
        UpdateRepDisplay();
        CheckWinCondition();
    }

    public void UpdateRepDisplay()
    {
        repDisplay.text = "Rep #" + repCounter;
    }

    public void ShowExplanation(GameObject explanation)
    {
        Debug.Log("Show Explanationnnnnnnnnnnnnnnnnn");
        explanation.SetActive(true);
    }

    public void EndExplanation(GameObject explanation)
    {
        Debug.Log("End Explanationnnnnnnnnnnnnnnnnn");
        explanation.SetActive(false);
    }

    public void OnDestroy()
    {
        Debug.Log("Exercise Destroyed");
        toggleExplanationOff.Invoke();
    }
}
