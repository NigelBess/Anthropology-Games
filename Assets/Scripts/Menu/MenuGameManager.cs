using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameManager : MonoBehaviour
{
    [SerializeField]private PlayerInfo info;
    [SerializeField] private InfoUI[] ui; 
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
        SceneManager.LoadScene("Trajectory");
    }
    public void Trajectory2()
    {
        info.thirdPersonMode = true;
        SceneManager.LoadScene("Trajectory");
    }
    public void Tracking()
    {
        SceneManager.LoadScene("AnimalTracking");
    }
    public void ResetEverything()
    {
        Destroy(info.transform.gameObject);
        SceneManager.LoadScene("Menu");
    }

}
