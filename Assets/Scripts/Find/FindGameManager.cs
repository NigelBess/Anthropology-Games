using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGameManager : MonoBehaviour
{
    private int currentAnimal;
    [SerializeField]private int countDownTime = 3;
    [SerializeField] private GameObject[] animalCanavases;
    [SerializeField] private CanvasManager cm;
    [SerializeField] private DynamicText hudInfo;
    [SerializeField] private GameObject[] resultUI;
    [SerializeField] private FindClickDetector clickDetect;
    [SerializeField] private GameObject timeCanvas;
    private void Awake()
    {
        foreach (GameObject o in animalCanavases)
        {
           if(o!=null) o.SetActive(false);
        }
    }
    private void Next()
    {
        currentAnimal++;
        CountDown(currentAnimal);
    }
    public void CountDown(int animalNum)
    {
        cm.HUD();
        ShowCountDown();
        hudInfo.StartCountDown(countDownTime);
        StartCoroutine(WaitToShow(animalNum));
    }
    IEnumerator WaitToShow(int animalNum)
    {
        yield return new WaitForSeconds(countDownTime);
        ShowAnimal(animalNum);
        clickDetect.StartDetect();
    }
    public void StartGame()
    {
        currentAnimal = 0;
        CountDown(0);
    }
    public void Clicking()
    {
        GameFunctions.OpenMenu(resultUI,0);//none
    }
    public void LogClick(bool didHit)
    {
        if (didHit)
        {
            clickDetect.StopDetect();
            GameFunctions.OpenMenu(resultUI, 2);//correct
            Debug.Log("you found it");
        }
        else
        {
            GameFunctions.OpenMenu(resultUI, 1);//incorrect
            Debug.Log("you didnt find it");
        }
    }
    private void ShowAnimal(int animalNum)
    {
        cm.HUD();
        GameFunctions.OpenMenu(animalCanavases, animalNum);
        timeCanvas.SetActive(false);
    }
    private void ShowCountDown()
    {
        timeCanvas.SetActive(true);
        foreach (GameObject g in animalCanavases)
        {
            if (g!=null) g.SetActive(false);
        }
    }
}
