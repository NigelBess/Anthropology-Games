using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    [SerializeField] private string directory;
    public void Save(string fileName, string data)
    {
        string path = Application.persistentDataPath + "/" + directory;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "/" + fileName+".csv";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            FileStream file = File.Create(path);
            file.Close();
        }
        using (TextWriter writer = new StreamWriter(path, append : true))
        {
            writer.WriteLine(data);
        }
    }

}
