using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System;
using UnityEngine.Events;
using TMPro;


public class GameManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private GameState state;
    [SerializeField]private InGame_Mode inGame_Mode;
    [SerializeField]private LevelMode levelMode;
    [SerializeField]private LevelP3KType levelType;
    [SerializeField]private ScreenFader screenFader;
    [SerializeField]private PlayerInventoryController playerInventoryController;
    private bool isPause;
    public UnityEvent OnPause, OnUnPause;
    public static Func<LevelMode> CheckLevelModeNow;
    public static Func<GameState> CheckGameStateNow;
    public static Func<LevelP3KType> CheckLevelTypeNow;
    public static Func<InGame_Mode> CheckInGameModeNow;
    public static Action<InGame_Mode> ChangeInGameModeNow;
    public static Action PauseGame;
    public static Action<GameState> ChangeGameStateNow;
    // [SerializeField]TMP_Text tMP_Text;

    private void Awake() 
    {
        CheckLevelModeNow += LevelModeNow;
        ChangeInGameModeNow += ChangeInGameMode;
        CheckInGameModeNow += InGame_ModeNow;

        CheckGameStateNow += GameStateNow;
        CheckLevelTypeNow += LevelTypeNow;
        ChangeGameStateNow += ChangeGameState;
        PauseGame += Pause;
        
    }
    private void Start() 
    {
        playerInventoryController.ChangeEnable();
        OnPause.AddListener(
            ()=>
            {
                PlayerRestriction.ApplyAllRestriction();
            }
        );
        OnUnPause.AddListener(
            ()=>
            {
                PlayerRestriction.LiftAllRestriction();
            }
        );
    }

    public GameState GameStateNow()
    {
        return state;
    }
    public InGame_Mode InGame_ModeNow()
    {
        return inGame_Mode;
    }
    public LevelMode LevelModeNow()
    {
        return levelMode;
    }
    public LevelP3KType LevelTypeNow()
    {
        return levelType;
    }
    public void ChangeGameState(GameState changeState)
    {
        state = changeState;
    }
    public void Pause()
    {
        if(state == GameState.Cinematic)
        {
            return;
        }
        isPause = !isPause;
        if(isPause)
        {
            state = GameState.Pause;
            screenFader.SetFadeLevel(0.2f);
            OnPause.Invoke();
        }
        else
        {
            OnUnPause.Invoke();
            screenFader.SetFadeLevel(0f);
            state = GameState.InGame;
        }
    }

    public void ChangeInGameMode(InGame_Mode change)
    {
        inGame_Mode = change;
        playerInventoryController.ChangeEnable();
    }
    public void TurnOffStatic()
    {
        CheckLevelModeNow -= LevelModeNow;
        ChangeInGameModeNow -= ChangeInGameMode;
        CheckGameStateNow -= GameStateNow;
        CheckLevelTypeNow -= LevelTypeNow;
        ChangeGameStateNow -= ChangeGameState;
        PauseGame -= Pause;
        CheckInGameModeNow -= InGame_ModeNow;
    }
}
