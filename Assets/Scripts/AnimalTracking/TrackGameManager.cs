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
    [SerializeField] private Text[] resultsText;
    [SerializeField] private int numTrials;
    [SerializeField] private Text trialsRemainingText;
    private int currentTrial;
    private PlayerInfo info;
    private float[] scores;
    private void Awake()
    {
        currentTrial = 0;
        scores = new float[numTrials];
    }
    public void StartPlay()
    {
        trialsRemainingText.text = "Trials Remaining: <color=yellow>" + (numTrials - currentTrial).ToString() + "</color>";
       cm.HUD();
        animal.Reset();
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
        scores[currentTrial] = distance;
        currentTrial++;
        if (currentTrial >= numTrials)
        {
            GameComplete();
        }
        else
        {
            StartPlay();
        }
    }
    public void GameComplete()
    {
        cm.GameComplete();
        GameFunctions.OpenMenu(interfaceContents, 2);
        for (int i = 0; i < resultsText.Length; i++)
        {
            if (i >= scores.Length)
            {
                resultsText[i].text = "";
            }
            else
            {
                resultsText[i].text = scores[i].ToString("F2") + "<color=white> pixels</color>";
            }
        }
        if (info == null) info = PlayerInfo.instance;
        if (info != null)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                info.LogScore(PlayerInfo.Game.track, scores[i]);
                info.Save("Animal_Tracking", scores[i].ToString("F3") + ",");
            }
        }
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
