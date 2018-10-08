using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RotationGameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager cm;
    [SerializeField] private Question[] questions = new Question[10];
    [SerializeField] private Image[] answerImages = new Image[4];
    [SerializeField] private Image originalImage;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private RotationTimer timer;
    [SerializeField] private Text[] resultsText;
    [SerializeField] private Text mainResultsText;
    private PlayerInfo info;
    private Result[] results;
    private int currentQuestion;
    [System.Serializable]
    private struct Question
    {
        public Sprite originalSprite;
        public Sprite[] answerSprites;
        public int rightAnswer;

    }
    [System.Serializable]
    private struct Result
    {
        public int choice;
        public bool correct;
        public float time;
    }
    private void Awake()
    {
        results = new Result[questions.Length];
    }
    public void SetQuestion(int num)
    {
        originalImage.sprite = questions[num].originalSprite;
        for (int i = 0; i < answerImages.Length; i++)
        {
            answerImages[i].sprite = questions[num].answerSprites[i];
        }
        currentQuestion = num;
    }
    public void Answer(int choice)
    {
        EventSystem.current.SetSelectedGameObject(null);
        results[currentQuestion].choice = choice;
        results[currentQuestion].correct = choice == questions[currentQuestion].rightAnswer;
        results[currentQuestion].time = timer.GetTime();
        NextQuestion();
    }
    public void StartPlaying()
    {
        SetQuestion(0);
        cm.HUD();
        timer.StartTimer();
    }
    public void NextQuestion()
    {
        if (currentQuestion  >= questions.Length - 1)
        {
            GameComplete();
            return;
        }
        SetQuestion(currentQuestion+1);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GameComplete()
    {
        cm.GameComplete();
        string answer;
        int numCorrect = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            if (results[i].correct) numCorrect++;
        }
        mainResultsText.text = "Score :" +"<color=yellow>" + numCorrect.ToString()+ "/" + questions.Length.ToString() + "</color>";
        for(int i = 0; i < resultsText.Length; i++)
        {
            
            if (i < questions.Length)
            {
                if (results[i].correct)
                {
                    answer = "<color=#00FF00>Correct</color>";
                }
                else
                {
                    answer = "<color=#FF0000>Incorrect</color>";
                }
                resultsText[i].text = "Object " + (i+1).ToString() + ": " + answer;
            }
            else
            {
                resultsText[i].text = "";
            }
        }
        if (info == null) info = PlayerInfo.instance;
        if (info != null)
        {
            info.LogScore(PlayerInfo.Game.rotation,(float)numCorrect/(float)questions.Length);
            for (int i = 0; i < questions.Length; i++)
            {
                string dataString = (System.Convert.ToInt32(results[i].correct)).ToString() + ", " + results[i].choice.ToString() + ", " + results[i].time.ToString("F3") + ", " + i.ToString() + ","; 
                info.Save("Mental_Rotation",dataString);
            }
            }
    }
    public void ResetAll()
    {
        SceneManager.LoadScene("Rotation");
    }
}
