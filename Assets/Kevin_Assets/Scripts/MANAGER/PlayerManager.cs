using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;
using BNG;

[Serializable]
public class PlayerLevelSave
{
    public bool isTutorialFinish;
    [Serializable]
    public class LevelDataMini
    {
        public ScoreName score;
        public bool unlocked;
        public bool hasFinishIntro;
        public bool hasBeatenLevelOnce;
    }
    public LevelDataMini[] levelDataMiniList;
}
public class PlayerManager : MonoBehaviour, ITurnOffStatic
{
    const string SaveStateKey = "playerSaveData_Key";
    [Header("SaveFile")]
    [SerializeField]private SOPlayerFile realFile;
    [SerializeField]private SOPlayerFile resetFile;

    [Header("References")]
    [SerializeField]private GameManager gameManager;
    [SerializeField]private Transform playerGameObject;
    [SerializeField]private PlayerTeleport playerTeleport;
    [SerializeField]private EnvironmentLevelManager environmentLevelManager;
    [Header("DO NOT FORGET TO SET")]
    [SerializeField]private float playerTeleport_y = 1.65f;
    [SerializeField]private SOPlayerPosition playerPositionSO;

    [SerializeField]private PlayerLevelSave playerLevelSave;
    public static Action HasFinishedTutorialMain, ResetPlayerSave, SetPlayerPosition_DoP3k, SetPlayerPosition_FinishP3k;
    public static Action<InGame_Mode> ChangeInGame_Mode_Now;
    public static Action<int> HasFinishedIntroLevel;
    public static Action<int, ScoreName> HasBeatenLvl;
    public static Func<bool> IsTutorialMainFinish;
    public static Func<int> TotalLevels;
    public static Func<int, LevelPlayerData> LevelDataNow;
    public static Func<InGame_Mode> LastInGameMode;

    [Header("Debug Only")]
    public bool checkSave;
    public bool SetAwakePos;
    public bool SetOtherPos;
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
        SetPlayerPosition_FinishP3k += SetPlayerPostion_InGame_AfterP3K;
        ChangeInGame_Mode_Now += ChangePlayerLastInGameMode;
        LastInGameMode += PlayerLastInGameMode;

        gameManager = GetComponent<GameManager>();
        
        Load();
        
        if(gameManager.LevelModeNow() == LevelMode.Level)
        {
            environmentLevelManager.SetEnvironmentAwake(realFile.levelPlayerDataList[(int)gameManager.LevelTypeNow()].hasFinishIntro, realFile.lastInGameMode == InGame_Mode.FirstAid);
        }
        if(!WantToExploreWorld)SetPlayerPositionAwake();
    }
    public void TurnOffStatic()
    {
        HasFinishedTutorialMain -= HasFinished_TutorialMain;
        ResetPlayerSave -= ResetSavePlayer;
        HasFinishedIntroLevel -= HasFinished_IntroLevel;
        HasBeatenLvl -= BeatenLevel;
        IsTutorialMainFinish -= IsFinish_TutorialMain;
        LevelDataNow -= GetLevelData;
        TotalLevels -= TotalLevel;
        SetPlayerPosition_DoP3k -= SetPlayerPosition_InGame_DoP3k;
        SetPlayerPosition_FinishP3k -= SetPlayerPostion_InGame_AfterP3K;
        ChangeInGame_Mode_Now -= ChangePlayerLastInGameMode;
        LastInGameMode -= PlayerLastInGameMode;
    }

    private void Update() {
        // Debug.Log(playerGameObject.position + "tiga");
        if(checkSave)
        {
            checkSave = false;
            playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
            for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
            {
                playerLevelSave.levelDataMiniList[i].score = realFile.levelPlayerDataList[i].score;
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

        if(SetAwakePos)
        {
            SetAwakePos = false;
            SetPlayerPositionAwake();
        }
        if(SetOtherPos)
        {
            SetOtherPos = false;
            SetPlayerPostion_InGame_AfterP3K();
        }
        // Debug.Log(playerGameObject.position + "dua");
    }
    public bool IsFinish_TutorialMain(){ return realFile.isTutorialFinish; }
    public LevelPlayerData GetLevelData(int level){ return realFile.levelPlayerDataList[level]; }
    public InGame_Mode PlayerLastInGameMode(){return realFile.lastInGameMode;}
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
    private void BeatenLevel(int level, ScoreName score)
    {
        if(score > realFile.levelPlayerDataList[level].score)
        {
            realFile.levelPlayerDataList[level].score = score;
            playerLevelSave.levelDataMiniList[level].score = score;
        }
        

        if(!realFile.levelPlayerDataList[level].hasBeatenLevelOnce)
        {
            realFile.levelPlayerDataList[level].hasBeatenLevelOnce = true;
            playerLevelSave.levelDataMiniList[level].hasBeatenLevelOnce = true;
        }

        if(level+1 < realFile.levelPlayerDataList.Count)
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
            Debug.Log("Test");
            string loadString = PlayerPrefs.GetString(SaveStateKey);
            playerLevelSave = JsonConvert.DeserializeObject<PlayerLevelSave>(loadString);

            realFile.isTutorialFinish = playerLevelSave.isTutorialFinish;
            for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
            {
                realFile.levelPlayerDataList[i].score = playerLevelSave.levelDataMiniList[i].score;
                realFile.levelPlayerDataList[i].unlocked = playerLevelSave.levelDataMiniList[i].unlocked;
                realFile.levelPlayerDataList[i].hasFinishIntro = playerLevelSave.levelDataMiniList[i].hasFinishIntro;
                realFile.levelPlayerDataList[i].hasBeatenLevelOnce = playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce;
            }
        }
        else
        {
            Debug.Log("Test");
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
            realFile.levelPlayerDataList[i].score = resetFile.levelPlayerDataList[i].score;
            realFile.levelPlayerDataList[i].unlocked = resetFile.levelPlayerDataList[i].unlocked;
            realFile.levelPlayerDataList[i].hasFinishIntro = resetFile.levelPlayerDataList[i].hasFinishIntro;
            realFile.levelPlayerDataList[i].hasBeatenLevelOnce = resetFile.levelPlayerDataList[i].hasBeatenLevelOnce;
        }

        playerLevelSave.isTutorialFinish = realFile.isTutorialFinish;
        for(int i=1;i<realFile.levelPlayerDataList.Count;i++)
        {
            playerLevelSave.levelDataMiniList[i].score = realFile.levelPlayerDataList[i].score;
            playerLevelSave.levelDataMiniList[i].unlocked = realFile.levelPlayerDataList[i].unlocked;
            playerLevelSave.levelDataMiniList[i].hasFinishIntro = realFile.levelPlayerDataList[i].hasFinishIntro;
            playerLevelSave.levelDataMiniList[i].hasBeatenLevelOnce = realFile.levelPlayerDataList[i].hasBeatenLevelOnce;
        }
        
        Save();
    }

    private void SetPlayerPositionAwake()
    {
        PlayerPositionSave.PositionsInLevelMode.position positionNow = new PlayerPositionSave.PositionsInLevelMode.position();
        positionNow = playerPositionSO.PlayerPositionSearch(gameManager.LevelModeNow(), gameManager.LevelTypeNow(), realFile.lastLevel, realFile.lastInGameMode, NamaPosisi.None);
        if(gameManager.LevelModeNow() == LevelMode.Home)
        {
            realFile.lastInGameMode = InGame_Mode.NormalWalk;
        }
        else
        {
            if(gameManager.LevelTypeNow() == LevelP3KType.Choking)
            {
                if(realFile.lastLevel == LevelMode.Home)
                {
                    realFile.lastInGameMode = InGame_Mode.NormalWalk;
                }
                else
                {
                    if(realFile.lastInGameMode == InGame_Mode.NormalWalk)
                    {
                        gameManager.ChangeInGameMode(InGame_Mode.NormalWalk);
                    }
                    else
                    {
                        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
                    }
                }
            }
            else if(gameManager.LevelTypeNow() == LevelP3KType.Bleeding)
            {
                if(realFile.lastLevel == LevelMode.Home)
                {
                    realFile.lastInGameMode = InGame_Mode.NormalWalk;
                }
                else
                {
                    if(realFile.lastInGameMode == InGame_Mode.NormalWalk)
                    {
                        gameManager.ChangeInGameMode(InGame_Mode.NormalWalk);
                    }
                    else
                    {
                        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
                    }
                }
            }
            
        }

        Vector3 destination = new Vector3(positionNow.playerPosition.x, playerTeleport_y, positionNow.playerPosition.z);
        Quaternion rotation = Quaternion.Euler(0,positionNow.playerRotation,0);

        playerTeleport.TeleportPlayerAwake(destination, rotation);
        realFile.lastLevel = gameManager.LevelModeNow();
    }
    private void SetPlayerPosition_InGame_DoP3k()
    {
        PlayerPositionSave.PositionsInLevelMode.position positionNow = new PlayerPositionSave.PositionsInLevelMode.position();
        positionNow = playerPositionSO.PlayerPositionSearch(gameManager.LevelModeNow(), gameManager.LevelTypeNow(), realFile.lastLevel, realFile.lastInGameMode, NamaPosisi.None);
        
        Vector3 destination = new Vector3(positionNow.playerPosition.x, playerTeleport_y, positionNow.playerPosition.z);
        Quaternion rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        playerTeleport.TeleportPlayer(destination, rotation);
        // Debug.Break();

        gameManager.ChangeInGameMode(InGame_Mode.FirstAid);
        
    }

    private void SetPlayerPostion_InGame_AfterP3K()
    {
        PlayerPositionSave.PositionsInLevelMode.position positionNow = new PlayerPositionSave.PositionsInLevelMode.position();
        positionNow = playerPositionSO.PlayerPositionSearch(gameManager.LevelModeNow(), gameManager.LevelTypeNow(), realFile.lastLevel, realFile.lastInGameMode, NamaPosisi.LevelFinish);

        // if(gameManager.LevelTypeNow() == LevelP3KType.Choking)
        // {
        //     positionNow = playerPosition[1].positionsInLevel[0].positions[3]; 
        // }
        // else if(gameManager.LevelTypeNow() == LevelP3KType.Bleeding)
        // {
        //     positionNow = playerPosition[1].positionsInLevel[1].positions[3]; 
        // }
        
        Vector3 destination = new Vector3(positionNow.playerPosition.x, playerTeleport_y, positionNow.playerPosition.z);
        Quaternion rotation = Quaternion.Euler(0,positionNow.playerRotation,0);
        playerTeleport.TeleportPlayer(destination, rotation);
        // Debug.Break();


        gameManager.ChangeGameState(GameState.Cinematic);
        PlayerRestriction.ApplyAllRestriction();

        QuestEndingUI.ShowQuestEnding();

    }

}
