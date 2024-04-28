using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class AlcoholWipeSnapScaleHelper : MonoBehaviour
{
    [SerializeField]SnapZone snapZone;
    [SerializeField]GameObject snapInteractorGO;
    [SerializeField]BoxCollider boxColliderSnapInteractor;
    [SerializeField]Vector3 changeCollCenter, changeCollSize;
    [SerializeField]Quaternion changeRotation;
    Vector3 startCollCenter, startCollSize;
    Quaternion startRotation;
    bool change = false, shouldChange = false;
    bool isFirstTime = true;
    public void ChangeColliderSize()
    {
        if(snapZone.HeldItem == null)return;
        
        // Debug.Log(snapInteractorGO.transform.localScale + " WHAT");
        if(snapZone.GetScaleTo != 0.1f)return;
        snapInteractorGO = snapZone.HeldItem.transform.Find("SnapInteractor").gameObject;
        boxColliderSnapInteractor = snapInteractorGO.GetComponent<BoxCollider>();

        if(isFirstTime)
        {
            isFirstTime = false;
            startCollCenter = boxColliderSnapInteractor.center;
            startCollSize = boxColliderSnapInteractor.size;
            startRotation = snapInteractorGO.transform.rotation;
        }
        

        change = true;

        boxColliderSnapInteractor.center = changeCollCenter;
        boxColliderSnapInteractor.size = changeCollSize;
        snapInteractorGO.transform.rotation = changeRotation;
    }

    public void ChangeBack()
    {
        if(!change)return;
        change = false;

        boxColliderSnapInteractor.center = startCollCenter;
        boxColliderSnapInteractor.size = startCollSize;
        snapInteractorGO.transform.rotation = startRotation;

        boxColliderSnapInteractor = null;
        snapInteractorGO = null;
    }
}
