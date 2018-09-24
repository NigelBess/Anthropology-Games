using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindClickDetector : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private FindGameManager gm;
    private bool clicked;
    private void Awake()
    {
        Reset();
    }
    public void StartDetect()
    {
        enabled = true;
        timer.StartTimer();
        clicked = false;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!clicked)
            {
                clicked = true;
                gm.Clicking();
            }
            return;
        }
        if (clicked)//and not mousebuttondown (because of the "return" in the previous if)
        {
            clicked = false;
            gm.LogClick(false);
        }
    }
    public void StopDetect()
    {
        enabled = false;
        timer.StopTimer();
    }
    public void Reset()
    {
        timer.Reset();
        enabled = false;
        clicked = false;
    }
}
