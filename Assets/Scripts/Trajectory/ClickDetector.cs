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
    private bool thirdPersonMode;
    private Transform initialPoint;
    private float m;
    private float b;

    private void Awake()
    {
        gm = GetComponent<TrajGameManager>();
    }
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        Vector2 screenPoint = Input.mousePosition;
        if (thirdPersonMode)
        {
            //calculate m and b
            //m and b are from y=mx+b in screenspace
            // m and b define the line ins screeenspace where we are allowed to guess the landing to happen in third person mode
            Vector3 u = new Vector3(initialPoint.position.x, 0, initialPoint.position.z);
            Vector3 v = initialPoint.forward;
            v.y = 0;
            Vector3 w = u + v;
            //u and w are two worldspace positions contained in the line
            //will convert to screenspace
            u = Camera.main.WorldToScreenPoint(u);
            w = Camera.main.WorldToScreenPoint(w);
            //get y = mx+b in screen space
            m = (w.y - u.y) / (w.x - u.x);
            b = w.y - m * w.x;

            //in screenspace, we need to find the closest point to the line defined by y=mx+b
            Vector3 input = Input.mousePosition;
            float x;
            float y;
            if (m == 0)
            {
                x = input.x;
                y = b;
            }
            else
            {
                float m2 = -1 / m;

                float b2 = input.y - m2 * input.x;
                //equation m2*x+b2=m*x+b where x is the x value of the closest point on the line
                 x = (b - b2) / (m2 - m);
                y = m * x + b;
            }
            screenPoint = new Vector2(x,y);
        }
        ray = Camera.main.ScreenPointToRay(screenPoint);
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
    public void RelayInfo(bool thirdPerson,Transform initial)
    {
        thirdPersonMode = thirdPerson;
        if (!thirdPerson) return;

        initialPoint = initial;

     }
}
