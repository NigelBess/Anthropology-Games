using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTimer : Timer
{
    [SerializeField]private float allowedTime;//seconds
    public override void Update()
    {
        elapsedTime = Time.time - startTime;
        int totalSeconds = (int)(allowedTime - elapsedTime);
        int remainingMinutes = totalSeconds / 60;
        int remainingSeconds = totalSeconds % 60;
        timeText.text = remainingMinutes.ToString() + ":" + remainingSeconds.ToString("D2");

    }
}
