using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class FindGameManager : MonoBehaviour
{
    private int currentAnimal;
    [SerializeField]private int countDownTime = 3;
    [SerializeField] private GameObject[] animalCanavases;
    [SerializeField] private CanvasManager cm;
    [SerializeField] private DynamicText hudInfo;
    [SerializeField] private GameObject[] resultUI;
    [SerializeField] private FindClickDetector clickDetect;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject timeCanvas;
    [SerializeField] private Text nextButtonText;
    [SerializeField] private Text averageText;
    [SerializeField] private Text resultsText;
    [SerializeField] private string[] animalNames;
    private float[] scores;
    private PlayerInfo info;
    private void Awake()
    {
        CloseAnimals();
        CloseResult();
        scores = new float[animalCanavases.Length];
        info = PlayerInfo.instance;
    }
    private void CloseAnimals()
    {
        foreach (GameObject o in animalCanavases)
        {
            if (o != null) o.SetActive(false);
        }
    }
    private void CloseResult()
    {
        GameFunctions.OpenMenu(resultUI, 0);
    }
    public void Next()
    {
        currentAnimal++;
        if (currentAnimal >= animalCanavases.Length)
        {
            SaveResults();
            cm.GameComplete();
            CloseAnimals();
            SetResultsText();
        }
        else
        {
            CountDown(currentAnimal);
            clickDetect.Reset();
            CloseResult();
        }
        
    }
    private void SetResultsText()
    {
        averageText.text = "Average score: <color=yellow>" + scores.Average().ToString("F2")+ "</color> seconds";
        string str = "";
        for (int i = 0; i < scores.Length; i++)
        {
            str += animalNames[i] + ": <color=yellow>" + scores[i].ToString("F2") + "</color> seconds\n";
        }
        resultsText.text = str;
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
        CloseResult();
    }
    public void LogClick(bool didHit)
    {
        if (didHit)
        {
            clickDetect.StopDetect();
            GameFunctions.OpenMenu(resultUI, 2);//correct
            Debug.Log("you found it");
            LogResult(currentAnimal,timer.GetTime());
            if (currentAnimal >= animalCanavases.Length - 1)
            {
                nextButtonText.text = "View Results";
            }
            else
            {
                nextButtonText.text = "Next";
            }
            
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
        CloseAnimals();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void LogResult(int animalNum, float time)
    {
        scores[animalNum] = time;
    }
    private void SaveResults()
    {
        if (info == null) return;
        for(int i = 0; i < scores.Length;i++)
        {
            info.LogScore(PlayerInfo.Game.find,scores[i]);
            info.Save("Animal_Finding",scores[i].ToString("F3")+", "+animalNames[i]+",");
        }
    }
    public void ResetAll()
    {
        SceneManager.LoadScene("Find");
    }
}
