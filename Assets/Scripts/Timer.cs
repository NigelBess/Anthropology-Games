using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    protected float startTime;
    protected Text timeText;
    protected float elapsedTime;

    private void Awake()
    {
        
        
        timeText = GetComponent<Text>();
        Reset();
    }
    public void Reset()
    {
        enabled = false;
        timeText.text = "";
    }
    public void StartTimer()
    {
        enabled = true;
        startTime = Time.time;
    }
    public virtual void Update()
    {
        elapsedTime = Time.time - startTime;
        timeText.text = elapsedTime.ToString("F2");
    }
    public float GetTime()
    {
        return elapsedTime;    
    }
    public void StopTimer()
    {
        enabled = false;
    }

}
