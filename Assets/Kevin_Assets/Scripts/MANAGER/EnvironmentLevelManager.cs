using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLevelManager : MonoBehaviour, ITurnOffStatic
{
    [Tooltip("0- Pas Intro, 1 - biasa, 2 - first aid, 3- ending doang")]
    [SerializeField] private GameObject[] Environments;
    [SerializeField] private Transform[] robotPositionEveryEnvironment;
    [Header("Reference")]
    [SerializeField] ReturnRobotToStartingPos returnRobot;
    [SerializeField] ToolTipHomeManager toolTipHomeManager;
    public static Action SetEnvironment_FinishQuest, SetEnvironment_FirstAid, SetEnvironment_AfterIntro, SetEnvironment_HomeAfterIntro;
    private void Awake() 
    {
        SetEnvironment_FirstAid += SetEnvironmentFirstAid;
        SetEnvironment_FinishQuest += SetEnvironmentEndQuest;
        SetEnvironment_AfterIntro += SetEnvironmentAfterIntro;
        SetEnvironment_HomeAfterIntro += SetHomeAfterIntro;
    }
    
    public void SetEnvironmentAwake(bool hasFinishIntro_ThisLevel, bool isPlayerLastModeFirstAid)
    {
        CloseAllGameObject();
        if(isPlayerLastModeFirstAid)
        {
            Environments[2].SetActive(true);
            returnRobot.SetStartingPos(robotPositionEveryEnvironment[2]);
        }
        else
        {
            if(!hasFinishIntro_ThisLevel)
            {
                Environments[0].SetActive(true);
                returnRobot.SetStartingPos(robotPositionEveryEnvironment[0]);
            }
            else
            {
                Environments[1].SetActive(true);
                returnRobot.SetStartingPos(robotPositionEveryEnvironment[1]);
            }
        }
        
    }
    public void SetHomeAwake(bool hasFinishIntro_Home)
    {
        if(!hasFinishIntro_Home)
        {
            returnRobot.SetStartingPos(robotPositionEveryEnvironment[0]);
        }
        else
        {
            returnRobot.SetStartingPos(robotPositionEveryEnvironment[1]);
            toolTipHomeManager.Activate();
        }
    }
    public void SetHomeAfterIntro()
    {
        returnRobot.SetStartingPos(robotPositionEveryEnvironment[1]);
        toolTipHomeManager.Activate();
    }
    public void SetEnvironmentEndQuest()
    {
        CloseAllGameObject();
        Environments[3].SetActive(true);
        returnRobot.SetStartingPos(robotPositionEveryEnvironment[3]);
    }
    public void SetEnvironmentFirstAid()
    {
        // Debug.Log("SetEnvironmentFirstAid");
        CloseAllGameObject();
        Environments[2].SetActive(true);
        returnRobot.SetStartingPos(robotPositionEveryEnvironment[2]);
    }
    public void SetEnvironmentAfterIntro()
    {
        CloseAllGameObject();
        Environments[1].SetActive(true);
        returnRobot.OnlySetStartPos(robotPositionEveryEnvironment[1]);
    }
    public void CloseAllGameObject()
    {
        foreach (var obj in Environments)
        {
            obj.gameObject.SetActive(false);
        }
    }
    public void TurnOffStatic()
    {
        SetEnvironment_FirstAid -= SetEnvironmentFirstAid;
        SetEnvironment_FinishQuest -= SetEnvironmentEndQuest;
        SetEnvironment_AfterIntro -= SetEnvironmentAfterIntro;
        SetEnvironment_HomeAfterIntro -= SetHomeAfterIntro;
    }
}
