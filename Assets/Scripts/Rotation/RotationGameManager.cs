using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RotationGameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager cm;
    [SerializeField] private Question[] questions = new Question[10];
    [SerializeField] private Image[] answerImages = new Image[4];
    private Outline[] answerOutlines;
    [SerializeField] private Image originalImage;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private RotationTimer timer;
    [SerializeField] private Text[] resultsText;
    [SerializeField] private Text mainResultsText;
    [SerializeField] private Button submitButton;
    private PlayerInfo info;
    private Result[] results;
    private int currentQuestion;
    [System.Serializable]
    private struct Question
    {
        public Sprite originalSprite;
        public Sprite[] answerSprites;
        public int[] rightAnswers;
    }
    [System.Serializable]
    private struct Result
    {
        public int[] choice;
        public int score;
        public float time;
    }
    private void Awake()
    {
        answerOutlines = new Outline[answerImages.Length];
        for (int i = 0; i< answerImages.Length;i++)
        {
            answerOutlines[i] = answerImages[i].GetComponent<Outline>();
        }
        results = new Result[questions.Length];
        for (int i = 0; i < results.Length; i++)
        {
            results[i].choice = new int[2];
            results[i].choice[0] = -1;
            results[i].choice[1] = -1;
            results[i].score = 0;
            results[i].time = 0.0f;
        }
    }
    public void SetQuestion(int num)
    {
        originalImage.sprite = questions[num].originalSprite;
        for (int i = 0; i < answerImages.Length; i++)
        {
            answerImages[i].sprite = questions[num].answerSprites[i];
        }
        currentQuestion = num;
        SetOutlines();
    }
    public void SelectAnswer(int choice)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //check if choice has already been selected
        for (int i = 0; i < results[currentQuestion].choice.Length; i++)
        {
            if (results[currentQuestion].choice[i] == choice)
            {
                results[currentQuestion].choice[i] = -1;
                SetOutlines();
                return;
            }
        }
        int selectionNumber = 0;
        //if we make it this far the choice has not been selected yet
        for (int i = 0; i < results[currentQuestion].choice.Length; i++)
        {
            if ((results[currentQuestion].choice[i] == -1) || (i == results[currentQuestion].choice.Length - 1))
            {
                results[currentQuestion].choice[i] = choice;
                selectionNumber = i;
                SetOutlines();
                return;
            }
        }
       
    }
    private void SetOutlines()
    {
        submitButton.interactable = true;
        for (int i = 0; i < answerOutlines.Length; i++)
        {
            answerOutlines[i].enabled = false;
            foreach (int num in results[currentQuestion].choice)
            {
                if (i == num)
                {
                    answerOutlines[i].enabled = true;
                }
                if (num < 0)
                {
                    submitButton.interactable = false;
                }

            }
        }
    }
    public void Submit()
    {
        EventSystem.current.SetSelectedGameObject(null);
       results[currentQuestion].score = 0;
        int[] submittedAnswers = new int[results[currentQuestion].choice.Length];
        for (int i = 0; i<submittedAnswers.Length;i++)
        {
            submittedAnswers[i] = -1;
        }
        for (int i = 0; i < results[currentQuestion].choice.Length; i++)
        {
            if (System.Array.IndexOf(questions[currentQuestion].rightAnswers,results[currentQuestion].choice[i])!=-1)
            {
                if (System.Array.IndexOf(submittedAnswers,results[currentQuestion].choice[i])==-1)
                {
                    results[currentQuestion].score++;
                    submittedAnswers[i] = results[currentQuestion].choice[i];
                }
                
            }
        }
        
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
        timer.StopTimer();
        string answer;
        int numCorrect = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            numCorrect+=results[i].score;
        }
        mainResultsText.text = "Score :" +"<color=yellow>" + numCorrect.ToString()+ "/" + (results[0].choice.Length*questions.Length).ToString() + "</color>";
        for(int i = 0; i < resultsText.Length; i++)
        {
            
            if (i < questions.Length)
            {
                if (results[i].score==2)
                {
                    answer = "<color=#00FF00>2/2</color>";
                }
                else
                {
                    answer = "<color=#FF0000>"+results[i].score.ToString()+"/2</color>";
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
                string dataString = (System.Convert.ToInt32(results[i].score)).ToString() + ", " + results[i].choice.ToString() + ", " + results[i].time.ToString("F3") + ", " + i.ToString() + ","; 
                info.Save("Mental_Rotation",dataString);
            }
            }
    }
    public void ResetAll()
    {
        SceneManager.LoadScene("Rotation");
    }
}
