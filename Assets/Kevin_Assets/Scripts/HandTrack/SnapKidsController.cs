using System.Collections;
using System.Collections.Generic;
using BNG;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class SnapKidsController : MonoBehaviour
{
    BNG.Grabbable grab;
    IsBeingGrabHandTrack isBeingGrabHand;
    [SerializeField]SnapZone[] snapZoneChilds;
    private Dictionary<SnapZone, Collider> snapZoneColliderDictionary = new Dictionary<SnapZone, Collider>();
    private void Start()
    {
        grab = GetComponent<BNG.Grabbable>();
        isBeingGrabHand = GetComponent<IsBeingGrabHandTrack>();
        CheckEnabled();
    }

    private void Update() 
    {
        CheckEnabled();
    }
    public void CheckEnabled()
    {
        if(grab.BeingHeld || (isBeingGrabHand && isBeingGrabHand.IsBeingGrab()))
        {
            foreach(SnapZone snapzone in snapZoneChilds)
            {
                if(!snapZoneColliderDictionary.ContainsKey(snapzone))continue;
                if(snapZoneColliderDictionary[snapzone].enabled == false)snapZoneColliderDictionary[snapzone].enabled = true;
            }
        }
        else if (!grab.BeingHeld || (isBeingGrabHand && !isBeingGrabHand.IsBeingGrab()))
        {
            foreach(SnapZone snapzone in snapZoneChilds)
            {
                if(!snapZoneColliderDictionary.ContainsKey(snapzone))continue;
                if(snapZoneColliderDictionary[snapzone].enabled)snapZoneColliderDictionary[snapzone].enabled = false;
            }
        }
        
    }
    public void AddSnapZoneCollider(SnapZone snapZone)
    {
        if(!snapZoneColliderDictionary.ContainsKey(snapZone))
        {
            GameObject snapInteractor = snapZone.HeldItem.transform.Find("SnapInteractor").gameObject;
            Collider coll = snapInteractor.GetComponent<Collider>();
            coll.enabled = false;
            snapZoneColliderDictionary.Add(snapZone, coll);
        }
    }
    public void RemoveSnapZoneCollider(SnapZone snapZone)
    {
        if(snapZoneColliderDictionary.ContainsKey(snapZone))
        {
            snapZoneColliderDictionary[snapZone].enabled = false;
            snapZoneColliderDictionary.Remove(snapZone);
        }
    }
}
