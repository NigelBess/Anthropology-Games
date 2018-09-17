using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrajGameManager : MonoBehaviour
{

    private Projectile proj;
    [SerializeField] private bool defaultThirdPerson = false;
    private PlayerInfo.Game gameType;
    [SerializeField] private ClickDetector clickDetect;
    [SerializeField] private CanvasManager cm;
    [SerializeField] private Projectile[] projectiles;
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private GameObject[] menus;

    [SerializeField] private Vector3 clickedPoint;
    [SerializeField] private Text afterLandText;
    [SerializeField] private Text gameCompleteText;
    [SerializeField]private int throwsPerProj = 2;
    private int maxThrows;
    [SerializeField] private GameObject throwLogPrefab;
    private int numThrows;
    private float[] throws;
    private int currentProjectile;
    private PlayerInfo info;
    

    [SerializeField] private GameObject parentOfLogs;

    private void Awake()
    {
        maxThrows = throwsPerProj * projectiles.Length;
        SetProj(0);
        foreach (Projectile p in projectiles)
        {
            p.gm = this;
        }
        throws = new float[maxThrows];
        numThrows = 0;
        cm = GetComponent<CanvasManager>();
        clickDetect = GetComponent<ClickDetector>();
        clickDetect.Stop();
        SetMode();
        ResetProjectiles();

    }
    void ResetProjectiles()
    {
        foreach (Projectile p in projectiles)
        {
            p.Reset();
        }    
    }
    void SetMode()
    {
        if (info == null) info = PlayerInfo.instance;
        bool thirdPerson = defaultThirdPerson;
        if (info != null) thirdPerson = info.thirdPersonMode;
        Transform startPoint = startPoints[0];
        if (thirdPerson)
        {
            startPoint = startPoints[1];
            gameType = PlayerInfo.Game.traj2;
        }
        else
        {
            gameType = PlayerInfo.Game.traj1;
        }
        foreach (Projectile p in projectiles)
        {
            p.SetInitial(startPoint,thirdPerson);
        }
        clickDetect.RelayInfo(thirdPerson,startPoint);
    }
    void SetProj(int num)
    {
        currentProjectile = num;
        proj = projectiles[num];
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].transform.gameObject.SetActive(i==num);
        }
    }
    public void PrepNewThrow()
    {
        cm.HUD();
        OpenMenu(0);
        clickDetect.Stop();
        proj.Reset();
    }
    public void DoNewThrow()
    {
        SetProj(numThrows/throwsPerProj);
        PrepNewThrow();
        Throw();
    }
    public void Throw()
    {   
        proj.Throw();
        NoMenu();
        
    }
    public void WaitForInput()
    {
        clickDetect.Begin();
        OpenMenu(1);//instructions
    }
    public void LogClick(Vector3 point)
    {
        NoMenu();
        clickedPoint = point;
        proj.UnPause();
    }
    public void LogLand(Vector3 point)
    {
        OpenMenu(2);
        point.y = 0;
        clickedPoint.y = 0;
        Debug.Log("your point: "+ clickedPoint.ToString()+" actual point: "+point.ToString());

        float dist = Vector3.Distance(point,clickedPoint);
        afterLandText.text = "Your guess was <color=yellow>" + dist.ToString("F2") + "</color> meters off.";
        throws[numThrows] = dist;
        numThrows++;
       // UILogThrow(numThrows, dist);
        if (numThrows >= maxThrows)
        {
            GameComplete();
        }
    }
    void GameComplete()
    {
        PrepNewThrow();
        cm.GameComplete();
        string report = "";
        for (int i = 0; i < maxThrows; i++)
        {
            report += "Throw "+(i+1).ToString()+": <color=yellow>" + throws[i].ToString("F2") + "</color> meters\n\n"; 
        }
        gameCompleteText.text = report;
        if (info == null) info = PlayerInfo.instance;
        if (info != null)
        {
            foreach (float f in throws)
            {
                info.LogScore(gameType,f);
            }
        }
    }

    public void UILogThrow(int num,float dist)
    {
        GameObject newObj = GameObject.Instantiate(throwLogPrefab,parentOfLogs.transform);
        newObj.GetComponent<Text>().text = "Throw " + num.ToString() + ": " + dist.ToString("F2");
        RectTransform rt = newObj.GetComponent<RectTransform>();
        float newHeight = rt.position.y - rt.rect.height * (num - 1);
        rt.position = new Vector3(rt.position.x,newHeight);

    }

    void OpenMenu(int num)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == num);
        }

    }
    void NoMenu()
    {
        OpenMenu(-1);
    }
    public void ResetAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Save()
    {
       //string filename = "fileName";
       // string path = "C:\\Users\\Nigel\\Desktop\\"+filename+".csv";
       // System.IO.File.Create(path);
       // string report = "";
       // for (int i = 0; i < maxThrows; i++)
       // {
       //     report +=  throws[i].ToString() + ",\n";
       // }
       // System.IO.File.WriteAllText(path, report);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ClickTimedOut()
    {
        PrepNewThrow();
        OpenMenu(3);    
    }
}
