using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMoveManager : MonoBehaviour
{
    const string BGM_Tag = "BGM";
    [SerializeField]private List<ITurnOffStatic> turnOffStaticsList;
    public static Action<string> GoToAnotherScene;
    public BGMManager bGMManager;
    private void Awake() 
    {
        ITurnOffStatic[] turnOffStaticsArray = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ITurnOffStatic>().ToArray();
        turnOffStaticsList = new List<ITurnOffStatic>(turnOffStaticsArray);

        bGMManager = GameObject.FindGameObjectWithTag(BGM_Tag).GetComponent<BGMManager>();
        // Debug.Log(bGMManager + "WHat");
    }
    
    public void RestartScene()
    {
        // Debug.Log("What??");
        TurnOffAllStatics();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToScene(string sceneName)
    {
        TurnOffAllStatics();

        //debug bntr yaa
        // SceneManager.LoadScene(sceneName);

        bGMManager.DestroyInstance();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
