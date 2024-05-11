using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] protected QuestManagerUI questManagerUI;
    [SerializeField] private QuestYesNoUI questYesNoUI;
    protected LevelPlayerData levelPlayerDataNow; // ambil di awal utk punya data level
    protected LevelP3KType levelP3KTypeNow;
    [SerializeField]protected PlayerManager playerManager;
    [SerializeField]protected GameManager gameManager;
    
    [SerializeField]private Bleeding_QuestManager bleeding_QuestManager;
    [SerializeField]private Choking_QuestManager choking_QuestManager;
    [SerializeField]private SceneMoveManager sceneMoveManager;
    [SerializeField]protected DialogueManager dialogueManager;
    [SerializeField]Robot robot;

    

    [Header("Level Data")]
    [SerializeField]protected float timerInSecsMax;
    protected float timerInSecs;
    protected ScoreName score;
    [Header("Checker")]
    [SerializeField]protected bool isQuestStart;
    [SerializeField]protected bool isTimerUp;

    public UnityEvent OnStartQuest, OnFinishQuest;
    
    [Header("Debug Only")]
    public bool Startq;
    [Header("Scene Fade")]
    [SerializeField]ScreenFader screenFader;
    UnityAction questDoneMethodAfterFade;
    private bool hasClickRestartQuit;
    protected virtual void Awake() 
    {
        questDoneMethodAfterFade = QuestDoneMethod;
        levelP3KTypeNow = gameManager.LevelTypeNow();
        levelPlayerDataNow = playerManager.GetLevelData((int)levelP3KTypeNow);
        hasClickRestartQuit = false;
    }

    private void Start()
    {
        if(PlayerManager.LastInGameMode() == InGame_Mode.FirstAid)StartQuest();
    }

    protected virtual void Update() 
    {
        if(Startq)
        {
            Startq = false;
            Restart();
        }
        if(isQuestStart && gameManager.GameStateNow() == GameState.InGame && !screenFader.IsFading)
        {
            if(timerInSecs < timerInSecsMax)
            {
                timerInSecs += Time.deltaTime;
                questManagerUI.ChangeTimerSlider(timerInSecs);
            }
            else
            {
                isTimerUp = true;
                QuestDone();
                
            }
        }
    }
    public void CheckStartQuest()
    {
        if(!(PlayerManager.LastInGameMode() == InGame_Mode.FirstAid))
        {
            if(!levelPlayerDataNow.hasBeatenLevelOnce)
            {
                StartQuest();
            }
            else
            {
                questYesNoUI.ActivateUI();
                PlayerRestriction.ApplyAllRestriction();
            }
        }
        
    }
    public void YesNoStartQuest(bool choice)
    {
        PlayerRestriction.LiftAllRestriction();
        if(choice)
        {
            StartQuest();
            robot.DeactivateFollowPlayer();
        }
        
        questYesNoUI.CloseUI();
    }
    public void StartQuest()
    {
        OnStartQuest.Invoke();
        
        if(PlayerManager.LastInGameMode() != InGame_Mode.FirstAid)
        {
            PlayerManager.ChangeInGame_Mode_Now(InGame_Mode.FirstAid);
            PlayerManager.SetPlayerPosition_DoP3k();
            EnvironmentLevelManager.SetEnvironment_FirstAid();
            BGMManager.ChangeBGMAudio(BGM_Type.tense);
        }
        
        if(levelP3KTypeNow == LevelP3KType.Choking)
        {
            // PlayerRestriction.LiftAllRestriction();
            choking_QuestManager.Quest();
        }
        
        else if (levelP3KTypeNow == LevelP3KType.Bleeding)
        {
            // PlayerRestriction.LiftGrabableRestriction();
            bleeding_QuestManager.Quest();
        }
        

    }
    public void QuestDone() 
    {
        OnFinishQuest.Invoke();
        isQuestStart = false;
        PlayerManager.ChangeInGame_Mode_Now(InGame_Mode.NormalWalk);
        if(levelP3KTypeNow == LevelP3KType.Choking) choking_QuestManager.ScoreCounter();
        else if (levelP3KTypeNow == LevelP3KType.Bleeding) bleeding_QuestManager.ScoreCounter();
        PlayerRestriction.ApplyAllRestriction();

        screenFader.AddEvent(questDoneMethodAfterFade);
        screenFader.DoFadeIn();
        //ntr perlu dinyalain fader sendiri di sini
        

    }
    public void QuestDoneMethod()
    {
        questManagerUI.DeactivateBaseUI();
        questManagerUI.CloseHelper_Bleeding_WithoutItem();
        EnvironmentLevelManager.SetEnvironment_FinishQuest();
        PlayerManager.SetPlayerPosition_FinishP3k();
        screenFader.ResetEvent();
        screenFader.DoFadeOut();
    }
    protected virtual void Quest(){}
    protected virtual void ScoreCounter()
    {
        PlayerManager.HasBeatenLvl((int)levelP3KTypeNow, score);
        QuestEndingUI.SetUIData(score, levelP3KTypeNow);
        DataSaveManager.Instance.SaveScoreAndTime(levelP3KTypeNow, score, timerInSecs);
    }

    public virtual void Restart()
    {
        if(hasClickRestartQuit)return;
        hasClickRestartQuit = true;

        PlayerManager.ChangeInGame_Mode_Now(InGame_Mode.FirstAid);
        if(levelP3KTypeNow == LevelP3KType.Choking) choking_QuestManager.ResetQuest();
        else if (levelP3KTypeNow == LevelP3KType.Bleeding) bleeding_QuestManager.ResetQuest();
    }
    public virtual void QuitQuest()
    {
        if(hasClickRestartQuit)return;
        hasClickRestartQuit = true;

        PlayerManager.ChangeInGame_Mode_Now(InGame_Mode.NormalWalk);
        BGMManager.ChangeBGMAudio(BGM_Type.main);
        if(levelP3KTypeNow == LevelP3KType.Choking) choking_QuestManager.ResetQuest();
        else if (levelP3KTypeNow == LevelP3KType.Bleeding) bleeding_QuestManager.ResetQuest();
    }
    protected virtual void ResetQuest() 
    {
        // Debug.Log(sceneMoveManager.gameObject);
        
        sceneMoveManager.RestartScene();
    }

}
