using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTimer : Timer
{
    [SerializeField]private float allowedTime;//seconds
    [SerializeField] private RotationGameManager gm;
    public override void Update()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime > allowedTime)
        {
            gm.GameComplete();
            return;
        }
        int totalSeconds = (int)(allowedTime - elapsedTime);
        int remainingMinutes = totalSeconds / 60;
        int remainingSeconds = totalSeconds % 60;
        timeText.text = remainingMinutes.ToString() + ":" + remainingSeconds.ToString("D2");
    }
}
