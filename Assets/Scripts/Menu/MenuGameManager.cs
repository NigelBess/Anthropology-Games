using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    public void Trajectory1()
    {
        SceneManager.LoadScene("Trajectory1");
    }
    public void Tracking()
    {
        SceneManager.LoadScene("AnimalTracking");
    }

}
