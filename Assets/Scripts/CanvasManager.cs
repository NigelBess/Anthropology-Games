using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour

{
    public GameObject[] canvases;

	void Awake ()
    {
        Welcome();
	}
    public void Welcome()
    {
        OpenCanvas(0);
    }
    public void HUD()
    {
        OpenCanvas(1);
    }
    public void GameComplete()
    {
        OpenCanvas(2);
    }





    void OpenCanvas(int num)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == num);
        }
    }

}
