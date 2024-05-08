using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using Oculus.Interaction.HandGrab;


public class PlayerRestriction : MonoBehaviour, ITurnOffStatic
{
    [Header("Reference")]
    [SerializeField]private GameManager gameManager;
    [SerializeField]private PlayerManager playerManager;
    [Tooltip("Kalo ada button yg ikut grabable, buttonnya jg masukkin ya")]
    [SerializeField]private List<Rigidbody> all_Grabable_Rigidbody;
    [SerializeField]private List<Grabber> playerHandController; //to prevent controller hand tetep pegang
    [SerializeField]private List<HandGrabInteractor> playerHandTrack;
    private List<Grabbable> all_BNG_Grabable;
    // [SerializeField]private Dictionary<Grabbable, bool> initial_BNG_GrabbableStates = new Dictionary<Grabbable, bool>();
    private List<HandGrabInteractable> all_OVR_Grabable;
    private List<DistanceHandGrabInteractable> all_OVR_DistanceGrabable;

    [SerializeField]private SmoothLocomotion playerMovement_BNG;
    [SerializeField]private GameObject[] playerMovements_OVR;
    [SerializeField]private PlayerRotation playerRotation_BNG;
    [SerializeField]private GameObject[] playerRotations_OVR;
    
    private bool isRestrictAll, isRestrictGrabable, isRestrictRotation, isRestrictMovement;
    public static Func<bool> IsRestrictAll, IsRestrictMovement, IsRestrictRotation, IsRestrictGrabable;
    public static Action<GameObject> AddData, RemoveData;

    public static Action LiftAllRestriction, ApplyAllRestriction, LiftGrabableRestriction, ApplyGrabableRestriction, LiftMovementRestriction, ApplyMovementRestriction, LiftRotationRestriction, ApplyRotationRestriction;
    
    [Header("Debug")]
    public bool enableNow;
    public bool DebugOnly;
    private void Awake() 
    {
        InitiateGrabableReference();
        AddData += AddExistingData;
        RemoveData += DeleteFromExistingData;

        LiftAllRestriction += EnableAll;
        ApplyAllRestriction += DisableAll;
        LiftGrabableRestriction += EnableAllGrabable;
        ApplyGrabableRestriction += DisableAllGrabable;
        LiftMovementRestriction += EnableAllMovement;
        ApplyMovementRestriction += DisableAllMovement;
        LiftRotationRestriction += EnableAllRotation;
        ApplyRotationRestriction += DisableAllRotation;

        IsRestrictAll += RestrictAll;
        IsRestrictMovement += RestrictMovement;
        IsRestrictGrabable += RestrictGrabable;
        IsRestrictRotation += RestrictRotation;


        if(!DebugOnly)
        {
            if(!playerManager.IsFinish_TutorialMain() && gameManager.LevelModeNow() == LevelMode.Home)
            {
                DisableAll();
            }
            else if(gameManager.LevelModeNow() == LevelMode.Level)
            {
                if(!playerManager.IsFinish_IntroLevel((int)gameManager.LevelTypeNow()))
                {
                    if(gameManager.LevelTypeNow() == LevelP3KType.Choking)
                    {
                        DisableAll();
                    }
                    else if(gameManager.LevelTypeNow() == LevelP3KType.Bleeding)
                    {
                        DisableAllGrabable();
                    }
                }
            }

        }
        
    }
    public void TurnOffStatic()
    {
        AddData -= AddExistingData;
        RemoveData -= DeleteFromExistingData;

        LiftAllRestriction -= EnableAll;
        ApplyAllRestriction -= DisableAll;
        LiftGrabableRestriction -= EnableAllGrabable;
        ApplyGrabableRestriction -= DisableAllGrabable;
        LiftMovementRestriction -= EnableAllMovement;
        ApplyMovementRestriction -= DisableAllMovement;
        LiftRotationRestriction -= EnableAllRotation;
        ApplyRotationRestriction -= DisableAllRotation;

        IsRestrictAll -= RestrictAll;
        IsRestrictMovement -= RestrictMovement;
        IsRestrictGrabable -= RestrictGrabable;
        IsRestrictRotation -= RestrictRotation;
        
    }
    private void Update() {
        if(enableNow)
        {
            enableNow = false;
            EnableAll();
        }
        // Debug.Log(playerMovement_BNG.enabled);
    }

    private bool RestrictAll(){return isRestrictAll;}
    private bool RestrictMovement(){return isRestrictMovement;}
    private bool RestrictGrabable(){return isRestrictGrabable;}
    private bool RestrictRotation(){return isRestrictRotation;}
    private void InitiateGrabableReference()
    {
        HandGrabInteractable[] grabbable_OVR_array = GameObject.FindObjectsOfType<HandGrabInteractable>();
        all_OVR_Grabable = new List<HandGrabInteractable>(grabbable_OVR_array);
        DistanceHandGrabInteractable[] distanceGrabbable_OVR_array = GameObject.FindObjectsOfType<DistanceHandGrabInteractable>();
        all_OVR_DistanceGrabable = new List<DistanceHandGrabInteractable>(distanceGrabbable_OVR_array);

        Grabbable[] grabbable_BNG_array = GameObject.FindObjectsOfType<Grabbable>();
        all_BNG_Grabable = new List<Grabbable>(grabbable_BNG_array);
        foreach (Grabbable grabbable in all_BNG_Grabable)
        {
            Rigidbody getRigid = grabbable.GetComponent<Rigidbody>();
            if(getRigid)all_Grabable_Rigidbody.Add(getRigid);
            // initial_BNG_GrabbableStates.Add(grabbable, grabbable.enabled);
        }
    }
    private void DisableAllGrabable()
    {
        isRestrictGrabable = true;
        foreach(Grabbable grabbable in all_BNG_Grabable)
        {
            grabbable.enabled = false;
        }
        foreach(Rigidbody rigidbody in all_Grabable_Rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        foreach(Grabber hand in playerHandController)
        {
            hand.TryRelease();
        }
        foreach(HandGrabInteractable handGrabInteractable in all_OVR_Grabable)
        {
            handGrabInteractable.enabled = false;
        }
        foreach(DistanceHandGrabInteractable distanceHandGrabInteractable in all_OVR_DistanceGrabable)
        {
            distanceHandGrabInteractable.enabled = false;
        }
        foreach(HandGrabInteractor hand in playerHandTrack)
        {
            hand.ForceRelease();
        }
        
    }
    private void EnableAllGrabable()
    {
        isRestrictGrabable = false;
        foreach(Grabbable grabbable in all_BNG_Grabable)
        {
            grabbable.enabled = true;
        }
        foreach(Rigidbody rigidbody in all_Grabable_Rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        foreach(HandGrabInteractable handGrabInteractable in all_OVR_Grabable)
        {
            handGrabInteractable.enabled = true;
        }
        foreach(DistanceHandGrabInteractable distanceHandGrabInteractable in all_OVR_DistanceGrabable)
        {
            if(!distanceHandGrabInteractable.IsInSnapZone())distanceHandGrabInteractable.enabled = true;
        }
        
    }

    private void DisableAllMovement()
    {
        isRestrictMovement = true;
        // Debug.Log("WHAT DO YOU MEAN THERE'S NO PLAYER MOVEMENT" + playerMovement_BNG + "???");
        if(playerMovement_BNG)playerMovement_BNG.enabled = false;
        // Debug.Log("test");
        // Debug.Log(playerMovements_OVR + " Kok bisa i;ang???");
        foreach(GameObject playermovement_OVR in playerMovements_OVR)
        {
            playermovement_OVR.SetActive(false);
        }
    }
    private void EnableAllMovement()
    {
        isRestrictMovement = false;

        if(playerMovement_BNG)playerMovement_BNG.enabled = true;
        foreach(GameObject playermovement_OVR in playerMovements_OVR)
        {
            playermovement_OVR.SetActive(true);
        }
    }

    private void DisableAllRotation()
    {
        isRestrictRotation = true;

        if(playerRotation_BNG)playerRotation_BNG.enabled = false;
        foreach(GameObject playerRotation_OVR in playerRotations_OVR)
        {
            playerRotation_OVR.SetActive(false);
        }
    }
    private void EnableAllRotation()
    {
        isRestrictRotation = false;

        if(playerRotation_BNG)playerRotation_BNG.enabled = true;
        foreach(GameObject playerRotation_OVR in playerRotations_OVR)
        {
            playerRotation_OVR.SetActive(true);
        }
    }

    private void DisableAll()
    {
        isRestrictAll = true;
        DisableAllMovement();
        DisableAllGrabable();
    }
    private void EnableAll()
    {
        isRestrictAll = false;
        EnableAllMovement();
        EnableAllGrabable();
    }
    public void AddExistingData(GameObject gameObject)
    {
        HandGrabInteractable[] grabs = gameObject.GetComponentsInChildren<HandGrabInteractable>(true);
        foreach(HandGrabInteractable grab in grabs)
        {
            if(!all_OVR_Grabable.Contains(grab))
            {
                all_OVR_Grabable.Add(grab);
                Debug.Log("I add handgrab " + gameObject);
            }  
            
        }

        DistanceHandGrabInteractable[] distanceGrabs = gameObject.GetComponentsInChildren<DistanceHandGrabInteractable>(true);
        foreach(DistanceHandGrabInteractable grab in distanceGrabs)
        {
            if(!all_OVR_DistanceGrabable.Contains(grab))
            {
                all_OVR_DistanceGrabable.Add(grab);
                Debug.Log("I add handgrabdis " + gameObject);
            }
            
        }

        Grabbable[] grabBNGs = gameObject.GetComponentsInChildren<Grabbable>(true);
        foreach(Grabbable grab in grabBNGs)
        {
            if(!all_BNG_Grabable.Contains(grab))
            {
                all_BNG_Grabable.Add(grab);
                Debug.Log("I add bngGrab " + gameObject);
            }
        }

        foreach (Grabbable grabbable in grabBNGs)
        {
            Rigidbody getRigid = grabbable.GetComponent<Rigidbody>();
            if(getRigid)
            {
                if(!all_Grabable_Rigidbody.Contains(getRigid))
                {
                    all_Grabable_Rigidbody.Add(getRigid);
                    Debug.Log("I add rb " + gameObject);
                }
                
            }
            // if(!initial_BNG_GrabbableStates.ContainsKey(grabbable))initial_BNG_GrabbableStates.Add(grabbable, grabbable.enabled);
        }
    }
    public void DeleteFromExistingData(GameObject gameObject)
    {
        HandGrabInteractable[] grabs = gameObject.GetComponentsInChildren<HandGrabInteractable>(true);
        foreach(HandGrabInteractable grab in grabs)
        {
            if(all_OVR_Grabable.Contains(grab))
            {
                all_OVR_Grabable.Remove(grab);
                Debug.Log("I remove handgrab " + grab+ gameObject);
            }
            
        }

        DistanceHandGrabInteractable[] distanceGrabs = gameObject.GetComponentsInChildren<DistanceHandGrabInteractable>(true);
        foreach(DistanceHandGrabInteractable grab in distanceGrabs)
        {
            if(all_OVR_DistanceGrabable.Contains(grab))
            {
                all_OVR_DistanceGrabable.Remove(grab);
                Debug.Log("I remove handgrabdis " + grab + gameObject);
            }
        }

        Grabbable[] grabBNGs = gameObject.GetComponentsInChildren<Grabbable>(true);
        foreach(Grabbable grab in grabBNGs)
        {
            if(all_BNG_Grabable.Contains(grab))
            {
                all_BNG_Grabable.Remove(grab);
                Debug.Log("I remove rb " + grab+ gameObject);
            }
        }

        foreach (Grabbable grabbable in grabBNGs)
        {
            Rigidbody getRigid = grabbable.GetComponent<Rigidbody>();
            if(getRigid)
            {
                if(all_Grabable_Rigidbody.Contains(getRigid))
                {
                    all_Grabable_Rigidbody.Remove(getRigid);
                    Debug.Log("I remove rb " + getRigid+ gameObject);
                }
            }
            // if(initial_BNG_GrabbableStates.ContainsKey(grabbable))initial_BNG_GrabbableStates.Remove(grabbable);
        }
    }
}
