using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressChecker : MonoBehaviour
{
	void Start ()
    {
        if (PlayerInfo.instance == null) return;
        if (PlayerInfo.instance.AreAllComplete())
        {
            PlayerInfo.instance.SaveTotal();
        }
	}
	
}
