using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject title;
    public GameObject RecalibrateScreen;
    public GameObject leftHandPosition;
    public GameObject recalibrationScreen;
    public GameObject centerEye;
    public GameObject resultScreen;
    public ExerciseManager exerciseManager;
    public MenuMaintenance menuMaintenance;
    public GameObject rockThrowMenu;
    public GameObject fireFlyMenu;
    public bool test1 = false;
    public bool test2 = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // for debugging purposes
        if (test1)
        {
            StartRockThrow();
            test1 = false;
        }
        if (test2)
        {
            StartFirefly();
            test2 = false;
        }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    /**
    * Opens the hand menu next to user's left hand while pointing towards the user's view.
    */
    public void OpenMenu()
    {
        if (recalibrationScreen.activeSelf || title.activeSelf)
        {
            return;
        }
        menu.transform.position = leftHandPosition.transform.position;
        menu.transform.LookAt(centerEye.transform.position);
        menu.SetActive(true);
    }

    /**
    * Starts the rock throw exercise and disables menu so that both do
    * not show at the same time.
    */
    public void StartRockThrow()
    {
        CloseExercises();
        rockThrowMenu.SetActive(true);
        exerciseManager.StartExercise(0);
        menu.SetActive(false);
    }

    /**
    * Starts the firefly exercise and disables menu so that both do
    * not show at the same time.
    */
    public void StartFirefly()
    {
        CloseExercises();
        fireFlyMenu.SetActive(true);
        exerciseManager.StartExercise(1);
        menu.SetActive(false);
    }

    /**
    * Disables the hand menu and stops the current exercise in progress. 
    * Then begins recalibration.
    */
    public void Recalibrate()
    {
        menu.SetActive(false);
        CloseExercises();
        recalibrationScreen.SetActive(true);
    }

    public void OpenTitleScreen()
    {
        menu.SetActive(false);
        CloseExercises();
        title.SetActive(true);
    }

    public void CloseExercises()
    {
        fireFlyMenu.SetActive(false);
        rockThrowMenu.SetActive(false);
        exerciseManager.StopExercise();
        resultScreen.SetActive(false);
    }
}
