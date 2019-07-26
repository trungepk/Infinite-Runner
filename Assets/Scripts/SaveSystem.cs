using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Items/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void SaveItem(string selectedItem)
    {
        int saveNumber = 1;
        while(File.Exists(SAVE_FOLDER + "Item_" + saveNumber + ".txt"))
        {
            saveNumber++;
        }
        File.WriteAllText(SAVE_FOLDER + "Item_" + saveNumber + ".txt", selectedItem);
        Debug.Log(saveNumber);
    }

    public static string LoadItem()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] savedFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFile = null;
        foreach(var fileInfo in savedFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }
        if (mostRecentFile != null)
        {
            return File.ReadAllText(mostRecentFile.FullName);
        }
        else
        {
            return null;
        }
    }

}
