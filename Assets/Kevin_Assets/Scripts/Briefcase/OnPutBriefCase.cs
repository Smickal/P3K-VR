using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using BNG;
using Unity.Mathematics;

public class OnPutBriefCase : MonoBehaviour
{
    [SerializeField]GameObject checker;
    private bool isThereBriefCase, isMovingTowardsPlace;
    public bool IsThereBriefCase{get{return isThereBriefCase;}}
    Rigidbody rb;
    Briefcase briefcase;
    [SerializeField]HandGrabInteractable[] grabs;
    [SerializeField]DistanceHandGrabInteractable[] distanceGrabs;
    [SerializeField] Grabbable[] grabBNGs;
    [SerializeField]private Transform positionBriefCase;
    [SerializeField]private Quaternion rotationBriefCase;
    [SerializeField] float _lerpSpeed = 15f;
    [SerializeField] float _snapDistance = 0.05f;
    private void Update() 
    {
        if(!isMovingTowardsPlace)return;
        Vector3 moveDir = positionBriefCase.position - briefcase.transform.position;

        //rigid.AddForce(moveDir * Time.deltaTime * _lerpSpeed, ForceMode.Force);
        rb.velocity = moveDir * Time.deltaTime * _lerpSpeed;

        //transform.position = Vector3.MoveTowards(transform.position, _startingPos.transform.position, Time.deltaTime * _lerpSpeed);



        if (Vector3.Distance(briefcase.transform.position, positionBriefCase.transform.position) <= _snapDistance)
        {
            isMovingTowardsPlace = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            briefcase.transform.position = positionBriefCase.transform.position;
            briefcase.transform.rotation = rotationBriefCase;
            if(briefcase)briefcase.ChangeButtonCollEnableOnPlace(true);
            checker.SetActive(false);
        }  
    }
    public void PutInPlace(GameObject briefCase)
    {
        isThereBriefCase = true;
        PlayerRestriction.RemoveData(briefCase);
        
        grabs = briefCase.GetComponentsInChildren<HandGrabInteractable>(true);
        foreach(HandGrabInteractable grab in grabs)
        {
            grab.enabled = false;
        }
        distanceGrabs = briefCase.GetComponentsInChildren<DistanceHandGrabInteractable>(true);
        foreach(DistanceHandGrabInteractable grab in distanceGrabs)
        {
            grab.enabled = false;
        }

        grabBNGs = briefCase.GetComponentsInChildren<Grabbable>(true);
        foreach(Grabbable grab in grabBNGs)
        {
            grab.enabled = false;
        }
        rb = briefCase.GetComponent<Rigidbody>();
        briefcase = briefCase.GetComponent<Briefcase>();
        isMovingTowardsPlace = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(isThereBriefCase)return;
        if(other.gameObject.name.Contains("BriefCase"))
        {
            Debug.Log("yes");
            if(other.gameObject.transform.parent != null && other.gameObject.transform.parent.GetComponent<SnapZone>() != null) return;
            Debug.Log("lewat");
            Grabbable grabHere = other.GetComponent<Grabbable>();
            // IsBeingGrabHandTrack _isBeing = other.GetComponent<IsBeingGrabHandTrack>();
            // if((grabHere != null && grabHere.BeingHeld) || (_isBeing != null && _isBeing.IsBeingGrab()))return;
            // Debug.Log("lewat");
            BriefCaseInteractableEvent brief = other.gameObject.GetComponent<BriefCaseInteractableEvent>();
            if(grabHere != null && grabHere.BeingHeld) 
            {
                Grabber grabber = grabHere.GetPrimaryGrabber();
                if(grabber != null)grabber.TryRelease();
            }
            else
            {
                brief.ReleaseHandGrabNow();
            }
            PutInPlace(other.gameObject);
        }
    }

}
