using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PatientBackBlowHeimlich : MonoBehaviour
{
    [SerializeField] RigBuilder _rig;
    [SerializeField] Choking_QuestManager choking_QuestManager;
    public AudioClip SoundOnDone;
    [SerializeField] Grabber[] grabbers;
    [SerializeField] HandGrabInteractor[] grabbersHandTrack;

    [Header("Heimlich")]
    [SerializeField] HeimlichMovement _heimlichMove;
    [SerializeField] GameObject _heimlichContainerOBJ;
    [SerializeField] Rig _heimlichRig;
    [SerializeField] int _heimlichMaxCount = 5;
    [SerializeField] float _heimlichWeight = 0.4f;
    private bool _heimlichDone;
    public bool HeimlichDone { get { return _heimlichDone; } }
    const string titleChokingHeimlich = "Heimlich";

    [Header("BackBlow")]
    [SerializeField] BackBlowMovement _backBlowMove;
    [SerializeField] GameObject _backBlowContainerOBJ;
    [SerializeField] Rig _backBlowRig;
    [SerializeField] int _backBlowMaxCount = 5;
    [SerializeField] float _backblowWeight = 0.5f;
    private bool _backBlowDone;
    public bool BackBlowDone { get { return _backBlowDone; } }
    const string titleChokingBackblow = "Backblow";


    [HideInInspector] public UnityEvent<int, float> OnHeimlichCountUp;
    [HideInInspector] public UnityEvent<int, float> OnBackBlowCountUp;

    private void Start()
    {
        OnHeimlichCountUp.AddListener(CheckHeimlichCount);
        OnBackBlowCountUp.AddListener(CheckBackBlowCount);

        ActivateBackBlowRig();
    }
    int i =0;
    public void ActivateBackBlowRig()
    {
        Debug.Log("ini i " + i + " cek");
        i++;
        // _heimlichContainerOBJ.SetActive(false);
        // _backBlowContainerOBJ.SetActive(true);

        _heimlichRig.weight = 0f;
        _backBlowRig.weight = _backblowWeight;
        // _heimlichDone = false;
        // _backBlowDone = false;
        _heimlichMove.ResetCount();
        _backBlowMove.ResetCount();
        choking_QuestManager.PlayDialogueBackBlow();
        QuestManagerUI.ChangeHelperDesc_Choking(titleChokingBackblow);
    }

    public void ActivateHeimlich()
    {
        // _heimlichContainerOBJ?.SetActive(true);
        // _backBlowContainerOBJ.SetActive(false);

        _backBlowRig.weight = 0f;
        _heimlichRig.weight = _heimlichWeight;
        // _heimlichDone = false;
        // _backBlowDone = false;
        _heimlichMove.ResetCount();
        _backBlowMove.ResetCount();
        choking_QuestManager.PlayDialogueHeimlich();
        QuestManagerUI.ChangeHelperDesc_Choking(titleChokingHeimlich);
    }
    public void UnActivateAll()
    {
        // TurnOffGrabber();
        _heimlichContainerOBJ?.SetActive(false);
        _backBlowContainerOBJ?.SetActive(false);
    }
    public void ActivateContainerBackblowOnly()
    {
        _heimlichContainerOBJ.SetActive(false);
        _backBlowContainerOBJ.SetActive(true);
    }
    public void ActivateContainerHeimlichOnly()
    {
        _heimlichContainerOBJ?.SetActive(true);
        _backBlowContainerOBJ.SetActive(false);
    }

    private void CheckBackBlowCount(int count, float score)
    {
        if(count >= _backBlowMaxCount)
        {
            PlaySoundCorrect();
            ActivateContainerHeimlichOnly();
            TurnOffGrabber();
            
            Choking_QuestManager.AddProgressBar(score);
            ActivateHeimlich();
            _backBlowDone = true;
            
        }
    }


    private void CheckHeimlichCount(int count, float score)
    {
        if(count >= _heimlichMaxCount)
        {
            PlaySoundCorrect();
            ActivateContainerBackblowOnly();
            TurnOffGrabber();
            
            Choking_QuestManager.AddProgressBar(score);
            ActivateBackBlowRig();
            _heimlichDone = true;
            
        }
    }

    private void TurnOffGrabber()
    {
        if(!InteractToolsController.CheckIsHandTrackOn())
        {
            grabbers[0].TryRelease();
            grabbers[1].TryRelease();
        }
        else
        {
            grabbersHandTrack[0].ForceRelease();
            grabbersHandTrack[1].ForceRelease();
        }
        
    }
    private void PlaySoundCorrect()
    {
        if (SoundOnDone) {
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnDone, transform.position, 0.75f);
            }
        }
    }

}
