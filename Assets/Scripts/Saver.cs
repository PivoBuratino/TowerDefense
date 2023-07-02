using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Saver<T>
{
    private static string Path(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }
    public static void TryLoad(string filename, ref T data)
    {
        var path = Path(filename);
        Debug.Log(path);
        if (File.Exists(path))
        {
            Debug.Log($"loading from {path}");
            var dataString = File.ReadAllText(path);
            var saver = JsonUtility.FromJson<Saver<T>>(dataString);
            data = saver.Data;
        }
        else 
        {
            Debug.Log($"no file at {path}");
        }
    }

    public static void Save(string filename, T data)
    {
        var wrapper = new Saver<T> { Data = data };
        var dataString = JsonUtility.ToJson(wrapper);
       
        File.WriteAllText(Path(filename), dataString);
    }
    public T Data;    
}