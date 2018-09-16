using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public gameInfo[] games;
    public static PlayerInfo instance;
    
    public enum Game
    {
        traj1,
        traj2,
        track,
        find
    }
    [System.Serializable]
    public struct gameInfo
    {
        public List<float> scores;
        public bool completed;
    }
	void Awake ()
    {
        DontDestroyOnLoad(this);
        if (instance != this)
        {
            instance = this;
        }
	}
    public void LogScore(Game game,float score)
    {
        if (!games[(int)game].completed) games[(int)game].completed = true;
        games[(int)game].scores.Add(score);
    }
	
}
