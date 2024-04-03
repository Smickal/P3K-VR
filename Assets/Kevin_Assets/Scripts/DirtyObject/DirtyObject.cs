using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirtyObject : MonoBehaviour
{
    const float MAX_TRANSPARANCY = 1f;

    [SerializeField] ETypeOfCleaning _typeOfCleaning;
    [SerializeField] float _reduceMultiplier = 0.00625f;
    [SerializeField] Renderer _myModel;

    DirtyCleaner cleaner;
    bool isDoneCleaning = false;
    float curTransparancy;

    public bool IsDoneCleaning {  get { return isDoneCleaning; } }


    private void Start()
    {
        curTransparancy = MAX_TRANSPARANCY;
    }

    private void SetTransparancy(float transparancy)
    {
        Color color = _myModel.material.color;
        color.a = transparancy / 1f;
        _myModel.material.color = color;
    }

    public void RegisterTheCleanerType(DirtyCleaner cleaner)
    {
        this.cleaner = cleaner;

        //Checks if the cleaner has the same type as the dirty Object
        if (cleaner.GetTypeOfCleaner() != _typeOfCleaning)
        {
            Debug.Log($"FAILED_TO_CLEAN \n Cleaner = {cleaner.GetTypeOfCleaner()},\n Object {_typeOfCleaning}");
            return;
        }


        //Checks if already subscribed or no
        if(!IsSubscribed(cleaner.OnCleaning))
        {
            Debug.Log("registered Cleaner");
            cleaner.OnCleaning += ReduceTransparancyOnHit;
        }
    }

    public void UnRegisterCleaner()
    {
        cleaner.OnCleaning -= ReduceTransparancyOnHit;

        cleaner = null;
    }

    public void ReduceTransparancyOnHit()
    {
        if (curTransparancy < 0f)
        {
            //TRIGGER DONE CLEANING HERE!
            isDoneCleaning = true;
            Debug.Log($"Done Clean -> {name}");

            return;
        }
        Debug.Log("REDUCING TRANPARANCY!!");

        curTransparancy -= _reduceMultiplier;
        SetTransparancy(curTransparancy);      
    }

    private bool IsSubscribed(UnityAction action)
    {
        if (action == null)
        {
            return false;
        }

        var InvocationList = action.GetInvocationList();

        foreach (var Entry in InvocationList)
        {
            if (Entry.Equals(action))
            {
                return true;
            }
        }

        return false;
    }
}
