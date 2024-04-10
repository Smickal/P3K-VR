using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMoveManager : MonoBehaviour
{
    [SerializeField]private List<ITurnOffStatic> turnOffStaticsList;
    public static Action<string> GoToAnotherScene;
    private void Awake() 
    {
        ITurnOffStatic[] turnOffStaticsArray = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ITurnOffStatic>().ToArray();
        turnOffStaticsList = new List<ITurnOffStatic>(turnOffStaticsArray);
    }
    
    public void RestartScene()
    {
        Debug.Log("What??");
        TurnOffAllStatics();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToScene(string sceneName)
    {
        TurnOffAllStatics();
        SceneManager.LoadScene(sceneName);
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
