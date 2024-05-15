using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class DataSaveManager : MonoBehaviour
{
    const string Player = "Player_";
    const string PlayerIDKey = "UniquePlayerID_Key";

    public static DataSaveManager Instance;

    GameData gameData;
    LocalSaveFileHandler localDataHandler;
    string currentFileSaveID;
    int playerID;
    List<IPersistanceDataSave> dataSaveList;

    [Header("Turn this ON to Reset Player ID")]
    [SerializeField] bool _isDeleteAllPlayerID = false;

    private void Awake()
    {
        Instance = this;

        if(_isDeleteAllPlayerID)
        {
            PlayerPrefs.DeleteAll();
        }

        if(!PlayerPrefs.HasKey(PlayerIDKey))
        {
            playerID = 1;
        }       
        else
        {
            playerID = PlayerPrefs.GetInt(PlayerIDKey);
        }

        dataSaveList = FindAllPersistantDataSave();
        localDataHandler = new LocalSaveFileHandler(Application.persistentDataPath);

        //Tries to load a localSaveData
        gameData = localDataHandler.LoadGameDataFromLocal(Player + playerID.ToString());

        if(gameData == null)
        {
            gameData = new GameData(CreatePersonID());
        }
        else if(gameData.IsLevel1Done && gameData.IsLevel2Done)
        {
            playerID++;

            gameData = new GameData(CreatePersonID());
        }

    }
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.I))
    //    {
    //        gameData.IsLevel1Done = true;
    //        localDataHandler.SaveGameDataToLocal(gameData.PlayerID, gameData);
    //    }

    //    if(Input.GetKeyDown(KeyCode.O))
    //    {
    //        gameData.IsLevel2Done= true;
    //        localDataHandler.SaveGameDataToLocal(gameData.PlayerID, gameData);
    //    }
    //}

    public void Save()
    {
        foreach(var dataSave in dataSaveList)
        {
            dataSave.SaveData(gameData);
        }

        
        localDataHandler.SaveGameDataToLocal(gameData.PlayerID, gameData);
    }

    private List<IPersistanceDataSave> FindAllPersistantDataSave()
    {
        IEnumerable<IPersistanceDataSave> temp = FindObjectsOfType<MonoBehaviour>().OfType<IPersistanceDataSave>();



        return new List<IPersistanceDataSave>(temp);
    }

    private string CreatePersonID()
    {
        currentFileSaveID = Player + playerID.ToString();
       

        //Debug.Log($"Created New ID => {currentFileSaveID}");

        PlayerPrefs.SetInt(PlayerIDKey, playerID);
        PlayerPrefs.Save();

        return currentFileSaveID;
    }

    private void OnApplicationQuit()
    {
        playerID++;

        PlayerPrefs.SetInt(PlayerIDKey, playerID);
        PlayerPrefs.Save();
    }
}
