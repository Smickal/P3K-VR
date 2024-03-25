using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;
using BNG;

[Serializable]
public class PlayerPositionSave
{
    public LevelMode levelMode;
    [Serializable]
    public class PositionsInLevelMode
    {
        public LevelP3KType levelP3KType;
        [Serializable]
        public class position{
            public string namaPosisi;
            public Vector3 playerPosition;
            public float playerRotation;

        }
        public position[] positions;
    }
    public PositionsInLevelMode[] positionsInLevel;
}

[Serializable]
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
    [SerializeField]private Transform playerGameObject;
    [Header("DO NOT FORGET TO SET")]
    [SerializeField]private float playerTeleport_y = 1.65f;
    [SerializeField]private PlayerPositionSave[] playerPosition;

    [SerializeField]private PlayerLevelSave playerLevelSave;
    public static Action HasFinishedTutorialMain, ResetPlayerSave, SetPlayerPosition_DoP3k;
    public static Action<InGame_Mode> ChangeInGame_Mode_Now;
    public static Action<int> HasFinishedIntroLevel;
    public static Action<int, int> HasBeatenLvl;
    public static Func<bool> IsTutorialMainFinish;
    public static Func<int> TotalLevels;
    public static Func<int, LevelPlayerData> LevelDataNow;

    [Header("Debug Only")]
    public bool checkSave;
    [Tooltip("Nyalakan ini kalo gamau di set posisi di awal")]
    public bool WantToExploreWorld;

    public bool MasukQuest;
    
    private void Awake() 
    {
        HasFinishedTutorialMain += HasFinished_TutorialMain;
        ResetPlayerSave += ResetSavePlayer;
        HasFinishedIntroLevel += HasFinished_IntroLevel;
        HasBeatenLvl += BeatenLevel;
        IsTutorialMainFinish += IsFinish_TutorialMain;
        LevelDataNow += GetLevelData;
        TotalLevels += TotalLevel;
        SetPlayerPosition_DoP3k += SetPlayerPosition_InGame_DoP3k;
        ChangeInGame_Mode_Now += ChangePlayerLastInGameMode;

        gameManager = GetComponent<GameManager>();
        if(!WantToExploreWorld)SetPlayerPositionAwake();
        if(gameManager.levelModeNow() == LevelMode.Home)Load();
    }

    private void Update() {
        // Debug.Log(playerGameObject.position + "tiga");
        if(checkSave)
        {
            checkSave = false;
            playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
            for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
            {
                playerLevelSave.levelDataMiniList[i].totalScore = realFile.levelPlayerDataList[i].totalScore;
                playerLevelSave.levelDataMiniList[i].unlocked = realFile.levelPlayerDataList[i].unlocked;
                playerLevelSave.levelDataMiniList[i].hasFinishIntro = realFile.levelPlayerDataList[i].hasFinishIntro;
                playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce = realFile.levelPlayerDataList[i].hasBeatenLevelOnce;
            }
            Save();
        }

        if(MasukQuest)
        {
            MasukQuest = false;
            SetPlayerPosition_InGame_DoP3k();
            // Debug.Log(playerGameObject.position + "satu");
        }
        // Debug.Log(playerGameObject.position + "dua");
    }
    public bool IsFinish_TutorialMain(){ return realFile.isTutorialFinish; }
    public LevelPlayerData GetLevelData(int level){ return realFile.levelPlayerDataList[level]; }
    private void ChangePlayerLastInGameMode(InGame_Mode change)
    {
        realFile.lastInGameMode = change;
    }
    private int TotalLevel(){ return realFile.levelPlayerDataList.Count;}
    private void HasFinished_TutorialMain()
    {
        realFile.isTutorialFinish = true;
        playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;

        Save();
    }
    private void HasFinished_IntroLevel(int level)
    {
        realFile.levelPlayerDataList[level].hasFinishIntro = true;
        playerLevelSave.levelDataMiniList[level].hasFinishIntro = true;

        Save();
    }
    private void BeatenLevel(int level, int score)
    {
        realFile.levelPlayerDataList[level].totalScore = score;
        playerLevelSave.levelDataMiniList[level].totalScore = score;

        if(!realFile.levelPlayerDataList[level].hasBeatenLevelOnce)
        {
            realFile.levelPlayerDataList[level].hasBeatenLevelOnce = true;
            playerLevelSave.levelDataMiniList[level].hasBeatenLevelOnce = true;
        }

        if(level+1 <= realFile.levelPlayerDataList.Count)
        {
            realFile.levelPlayerDataList[level+1].unlocked = true;
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
            for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
            {
                realFile.levelPlayerDataList[i].totalScore = playerLevelSave.levelDataMiniList[i].totalScore;
                realFile.levelPlayerDataList[i].unlocked = playerLevelSave.levelDataMiniList[i].unlocked;
                realFile.levelPlayerDataList[i].hasFinishIntro = playerLevelSave.levelDataMiniList[i].hasFinishIntro;
                realFile.levelPlayerDataList[i].hasBeatenLevelOnce = playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce;
            }
        }
        else
        {
            playerLevelSave = new PlayerLevelSave();
            playerLevelSave.levelDataMiniList = new PlayerLevelSave.LevelDataMini[realFile.levelPlayerDataList.Count];
            for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
            {
                playerLevelSave.levelDataMiniList[i] = new PlayerLevelSave.LevelDataMini();
            }
        }
    }
    private void ResetSavePlayer()
    {
        UIKotakP3K.ResetSaveData();
        UILangkahP3K.ResetSaveData();

        realFile.lastLevel = LevelMode.Home;
        realFile.lastInGameMode = InGame_Mode.NormalWalk;
        realFile.isTutorialFinish = resetFile.isTutorialFinish;
        realFile.isTutorialFinish = resetFile.isTutorialFinish;
        for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
        {
            realFile.levelPlayerDataList[i].totalScore = resetFile.levelPlayerDataList[i].totalScore;
            realFile.levelPlayerDataList[i].unlocked = resetFile.levelPlayerDataList[i].unlocked;
            realFile.levelPlayerDataList[i].hasFinishIntro = resetFile.levelPlayerDataList[i].hasFinishIntro;
            realFile.levelPlayerDataList[i].hasBeatenLevelOnce = resetFile.levelPlayerDataList[i].hasBeatenLevelOnce;
        }

        playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
        for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
        {
            playerLevelSave.levelDataMiniList[i].totalScore = realFile.levelPlayerDataList[i].totalScore;
            playerLevelSave.levelDataMiniList[i].unlocked = realFile.levelPlayerDataList[i].unlocked;
            playerLevelSave.levelDataMiniList[i].hasFinishIntro = realFile.levelPlayerDataList[i].hasFinishIntro;
            playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce = realFile.levelPlayerDataList[i].hasBeatenLevelOnce;
        }
        
        Save();
    }

    private void SetPlayerPositionAwake()
    {
        PlayerPositionSave.PositionsInLevelMode.position positionNow = new PlayerPositionSave.PositionsInLevelMode.position();
        if(gameManager.levelModeNow() == LevelMode.Home)
        {
            if(realFile.lastLevel == LevelMode.Home)
            {
                positionNow = playerPosition[0].positionsInLevel[0].positions[0];                
            }
            else
            {
                positionNow = playerPosition[0].positionsInLevel[0].positions[1];  
            }
            realFile.lastInGameMode = InGame_Mode.NormalWalk;
        }
        else
        {
            if(gameManager.levelTypeNow() == LevelP3KType.Choking)
            {
                if(realFile.lastLevel == LevelMode.Home)
                {
                    positionNow = playerPosition[1].positionsInLevel[0].positions[0];  
                    realFile.lastInGameMode = InGame_Mode.NormalWalk;
                }
                else
                {
                    if(realFile.lastInGameMode == InGame_Mode.NormalWalk)
                    {
                        positionNow = playerPosition[1].positionsInLevel[0].positions[1]; 
                        gameManager.ChangeInGameMode(InGame_Mode.NormalWalk);
                    }
                    else
                    {
                        positionNow = playerPosition[1].positionsInLevel[0].positions[2]; 
                        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
                    }
                }
            }
            else if(gameManager.levelTypeNow() == LevelP3KType.Bleeding)
            {
                if(realFile.lastLevel == LevelMode.Home)
                {
                    positionNow = playerPosition[1].positionsInLevel[1].positions[0];  
                    realFile.lastInGameMode = InGame_Mode.NormalWalk;
                }
                else
                {
                    if(realFile.lastInGameMode == InGame_Mode.NormalWalk)
                    {
                        positionNow = playerPosition[1].positionsInLevel[1].positions[1]; 
                        gameManager.ChangeInGameMode(InGame_Mode.NormalWalk);
                    }
                    else
                    {
                        positionNow = playerPosition[1].positionsInLevel[1].positions[2]; 
                        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
                    }
                }
            }
            
        }

        // playerGameObject.position = positionNow.playerPosition;
        // playerGameObject.rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        Vector3 destination = new Vector3(positionNow.playerPosition.x, playerTeleport_y, positionNow.playerPosition.z);
        Quaternion rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        PlayerTeleport.Teleport(destination, rotation);
        realFile.lastLevel = gameManager.levelModeNow();
    }
    private void SetPlayerPosition_InGame_DoP3k()
    {
        PlayerPositionSave.PositionsInLevelMode.position positionNow = new PlayerPositionSave.PositionsInLevelMode.position();
        if(gameManager.levelTypeNow() == LevelP3KType.Choking)
        {
            positionNow = playerPosition[1].positionsInLevel[0].positions[2]; 
        }
        else if(gameManager.levelTypeNow() == LevelP3KType.Bleeding)
        {
            positionNow = playerPosition[1].positionsInLevel[1].positions[2]; 
        }
        
        // playerGameObject.position += positionNow.playerPosition;
        // playerGameObject.rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        // Debug.Break();
        Debug.Log(playerGameObject.position + "posisi");
        Vector3 destination = new Vector3(positionNow.playerPosition.x, playerTeleport_y, positionNow.playerPosition.z);
        Quaternion rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        PlayerTeleport.Teleport(destination, rotation);
        // Debug.Break();
        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
        
    }

}
