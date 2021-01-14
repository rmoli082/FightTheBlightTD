using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{

    public static string path = $"{Application.persistentDataPath}/saves/";
    public static string extension = ".ksv";

    public static void Save<T>(T objectToSave, string key)
    {
        Directory.CreateDirectory(path);

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream filestream = new FileStream(path + key + extension, FileMode.Create))
        {
            formatter.Serialize(filestream, objectToSave);
        }

    }

    public static T Load<T>(string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream filestream = new FileStream(path + key + extension, FileMode.Open))
        {
           returnValue = (T) formatter.Deserialize(filestream);
        }

        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string check = path + key + extension;
        return File.Exists(check);
    }

    public static void DeleteAllSaveData()
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete();
        Directory.CreateDirectory(path);
    }
}
