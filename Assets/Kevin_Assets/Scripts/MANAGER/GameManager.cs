using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InGame, Pause, Cinematic
}
public enum InGame_Mode
{
    NormalWalk, FirstAid
}
public enum LevelMode
{
    Home, Level
}
public class GameManager : MonoBehaviour
{
    [SerializeField]private GameState state;
    [SerializeField]private InGame_Mode inGame_Mode;
    [SerializeField]private LevelMode levelMode;
    private bool isPause;
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
    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            state = GameState.Pause;
        }
        else
        {
            state = GameState.InGame;
        }
    }
}
