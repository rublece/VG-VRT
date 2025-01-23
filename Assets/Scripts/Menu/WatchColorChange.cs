using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchColorChange : MonoBehaviour
{
    public bool colorChanged = false;
    public float colorChangedTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (colorChanged)
        {
            colorChangedTime += Time.deltaTime;
            if (colorChangedTime >= 0.25f)
            {
                GetComponent<Renderer>().materials[2].color = Color.white;
                colorChangedTime = 0f;
                colorChanged = false;
            }
        }
    }

    public void ChangeColor()
    {
        GetComponent<Renderer>().materials[2].color = Color.blue;
        colorChanged = true;
    }
}
