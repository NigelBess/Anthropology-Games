using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBarDetection : MonoBehaviour
{
    [SerializeField] private TrackGameManager gm;
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gm.Play();
        }
	}
}
