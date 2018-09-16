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
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }


}
