using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLevelManager : MonoBehaviour
{
    [Tooltip("0- Pas Intro, 1 - biasa, 2- ending doang")]
    [SerializeField] private GameObject[] Environments;
    public static Action SetEnvironment_FinishQuest;
    private void Awake() 
    {
        SetEnvironment_FinishQuest += SetEnvironmentEndQuest;
    }
    public void SetEnvironmentAwake(bool hasFinishIntro_ThisLevel)
    {
        CloseAllGameObject();
        if(!hasFinishIntro_ThisLevel)Environments[0].SetActive(true);
        else Environments[1].SetActive(true);
    }
    public void SetEnvironmentEndQuest()
    {
        CloseAllGameObject();
        Environments[2].SetActive(true);
    }
    public void CloseAllGameObject()
    {
        foreach (var obj in Environments)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
