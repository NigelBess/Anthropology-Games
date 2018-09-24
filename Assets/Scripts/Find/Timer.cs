using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float startTime;
    private Text timeText;

    private void Awake()
    {
        
        enabled = false;
        timeText = GetComponent<Text>();
        timeText.text = "";
    }
    public void StartTimer()
    {
        enabled = true;
        startTime = Time.time;
    }
    private void Update()
    {
        timeText.text = GetTime().ToString("F2");
    }
    public float GetTime()
    {
        return Time.time - startTime;
    }
    public void StopTimer()
    {
        enabled = false;
    }

}
