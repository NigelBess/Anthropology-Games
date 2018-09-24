using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScaler : MonoBehaviour
{
    private RectTransform rt;
    private float baseWidth;

	void Awake ()
    {
        rt = GetComponent<RectTransform>();
        baseWidth = rt.rect.width;
	}
	
	void Update ()
    {
        if ((int)Mathf.Floor(baseWidth * rt.sizeDelta.x) != (int)Screen.width)
        {
            float factor = Screen.width / baseWidth;
            rt.localScale = new Vector2(factor,factor);
        }
	}
}
