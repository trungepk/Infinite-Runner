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
        return (mostRecentFile != null) ? File.ReadAllText(mostRecentFile.FullName) : null;
    }

    public static List<string> LoadAllSavedItems()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] savedFiles = directoryInfo.GetFiles("*.txt");
        List<string> itemsDatas = new List<string>();
        foreach(var fileInfo in savedFiles)
        {
            string data = File.ReadAllText(fileInfo.FullName);
            if (!string.IsNullOrEmpty(data))
                itemsDatas.Add(data);
        }
        return itemsDatas;
    }

    public static List<string> ScanSaveFolder()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] savedFiles = directoryInfo.GetFiles("*.txt");
        List<string> itemsPaths = new List<string>();
        foreach (var fileInfo in savedFiles)
        {
            string data = File.ReadAllText(fileInfo.FullName);
            if (!string.IsNullOrEmpty(data))
                itemsPaths.Add(fileInfo.FullName);
        }
        return itemsPaths;
    }
}
