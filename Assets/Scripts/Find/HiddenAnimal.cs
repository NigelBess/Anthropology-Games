using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAnimal : MonoBehaviour
{
    [SerializeField] private FindGameManager gm;
    [SerializeField] private GameObject circle;
    private bool found;

    private void Awake()
    {
        found = false;
        circle.SetActive(false);
    }
    private void OnMouseUp()
    {
        if (found) return;
        found = true;
        gm.LogClick(true);
        Debug.Log("clicky the hidey");
        circle.SetActive(true);
    }
}
