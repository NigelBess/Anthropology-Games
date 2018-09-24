using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameManager : MonoBehaviour
{
    [SerializeField]private PlayerInfo info;
    [SerializeField] private InfoUI[] ui;
    [SerializeField] private Button doubleCheckButton;
    [SerializeField] private Text doubleCheckButtonText;
    [SerializeField] private Text doubleCheckText;
    [SerializeField] private CanvasManager cm;
    private void Start()
    {
        info = PlayerInfo.instance;
        for(int i = 0;i<info.games.Length;i++)
        {
            ui[i].Set(info.games[i],(PlayerInfo.Game)i);
        }
    }
    public void Trajectory1()
    {  
        info.thirdPersonMode = false;
       TryToLoad("Trajectory",PlayerInfo.Game.traj1);
    }
    public void Trajectory2()
    {
        info.thirdPersonMode = true;
        TryToLoad("Trajectory", PlayerInfo.Game.traj2);
    }
    public void Tracking()
    {
        TryToLoad("AnimalTracking", PlayerInfo.Game.track);
    }
    public void Find()
    {
        TryToLoad("Find", PlayerInfo.Game.find);
    }
    public void ResetEverything()
    {
        Destroy(info.transform.gameObject);
        SceneManager.LoadScene("Menu");
    }
    private void TryToLoad(string sceneName, PlayerInfo.Game game)
    {
        if (info.games[(int)game].completed)
        {
            cm.AreYouSure2();
            doubleCheckButton.onClick.RemoveAllListeners();
            doubleCheckButton.onClick.AddListener(delegate { SceneManager.LoadScene(sceneName); });
            doubleCheckText.text = "You have already completed this task.\nWould you like to play again?";
            doubleCheckButtonText.text = "Play Again";
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void TryToQuit()
    {
        cm.AreYouSure2();
        doubleCheckButton.onClick.RemoveAllListeners();
        doubleCheckButton.onClick.AddListener(delegate { Quit(); });
        doubleCheckText.text = "Are you sure you want to quit?\nYou will lose all data.";
        doubleCheckButtonText.text = "Quit";
    }
    public void Quit()
    {
        Application.Quit();
    }


}
