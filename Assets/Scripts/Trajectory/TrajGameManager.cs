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
    [SerializeField]private int throwsPerProj = 3;
    private int maxThrows;
    [SerializeField] private GameObject throwLogPrefab;
    private int numThrows;
    private result[] results;
    private int currentProjectile;
    private PlayerInfo info;
    private string outputFileName;
    private bool demoFirst = false;

    [System.Serializable]
    private struct result
    {
        public float distance;
        public bool shadow;
        public string projectile;
    }

    

    [SerializeField] private GameObject parentOfLogs;

    private void Awake()
    {
        maxThrows = 2*throwsPerProj * projectiles.Length;//2 at the beggining is for with/without shadows
        
        foreach (Projectile p in projectiles)
        {
            p.gm = this;
        }
        results = new result[maxThrows];
        numThrows = 0;
        SetProj();
        cm = GetComponent<CanvasManager>();
        clickDetect = GetComponent<ClickDetector>();
        clickDetect.Stop();
        SetMode();
        ResetProjectiles();
        demoFirst = true;
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
            outputFileName = "Trajectory_Third_Person";
        }
        else
        {
            gameType = PlayerInfo.Game.traj1;
            outputFileName = "Trajectory_First_Person";
        }
        foreach (Projectile p in projectiles)
        {
            p.SetInitial(startPoint,thirdPerson);
        }
        clickDetect.RelayInfo(thirdPerson,startPoint);
    }
    void SetProj()
    {
        int num =((numThrows / throwsPerProj) % 2); //cycles through 0 1nd 1 as numthrows changes from 0 to (maxThrows-1)
        if (currentProjectile != num)
        {
            currentProjectile = num;
            demoFirst = true;
        }
        else
        {
            demoFirst = false;
        }
        
        proj = projectiles[num];
        string projName = "Arrow";
        if (num > 0) projName = "Tennis Ball";
        results[numThrows].projectile = projName;
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].transform.gameObject.SetActive(i==num);
        }
        bool shadow = numThrows / throwsPerProj < 2;
        proj.SetShadows(shadow);
        results[numThrows].shadow = shadow;
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
        SetProj();
        PrepNewThrow();
        Throw();
    }
    public void Throw()
    {   
        proj.Throw(demoFirst);
        if (demoFirst)
        {
            OpenMenu(4);
        }
        else
        {
            NoMenu();
        }
        
        
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
        if (gameType == PlayerInfo.Game.traj2)
        {
            dist = Mathf.Abs(point.x - clickedPoint.x);
        }
        afterLandText.text = "Your guess was <color=yellow>" + dist.ToString("F2") + "</color> meters off.";
        results[numThrows].distance = dist;
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
            report += "Throw "+(i+1).ToString()+": <color=yellow>" + results[i].distance.ToString("F2") + "</color> meters\n\n"; 
        }
        gameCompleteText.text = report;
        if (info == null) info = PlayerInfo.instance;
        if (info != null)
        {
            foreach (result r in results)
            {
                info.LogScore(gameType,r.distance);
                string shadowText = "No Shadow";
                if (r.shadow) shadowText = "With Shadow";
                info.Save(outputFileName,r.distance.ToString("F4")+", "+ shadowText+", "+r.projectile+",");
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
    public void NoMenu()
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
