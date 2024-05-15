using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LocalSaveFileHandler
{
    string dataDirPath = "";

    public LocalSaveFileHandler(string dataDirPath)
    {
        this.dataDirPath = dataDirPath;
        //Debug.Log($"DirPathChosed = {dataDirPath}");
    }



    /// <summary>
    /// Saves the Data to Local PersistentDataPath
    /// </summary>
    /// <param name="fileID"></param>
    /// <param name="data"></param>
    public void SaveGameDataToLocal(string fileID, GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, fileID);
        
        FileStream stream = File.Create(fullPath);
        StreamWriter writer = new StreamWriter(stream);

        string dataToStore = JsonUtility.ToJson(data, true);

        writer.Write(dataToStore);

        writer.Close();
        stream.Close();

        //Debug.Log($"GameSavedTo = {fullPath}");
    }


    public GameData LoadGameDataFromLocal(string fileID)
    {
        string fullPath = Path.Combine(dataDirPath, fileID);
        string dataToLoad = "";
        GameData deserelizeData = null;

        try
        {
            FileStream stream = File.OpenRead(fullPath);
            StreamReader reader = new StreamReader(stream);

            dataToLoad = reader.ReadToEnd();

            stream.Close();
            reader.Close();

            deserelizeData = JsonUtility.FromJson<GameData>(dataToLoad);

            //Debug.Log($"Successfully Load Data From = {fullPath}");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }


        return deserelizeData;
    }
}
