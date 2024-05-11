using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class DataSaveManager : MonoBehaviour
{
    const string Player = "Player_";
    const string PlayerIDKey = "UniquePlayerID_Key";

    public static DataSaveManager Instance;

    GameData gameData;
    LocalSaveFileHandler dataHandler;
    string currentFileSaveID;
    int playerID;

    private void Awake()
    {
        Instance = this;

        if(!PlayerPrefs.HasKey(PlayerIDKey))
        {
            playerID = 1;
        }
        
        else
        {
            playerID = PlayerPrefs.GetInt(PlayerIDKey);
        }

        dataHandler = new LocalSaveFileHandler(Application.persistentDataPath);
    }


    public void SaveScoreAndTime(LevelP3KType levelType, ScoreName scoreName, float TimeFinished)
    {
        gameData = new GameData();
        gameData.PlayerID = CreatePersonID();

        gameData.LevelName = levelType.ToString();
        gameData.LevelScore = scoreName.ToString();
        gameData.TimeToFinish = TimeFinished;

        dataHandler.SaveGameDataToLocal(gameData.PlayerID, gameData);
    }

    private string CreatePersonID()
    {
        currentFileSaveID = Player + playerID.ToString();
        playerID++;

        Debug.Log($"Created New ID => {currentFileSaveID}");

        PlayerPrefs.SetInt(PlayerIDKey, playerID);
        PlayerPrefs.Save();

        return currentFileSaveID;
    }
}
