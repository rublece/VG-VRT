using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningOfGame : MonoBehaviour
{
    public GameObject recalibrationScreen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
    * When user selects to start the app in title screen it disables the title screen
    * and enables the recalibration screen
    */
    public void StartApp()
    {
        gameObject.SetActive(false);
        recalibrationScreen.SetActive(true);
    }

    public void QuitApp()
    {
        Application.Quit();
    }   
}
