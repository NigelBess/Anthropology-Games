using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuCanvasManager : CanvasManager
{
    [SerializeField] private GameObject infoWindowParent;
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private Text infoTitleText;
    [SerializeField] private Text infoAvgText;
    [SerializeField] private Text infoNumberText;
    [SerializeField] private Text infoErrorText;
    [SerializeField] private int maxScoresToList = 30;
    private string targetSceneName;
    public void LogResults(List<float> scores,string units,PlayerInfo.Game game)
    {
        foreach (Transform child in infoWindowParent.transform)
        {
            Destroy(child.gameObject);
        }
        Info();
        int num = scores.Count;
        if (num > maxScoresToList)
        {
            num = maxScoresToList;
            infoErrorText.text = "Only the first " + maxScoresToList.ToString() + " trials are shown.";
        }
        else
        {
            infoErrorText.text = "";
        }
        for (int i = 0; i < num; i++)
        {
            GameObject infoObj = GameObject.Instantiate(infoPrefab,infoWindowParent.transform);
            infoObj.GetComponent<Text>().text = scores[i].ToString("F2");
        }
        string titleText = "";
        switch (game)
        {
            case PlayerInfo.Game.traj1:
                titleText = "First-Person Trajectory Prediction";
                break;
            case PlayerInfo.Game.traj2:
                titleText = "Third-Person Trajectory Prediction";
                break;
            case PlayerInfo.Game.track:
                titleText = "Animal Tracking";
                break;
            case PlayerInfo.Game.find:
                titleText = "Camouflaged Animal Detection";
                break;
        }
        infoTitleText.text = titleText;
        infoAvgText.text = "Average: <color=yellow>"+scores.Average().ToString("F2")+"</color> "+units;
        infoNumberText.text = "Number of Trials: <color=yellow>" + scores.Count.ToString() + "</color> ";
    }
    public void Info()
    {
        HUD();
    }
}
