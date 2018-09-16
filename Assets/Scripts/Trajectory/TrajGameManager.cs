using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrajGameManager : MonoBehaviour
{

    private Projectile proj;
    public ClickDetector clickDetect;
    public CanvasManager cm;
    public Projectile[] projectiles;
    public GameObject[] menus;

    public Vector3 clickedPoint;
    public Text afterLandText;
    public Text gameCompleteText;
    [SerializeField]private int throwsPerProj = 2;
    private int maxThrows;
    public GameObject throwLogPrefab;
    private int numThrows;
    private float[] throws;
    private int currentProjectile;

    public GameObject parentOfLogs;

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
        float dist = Vector3.Distance(point,clickedPoint);
        afterLandText.text = "Your guess was " + dist.ToString("F2") + " meters off.";
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
            report += "Throw "+(i+1).ToString()+": " + throws[i].ToString() + "\n"; 
        }
        gameCompleteText.text = report;
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
}
