using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackGameManager : MonoBehaviour
{
    [SerializeField]private Animal animal;
    [SerializeField]private CanvasManager cm;
    [SerializeField] private GameObject[] interfaceContents;
    [SerializeField] private Text gameCompleteText;
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
        cm.GameComplete();
        GameFunctions.OpenMenu(interfaceContents, 2);
        gameCompleteText.text = "<color=yellow>" + distance.ToString("F2") + "</color> pixels";
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
