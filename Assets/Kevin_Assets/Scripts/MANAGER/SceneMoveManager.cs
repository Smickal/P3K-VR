using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneMoveManager : MonoBehaviour
{
    const string BGM_Tag = "BGM";
    [SerializeField]private List<ITurnOffStatic> turnOffStaticsList;
    public static Action<string> GoToAnotherScene;
    public BGMManager bGMManager;
    [SerializeField]ScreenFader screenFader;
    UnityAction questDoneRestartSceneAfterFade, questGoToSceneAfterFade;
    [SerializeField]PlayerHeightController playerHeightController;
    [SerializeField]PlayerManager playerManager;
    private void Awake() 
    {
        questDoneRestartSceneAfterFade = ()=>
        {
            if(playerManager.PlayerLastInGameMode() == InGame_Mode.NormalWalk)
            {
                playerHeightController.ResetHeight();
            }
            screenFader.ResetEvent();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
        ITurnOffStatic[] turnOffStaticsArray = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ITurnOffStatic>().ToArray();
        turnOffStaticsList = new List<ITurnOffStatic>(turnOffStaticsArray);

        bGMManager = GameObject.FindGameObjectWithTag(BGM_Tag).GetComponent<BGMManager>();
        // Debug.Log(bGMManager + "WHat");
    }
    
    public void RestartScene()
    {
        // Debug.Log("What??");
        TurnOffAllStatics();

        screenFader.AddEvent(questDoneRestartSceneAfterFade);
        screenFader.DoFadeIn();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToScene(string sceneName)
    {
        
        TurnOffAllStatics();

        //debug bntr yaa
        // SceneManager.LoadScene(sceneName);

        bGMManager.DestroyInstance();

        
        questGoToSceneAfterFade = ()=>
        {
            playerHeightController.ResetHeight();
            screenFader.ResetEvent();
            SceneManager.LoadScene(sceneName);
        };
        screenFader.AddEvent(questGoToSceneAfterFade);
        screenFader.DoFadeIn();

    }
    public void GoBackHome()
    {
        if(GameManager.CheckGameStateNow == null || GameManager.CheckInGameModeNow == null)return;
        if(GameManager.CheckGameStateNow() != GameState.InGame || GameManager.CheckInGameModeNow() == InGame_Mode.FirstAid) return;
        TurnOffAllStatics();
        bGMManager.DestroyInstance();
        questGoToSceneAfterFade = ()=>
        {
            screenFader.ResetEvent();
            SceneManager.LoadScene("Home");
        };
        screenFader.AddEvent(questGoToSceneAfterFade);
        screenFader.DoFadeIn();
    }
    public void GoBackHomeReset()
    {
        if(GameManager.CheckGameStateNow == null || GameManager.CheckInGameModeNow == null)return;
        if(GameManager.CheckGameStateNow() != GameState.InGame) return;
        TurnOffAllStatics();
        bGMManager.DestroyInstance();
        questGoToSceneAfterFade = ()=>
        {
            screenFader.ResetEvent();
            SceneManager.LoadScene("Home");
        };
        screenFader.AddEvent(questGoToSceneAfterFade);
        screenFader.DoFadeIn();
    }

    private void TurnOffAllStatics()
    {
        if(turnOffStaticsList == null)return;
        foreach(ITurnOffStatic turnOffStatic in turnOffStaticsList)
        {
            turnOffStatic.TurnOffStatic();
        }
    }
}
