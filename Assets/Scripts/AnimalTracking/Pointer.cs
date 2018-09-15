using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Renderer rend;
    private Transform followed;
    private float offset;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    public void PointTo(Transform trans, float heightOffset)
    {
        followed = trans;
        offset = heightOffset;
    }
    private void Update()
    {
        
    }
    public void StopPointing()
    {
        transform.gameObject.SetActive(false);
    }
}
