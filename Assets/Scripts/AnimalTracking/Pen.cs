using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    
    [SerializeField] private Camera cam;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = cam.nearClipPlane;
            transform.position = cam.ScreenToWorldPoint(screenPos);
        }
    }
}
