using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PatientBackBlowHeimlich : MonoBehaviour
{
    [SerializeField] RigBuilder _rig;
    [SerializeField] Grabber[] grabbers;

    [Header("Heimlich")]
    [SerializeField] HeimlichMovement _heimlichMove;
    [SerializeField] GameObject _heimlichContainerOBJ;
    [SerializeField] Rig _heimlichRig;
    [SerializeField] int _heimlichMaxCount = 5;
    private bool _heimlichDone;
    public bool HeimlichDone { get { return _heimlichDone; } }
    const string titleChokingHeimlich = "Heimlich";

    [Header("BackBlow")]
    [SerializeField] BackBlowMovement _backBlowMove;
    [SerializeField] GameObject _backBlowContainerOBJ;
    [SerializeField] Rig _backBlowRig;
    [SerializeField] int _backBlowMaxCount = 5;
    private bool _backBlowDone;
    public bool BackBlowDone { get { return _backBlowDone; } }
    const string titleChokingBackblow = "Backblow";


    [HideInInspector] public UnityEvent<int, float> OnHeimlichCountUp;
    [HideInInspector] public UnityEvent<int, float> OnBackBlowCountUp;

    public static Action UnActivateAllQuest;

    private void Awake() 
    {
        UnActivateAllQuest += UnActivateAll;
    }


    private void Start()
    {
        OnHeimlichCountUp.AddListener(CheckHeimlichCount);
        OnBackBlowCountUp.AddListener(CheckBackBlowCount);

        ActivateBackBlowRig();
    }

    public void ActivateBackBlowRig()
    {
        _heimlichContainerOBJ.SetActive(false);
        _backBlowContainerOBJ.SetActive(true);

        _heimlichRig.weight = 0f;
        _backBlowRig.weight = 0.75f;
        // _heimlichDone = false;
        // _backBlowDone = false;
        _heimlichMove.ResetCount();
        _backBlowMove.ResetCount();
        QuestManagerUI.ChangeHelperDesc_Choking(titleChokingBackblow);
    }

    public void ActivateHeimlich()
    {
        _heimlichContainerOBJ?.SetActive(true);
        _backBlowContainerOBJ.SetActive(false);

        _backBlowRig.weight = 0f;
        _heimlichRig.weight = 0.75f;
        // _heimlichDone = false;
        // _backBlowDone = false;
        _heimlichMove.ResetCount();
        _backBlowMove.ResetCount();
        QuestManagerUI.ChangeHelperDesc_Choking(titleChokingHeimlich);
    }
    public void UnActivateAll()
    {
        _heimlichContainerOBJ?.SetActive(false);
        _backBlowContainerOBJ?.SetActive(false);
    }

    private void CheckBackBlowCount(int count, float score)
    {
        if(count >= _backBlowMaxCount)
        {
            
            TurnOffGrabber();
            ActivateHeimlich();
            Choking_QuestManager.AddProgressBar(score);
            _backBlowDone = true;
            
        }
    }


    private void CheckHeimlichCount(int count, float score)
    {
        if(count >= _heimlichMaxCount)
        {
            
            TurnOffGrabber();
            ActivateBackBlowRig();
            Choking_QuestManager.AddProgressBar(score);
            _heimlichDone = true;
            
        }
    }

    private void TurnOffGrabber()
    {
        grabbers[0].TryRelease();
        grabbers[1].TryRelease();
        
    }
}
