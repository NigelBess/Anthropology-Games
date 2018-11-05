using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text infoText;
    [SerializeField] private Color completeColor;
    [SerializeField] private MenuCanvasManager cm;
    private string units;
    private PlayerInfo.Game measuredGame;
    private PlayerInfo.gameInfo info;
    private void Awake()
    {
        if (!info.completed) transform.gameObject.SetActive(false);
    }
    public void Set(PlayerInfo.gameInfo gi,PlayerInfo.Game game)
    {
        Debug.Log("setting UI to "+ gi.completed.ToString());
        if (!gi.completed) return;
        transform.gameObject.SetActive(gi.completed);
        info = gi;
        units = "";
        measuredGame = game;
        switch (game)
        {
            case PlayerInfo.Game.traj1:
                units = "meters";
                break;
            case PlayerInfo.Game.traj2:
                units = "meters";
                break;
            case PlayerInfo.Game.track:
                units = "pixels";
                break;
            case PlayerInfo.Game.find:
                units = "seconds";
                break;
        }
        infoText.text = "Average Score: <color=yellow>" + gi.scores.Average().ToString("F2") + "</color> ";
        playButton.targetGraphic.color = completeColor;
    }
    public void DetailedInfo()
    {
        cm.LogResults(info.scores,units,measuredGame);
    }


}
