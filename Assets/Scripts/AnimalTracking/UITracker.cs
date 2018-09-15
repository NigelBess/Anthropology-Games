using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracker : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField] private Transform tracked;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rt.position = Camera.main.WorldToScreenPoint(tracked.position);
    }
}
