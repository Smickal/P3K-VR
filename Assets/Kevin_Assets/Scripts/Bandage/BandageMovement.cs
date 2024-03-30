using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageMovement : MonoBehaviour
{
    [SerializeField] float _detectionZone = 0.01f;


    [Header("Reference")]
    [SerializeField] SnapZone _snapZone;
    [SerializeField] CircleMotionTransform _circleTransform;
    [SerializeField] BandageDraw _bandageDraw;

    int currentIdx;
    int targetIdx;

    Bandage currBandage;
    Grabbable curBandageGrabbable;




    bool isMoving = false;

    private void Update()
    {

        if (curBandageGrabbable == null) return;  
        if (!isMoving || curBandageGrabbable.BeingHeld == false) return;


        //check if bandage in range of next target pos
        if (Vector3.Distance(curBandageGrabbable.transform.position, _circleTransform.CircleTransforms[targetIdx].position) <= _detectionZone)
        {
            _bandageDraw.CreateMeshesByIndex(0, targetIdx);

            currentIdx++;
            SetSnapZoneToIdx(currentIdx);


            targetIdx = currentIdx + 1;

            //make execption incase array idx overflow!
            if (targetIdx >= _circleTransform.CircleTransforms.Count - 1)
            {
                //DONE BANDAGE MOVEMENT
                _bandageDraw.CreateMeshesByIndex(0, _circleTransform.CircleTransforms.Count - 1);
                Debug.Log("BANDAGE MOVEMENT DONE!");
                isMoving = false;


                _snapZone.gameObject.SetActive(false);                
            }


        }

        //Draw bandage from current transform idx to bandage
        if(currentIdx < _circleTransform.CircleTransforms.Count - 1)
        _circleTransform.MoveCustomTransform(currBandage.StartingMeshTransform, currBandage.LeftMaxTransform, currBandage.RightMaxTransform);
        _bandageDraw.CreateCustomMeshByPositions(_circleTransform.CircleTransforms[currentIdx]);


    }


    public void SetSnapZoneToIdx(int idx)
    {
        if (idx == _circleTransform.CircleTransforms.Count - 1) return;

        _snapZone.transform.position = _circleTransform.CircleTransforms[idx].position;
    }

 

    public void RegisterBandage(Bandage bandage)
    {
        if (currBandage != null) return;
        //1. Make BandageSnapToStartingPosiition
        //2. Make Bandage return to latest snapPosition when grab is released

        currBandage = bandage;
        curBandageGrabbable = currBandage.GetComponent<Grabbable>();

        _snapZone.HeldItem = curBandageGrabbable;
        currBandage.GetComponent<ReturnToSnapZone>().ReturnTo = _snapZone;
        currBandage.RegisterBandageToMove(this);

        ResetTargeting();
        ResetSnapPositionToStartingIdx();

        curBandageGrabbable.GetPrimaryGrabber().TryRelease();
    }

    private void ResetSnapPositionToStartingIdx()
    {
        _snapZone.transform.position = _circleTransform.CircleTransforms[0].position;
        _bandageDraw.DeleteAllMeshes();
    }

    private void ResetTargeting()
    {
        currentIdx = 0;
        targetIdx = currentIdx + 1;       
    }

    public void ActivateBandageMovement()
    {
        isMoving = true;
    }

    public void DisableBandageMovement()
    {
        isMoving = false;
        _snapZone.HeldItem = curBandageGrabbable;
    }

    public void DeleteCustomPosMesh()
    {
        if (curBandageGrabbable == null) return;
        _bandageDraw.DeleteCustomPosMesh();
    }
}

