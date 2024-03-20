using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;

public class PlayerLevelSave
{
    public bool isTutorialFinish;
    [Serializable]
    public class LevelDataMini
    {
        public int totalScore;
        public bool unlocked;
        public bool hasFinishIntro;
        public bool hasBeatenLevelOnce;
    }
    public LevelDataMini[] levelDataMiniList;
}
public class PlayerManager : MonoBehaviour
{
    const string SaveStateKey = "playerSaveData_Key";
    [Header("SaveFile")]
    [SerializeField]private SOPlayerFile realFile;
    [SerializeField]private SOPlayerFile resetFile;

    [Header("References")]
    [SerializeField]private GameManager gameManager;


    private PlayerLevelSave playerLevelSave;
    public static Action HasFinishedTutorialMain, ResetPlayerSave;
    public static Action<int> HasFinishedIntroLevel;
    public static Action<int, int> HasBeatenLvl;
    public static Func<bool> IsTutorialMainFinish;
    public static Func<int> TotalLevels;
    public static Func<int, LevelData> LevelDataNow;

    [Header("Debug Only")]
    public bool checkSave;

    private void Awake() 
    {
        HasFinishedTutorialMain += HasFinished_TutorialMain;
        ResetPlayerSave += ResetSavePlayer;
        HasFinishedIntroLevel += HasFinished_IntroLevel;
        HasBeatenLvl += HasBeatenLevel;
        IsTutorialMainFinish += IsFinish_TutorialMain;
        LevelDataNow += GetLevelData;
        TotalLevels += TotalLevel;

        gameManager = GetComponent<GameManager>();
        if(gameManager.levelModeNow() == LevelMode.Home)Load();
    }

    private void Update() {
        if(checkSave)
        {
            checkSave = false;
            playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
            for(int i=1;i<realFile.levelDataList.Count;i++)
            {
                playerLevelSave.levelDataMiniList[i].totalScore = realFile.levelDataList[i].totalScore;
                playerLevelSave.levelDataMiniList[i].unlocked = realFile.levelDataList[i].unlocked;
                playerLevelSave.levelDataMiniList[i].hasFinishIntro = realFile.levelDataList[i].hasFinishIntro;
                playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce = realFile.levelDataList[i].hasBeatenLevelOnce;
            }
            Save();
        }
    }
    private bool IsFinish_TutorialMain(){ return realFile.isTutorialFinish; }
    private LevelData GetLevelData(int level){ return realFile.levelDataList[level]; }
    private int TotalLevel(){ return realFile.levelDataList.Count;}
    private void HasFinished_TutorialMain()
    {
        realFile.isTutorialFinish = true;
        playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;

        Save();
    }
    private void HasFinished_IntroLevel(int level)
    {
        realFile.levelDataList[level].hasFinishIntro = true;
        playerLevelSave.levelDataMiniList[level].hasFinishIntro = true;

        Save();
    }
    private void HasBeatenLevel(int level, int score)
    {
        realFile.levelDataList[level].totalScore = score;
        playerLevelSave.levelDataMiniList[level].totalScore = score;

        if(!realFile.levelDataList[level].hasBeatenLevelOnce)
        {
            realFile.levelDataList[level].hasBeatenLevelOnce = true;
            playerLevelSave.levelDataMiniList[level].hasBeatenLevelOnce = true;
        }

        if(level+1 <= realFile.levelDataList.Count)
        {
            realFile.levelDataList[level+1].unlocked = true;
            playerLevelSave.levelDataMiniList[level+1].unlocked = true;
        }
        
        Save();
    }
    private void Save()
    {
        string Json = JsonConvert.SerializeObject(playerLevelSave);
        PlayerPrefs.SetString(SaveStateKey, Json);
        PlayerPrefs.Save();
    }
    private void Load()
    {
        if (PlayerPrefs.HasKey(SaveStateKey))
        {
            string loadString = PlayerPrefs.GetString(SaveStateKey);
            playerLevelSave = JsonConvert.DeserializeObject<PlayerLevelSave>(loadString);

            realFile.isTutorialFinish = playerLevelSave.isTutorialFinish;
            for(int i=1;i<realFile.levelDataList.Count;i++)
            {
                realFile.levelDataList[i].totalScore = playerLevelSave.levelDataMiniList[i].totalScore;
                realFile.levelDataList[i].unlocked = playerLevelSave.levelDataMiniList[i].unlocked;
                realFile.levelDataList[i].hasFinishIntro = playerLevelSave.levelDataMiniList[i].hasFinishIntro;
                realFile.levelDataList[i].hasBeatenLevelOnce = playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce;
            }
        }
        else
        {
            playerLevelSave = new PlayerLevelSave();
            playerLevelSave.levelDataMiniList = new PlayerLevelSave.LevelDataMini[realFile.levelDataList.Count];
            for(int i=1;i<realFile.levelDataList.Count;i++)
            {
                playerLevelSave.levelDataMiniList[i] = new PlayerLevelSave.LevelDataMini();
            }
        }
    }
    private void ResetSavePlayer()
    {
        UIKotakP3K.ResetSaveData();
        realFile.isTutorialFinish = resetFile.isTutorialFinish;
        realFile.isTutorialFinish = resetFile.isTutorialFinish;
        for(int i=1;i<realFile.levelDataList.Count;i++)
        {
            realFile.levelDataList[i].totalScore = resetFile.levelDataList[i].totalScore;
            realFile.levelDataList[i].unlocked = resetFile.levelDataList[i].unlocked;
            realFile.levelDataList[i].hasFinishIntro = resetFile.levelDataList[i].hasFinishIntro;
            realFile.levelDataList[i].hasBeatenLevelOnce = resetFile.levelDataList[i].hasBeatenLevelOnce;
        }

        playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
        for(int i=1;i<realFile.levelDataList.Count;i++)
        {
            playerLevelSave.levelDataMiniList[i].totalScore = realFile.levelDataList[i].totalScore;
            playerLevelSave.levelDataMiniList[i].unlocked = realFile.levelDataList[i].unlocked;
            playerLevelSave.levelDataMiniList[i].hasFinishIntro = realFile.levelDataList[i].hasFinishIntro;
            playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce = realFile.levelDataList[i].hasBeatenLevelOnce;
        }
        
        Save();
    }

}
