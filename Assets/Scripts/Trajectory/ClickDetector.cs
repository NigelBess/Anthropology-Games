using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour
{
    private TrajGameManager gm;
    public GameObject mouseIndicator;
    [SerializeField] private float maxClickTime;
    private float remainingClickTime;
    [SerializeField] private Text timerText;
    [SerializeField] private Image timerBar;

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
        remainingClickTime -= Time.deltaTime;
        if (remainingClickTime < 0)
        {
            gm.ClickTimedOut();
        }
        timerText.text = "Remaining Time: <color=yellow>"+remainingClickTime.ToString("F1")+"</color> seconds";
        timerBar.fillAmount = remainingClickTime / maxClickTime;
    }
    public void Stop()
    {
        enabled = false;
        mouseIndicator.SetActive(false);
    }
    public void Begin()
    {
        enabled = true;
        remainingClickTime = maxClickTime;
    }
}
