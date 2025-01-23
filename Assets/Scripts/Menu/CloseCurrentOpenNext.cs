using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCurrentOpenNext : MonoBehaviour
{
    public GameObject[] current;
    public GameObject[] next;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Closes the current objects and opens the next objects
    public void CloseOpen()
    {
        Debug.Log("CloseCurrentOpenNext OCCURREEEEDDD");
        foreach (GameObject obj in current)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in next)
        {
            obj.SetActive(true);
        }
    }
}
