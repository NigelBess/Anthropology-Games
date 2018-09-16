using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour

{
    public GameObject[] canvases;
    private int LastMenu;
    private int currentMenu;

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
    public void Menu()
    {
        OpenCanvas(3);
    }
    public void Return()
    {
        OpenCanvas(LastMenu);
    }
    public void AreYouSure()
    {
        OpenCanvas(4);
    }
    public void AreYouSure2()
    {
        OpenCanvas(5);
    }




    void OpenCanvas(int num)
    {
        LastMenu = currentMenu;
        currentMenu = num;
        GameFunctions.OpenMenu(canvases,num);
    }

}
