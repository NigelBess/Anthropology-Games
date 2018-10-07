using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GameFunctions
{

    public static void OpenMenu(GameObject[] menus,int num)
    {
        for (int i = 0; i < menus.Length; i++)
        {   
            if(menus[i]!=null) menus[i].SetActive(i == num);

        }
    }
    public static void OpenMenu(List<GameObject> menus, int num)
    {
        for (int i = 0; i < menus.Count(); i++)
        {
            if (menus[i] != null) menus[i].SetActive(i == num);
        }
    }
    public static string RandomString(int length)
    {
        System.Random random = new System.Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];
        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        string finalString = new string(stringChars);
        return finalString;
    }
}
