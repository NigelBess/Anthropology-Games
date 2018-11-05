using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public gameInfo[] games;
    public static PlayerInfo instance;
    public bool thirdPersonMode = false;
    private string identifier;
    private DataLogger dl;
    [SerializeField] private string mainFileName;
    
    
    public enum Game
    {
        traj1,
        traj2,
        track,
        find,
        rotation,
    }

    [System.Serializable]
    public struct gameInfo
    {
        public List<float> scores;
        public bool completed;
    }
	void Awake ()
    {   
       
        if (instance != this)
        {
            if (instance != null)
            {
                Destroy(transform.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }
        dl = GetComponent<DataLogger>();
        SetID(GameFunctions.RandomString(16));
	}
    public void SetID(string id)
    {
        identifier = id;
    }
    public void LogScore(Game game,float score)
    {
        if (!games[(int)game].completed) games[(int)game].completed = true;
        games[(int)game].scores.Add(score);
    }
    public void Save(string fileName, string data)
    {
        dl.Save( identifier +"_" + fileName,data);
    }
    public bool AreAllComplete()
    {
        foreach (gameInfo g in games)
        {
            if (!g.completed) return false;
        }
        return true;
    }
    public void SaveTotal()
    {
        string data = identifier + ", ";
        for (int i = 0; i < games.Length; i++)
        {
           data += games[i].scores.Average().ToString("F3")+", ";
        }
        dl.Save(mainFileName,data);
    }
}
