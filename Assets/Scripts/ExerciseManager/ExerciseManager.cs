using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ExerciseManager : MonoBehaviour
{
    public GameObject[] exercises;
    public GameObject currentExercise;
    public GameObject winScreenTemplate;
    public GameObject winScreen;
    public LogToCSV logger;
    public bool createRockThrow = false;
    public bool createFirefly = false;
    public bool destroy = false;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // for debugging purposes
        if (createRockThrow)
        {
            StartExercise(0);
            createRockThrow = false;
        }
        if (createFirefly)
        {
            StartExercise(1);
            createFirefly = false;
        }
        if (destroy)
        {
            StopExercise();
            destroy = false;  
        }
    }

    public void StartExercise(int index)
    {
        StopExercise();
        currentExercise = Instantiate(exercises[index], transform.position, transform.rotation);
        currentExercise.SetActive(true);
        string name = exercises[index].name;
        logger.StartNewLog(name);
    }

    public void StopExercise()
    {
        if (ExerciseInProgress())
        {
            logger.EndLog();
            Destroy(currentExercise);
            currentExercise = null;
        }
    }

    public bool ExerciseInProgress()
    {
        return currentExercise != null;
    }

    // Disable all exercises on screen and enable the win screen.
    public void OnWin()
    {
        StopExercise();
        winScreenTemplate.SetActive(true);
        Debug.Log("Win Screen Enabled");
    }
}
