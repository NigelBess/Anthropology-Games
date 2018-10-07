using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    string writePath;
    [SerializeField] private string fileName;
    private void Awake()
    {
        writePath = Application.dataPath + "/"+fileName+".txt";
        string text = "hello world";
        System.IO.File.WriteAllText( writePath, text);
    }
}
