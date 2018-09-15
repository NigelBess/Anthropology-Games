using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private TrajGameManager gm;
    public GameObject mouseIndicator;

    private void Awake()
    {
        gm = GetComponent<TrajGameManager>();
    }
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            mouseIndicator.SetActive(true);
            mouseIndicator.transform.position = hit.point;
        }
        else
        {
            mouseIndicator.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                gm.LogClick(hit.point);
                Stop();
                mouseIndicator.SetActive(true);
            }
            else
            {
                return;
            }
           
        }
    }
    public void Stop()
    {
        enabled = false;
        mouseIndicator.SetActive(false);
    }
    public void Begin()
    {
        enabled = true;
    }
}
