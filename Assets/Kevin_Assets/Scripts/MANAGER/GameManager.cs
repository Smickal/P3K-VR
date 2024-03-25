using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField]private GameState state;
    [SerializeField]private InGame_Mode inGame_Mode;
    [SerializeField]private LevelMode levelMode;
    [SerializeField]private LevelP3KType levelType;
    private bool isPause;
    public UnityEvent OnPause, OnUnPause;
    public static Func<LevelMode> CheckLevelModeNow;
    public static Action<InGame_Mode> ChangeInGameModeNow;
    public static Action PauseGame;

    private void Awake() 
    {
        CheckLevelModeNow += levelModeNow;
        ChangeInGameModeNow += ChangeInGameMode;


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
    public LevelMode levelModeNow()
    {
        return levelMode;
    }
    public LevelP3KType levelTypeNow()
    {
        return levelType;
    }
    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            OnPause.Invoke();
            state = GameState.Pause;
        }
        else
        {
            OnUnPause.Invoke();
            state = GameState.InGame;
        }
    }

    public void ChangeInGameMode(InGame_Mode change)
    {
        inGame_Mode = change;
    }
}
