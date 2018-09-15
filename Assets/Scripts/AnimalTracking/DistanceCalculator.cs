using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    private int numFrames = 0;//number of frames already measured
    [SerializeField] private Text currentText;
    [SerializeField] private Text avgText;
    private float currentDist;
    private float avgDist;
    [SerializeField] private TrackGameManager gm;
    private Rect screenRect;

    private void Awake()
    {
        numFrames = 0;
        screenRect = new Rect(Vector2.zero,new Vector2( Screen.width, Screen.height));
    }
    private void Update()
    {
        if (!screenRect.Contains(target.position))
        {
            gm.LogDistance(avgDist);
            enabled = false;
            target.gameObject.SetActive(false);
            return;
        }
        currentDist = Vector2.Distance(Input.mousePosition,target.position);
        avgDist = (avgDist * numFrames + currentDist) / (numFrames + 1);
        numFrames++;
        avgText.text = avgDist.ToString("F0");
        currentText.text = currentDist.ToString("F0");
    }

}
