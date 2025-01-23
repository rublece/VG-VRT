using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreen;
    public float winTime = 0f;
    public TextMeshProUGUI resultText;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        winTime += Time.deltaTime;
        if (winTime >= 10f)
        {
            winTime = 0f;
            winScreen.SetActive(false);
            Debug.Log("Win Screen Disabled");
        }
    }

    public void SetText(string result)
    {
        resultText.text = result;
    }
}
