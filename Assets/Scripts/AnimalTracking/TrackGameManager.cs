using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackGameManager : MonoBehaviour
{
    [SerializeField]private Animal animal;
    [SerializeField]private CanvasManager cm;
    [SerializeField] private GameObject[] interfaceContents;
    public void StartPlay()
    {
        cm.HUD();
        animal.transform.gameObject.SetActive(true);
        GameFunctions.OpenMenu(interfaceContents,0);
    }
    public void Play()
    {
        animal.RandomActionAfter(0);
        GameFunctions.OpenMenu(interfaceContents,1);
    }
    public void LogDistance(float distance)
    {
        animal.transform.gameObject.SetActive(false);
        GameFunctions.OpenMenu(interfaceContents, 2);
    }
}
