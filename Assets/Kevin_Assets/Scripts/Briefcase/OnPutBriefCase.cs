using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using BNG;
using Unity.Mathematics;

public class OnPutBriefCase : MonoBehaviour
{
    [SerializeField]GameObject checker;
    [SerializeField]Collider coll;
    private bool isThereBriefCase, isMovingTowardsPlace;
    public bool IsThereBriefCase{get{return isThereBriefCase;}}
    Rigidbody rb;
    Briefcase briefcase;
    BriefCaseInteractableEvent briefInteractable;
    [SerializeField]private Transform positionBriefCase;
    [SerializeField]private Quaternion rotationBriefCase;
    [SerializeField] float _lerpSpeed = 15f;
    [SerializeField] float _snapDistance = 0.05f;
    private void Update() 
    {
        if(!isMovingTowardsPlace)return;
        if(briefcase.transform.position == positionBriefCase.transform.position)return;
        if(briefcase.transform.position != positionBriefCase.transform.position)
        {
            
            briefcase.transform.position = positionBriefCase.transform.position;
            briefcase.transform.rotation = rotationBriefCase;
        } 
        
    }
    public void PutInPlace(GameObject briefCase)
    {
        isThereBriefCase = true;

        briefInteractable.TurnOffAll();
        rb = briefCase.GetComponent<Rigidbody>();
        briefcase = briefCase.GetComponent<Briefcase>();
        

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        briefcase.transform.position = positionBriefCase.transform.position;
        briefcase.transform.rotation = rotationBriefCase;
        if(briefcase)briefcase.ChangeButtonCollEnableOnPlace(true);
        checker.SetActive(false);
        coll.enabled = false;
        isMovingTowardsPlace = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(isThereBriefCase)return;
        if(other.gameObject.name.Contains("BriefCase"))
        {
            // Debug.Log("yes");
            if(other.gameObject.transform.parent != null && other.gameObject.transform.parent.GetComponent<SnapZone>() != null) return;
            // Debug.Log("lewat");
            Grabbable grabHere = other.GetComponent<Grabbable>();
            // IsBeingGrabHandTrack _isBeing = other.GetComponent<IsBeingGrabHandTrack>();
            // if((grabHere != null && grabHere.BeingHeld) || (_isBeing != null && _isBeing.IsBeingGrab()))return;
            // Debug.Log("lewat");
            briefInteractable = other.gameObject.GetComponent<BriefCaseInteractableEvent>();

            Grabber grabber = grabHere.GetPrimaryGrabber();
            if(grabber != null)grabber.TryRelease();
            briefInteractable.ReleaseHandGrabNow();
            
            PutInPlace(other.gameObject);
        }
    }

}
