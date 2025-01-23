using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using TMPro;
using UnityEngine;

public class MenuMaintenance : MonoBehaviour
{
    public GameObject menu;
    public GameObject watch;
    public GameObject countdownScreen;
    public GameObject root;
    public GameObject head;
    public float countdownTime = 5f;
    public bool timeActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive)
        {
            countdownTime -= Time.deltaTime;
            TextMeshProUGUI words = countdownScreen.GetComponentInChildren<TextMeshProUGUI>();
            words.text = countdownTime.ToString("0");
        }

        if (countdownTime <= 0f)
        {
            ActivateWatch();
            countdownScreen.SetActive(false);
            timeActive = false;
            countdownTime = 5f;
            root.transform.rotation = Quaternion.Euler(0f, head.transform.rotation.eulerAngles.y, 0f);
            root.transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);
        }
    }

    public void ActivateWatch()
    {
        watch.SetActive(true);
    }

    public void Countdown()
    {
        menu.SetActive(false);
        countdownScreen.SetActive(true);
        timeActive = true;
    }
}
