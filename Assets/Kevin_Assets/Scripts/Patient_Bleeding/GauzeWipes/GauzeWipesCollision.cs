using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class GauzeWipesCollision : MonoBehaviour
{
    const string DirtyGauzeName = "GauzeWipes_Dirty";
    DirtyCleaner cleaner;
    GauzeWipes gauzeWipes;
    [SerializeField]Grabbable grabbable;
    [SerializeField]IsBeingGrabHandTrack isBeingGrabHandTrack;
    private bool isChange;

    public void RegisterCleaner(GauzeWipes gauzeWipes, DirtyCleaner cleaner)
    {
        this.cleaner = cleaner;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (cleaner == null || collision.gameObject.GetComponent<DirtyObject>() == null) return;
        // Debug.Log("Masukkk");
        cleaner.TryRegisterADirtyObject(collision.gameObject);
     
    }

    private void OnCollisionStay(Collision collision)
    {
        if (cleaner == null || collision.gameObject.GetComponent<DirtyObject>() == null) return;
        if(!isChange && collision.gameObject.GetComponent<DirtyObject>().IsDoneCleaning == false)
        {
            isChange = true;
            gameObject.name = DirtyGauzeName;
        }
        if(grabbable.BeingHeld || isBeingGrabHandTrack.IsBeingGrab())cleaner.OnCleaning?.Invoke();
        
    }

    private void OnTriggerEnter(Collider other) {
        if (cleaner == null || other.gameObject.GetComponent<DirtyObject>() == null) return;
        // Debug.Log("Masukkk");
        cleaner.TryRegisterADirtyObject(other.gameObject);
    }
    private void OnTriggerStay(Collider other) {
        if (cleaner == null || other.gameObject.GetComponent<DirtyObject>() == null) return;
        if(!isChange && other.gameObject.GetComponent<DirtyObject>().IsDoneCleaning == false)
        {
            isChange = true;
            gameObject.name = DirtyGauzeName;
        }
        if(grabbable.BeingHeld || isBeingGrabHandTrack.IsBeingGrab())cleaner.OnCleaning?.Invoke();
    }
}
