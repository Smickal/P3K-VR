using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DirtyObject : MonoBehaviour
{
    const float MAX_POINT = 1f;

    [SerializeField] ETypeOfCleaner _typeOfCleaner;
    [SerializeField] ETypeOfCleaning _typeOfCleaning;
    [SerializeField] float _reduceMultiplier = 0.00625f;
    [SerializeField] Renderer _myModel;

    DirtyCleaner cleaner;
    bool isDoneCleaning = false;
    float curTransparancy;
    float curGauzePadTime;
    public bool IsDoneCleaning {  get { return isDoneCleaning; } }


    private void Start()
    {
        curTransparancy = MAX_POINT;
        curGauzePadTime = MAX_POINT;
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

        //Checks if the cleaningType has the same type as the dirty Object
        if (cleaner.GetTypeOfCleaning() != _typeOfCleaning)
        {
            Debug.Log($"FAILED_TO_CLEAN \n Cleaner = {cleaner.GetTypeOfCleaning()},\n Object {_typeOfCleaning}");
            return;
        }

        //Checks if the Cleaner has the same type as the object
        if(cleaner.GetTypeOfService() != _typeOfCleaner)
        {
            Debug.Log($"FAILED_TO_CLEAN \n Cleaner = {cleaner.GetTypeOfService()},\n Object {_typeOfCleaner}");
            return;
        }


        //Checks if already subscribed or no
        if(!IsSubscribed(cleaner.OnCleaning))
        {
            switch(_typeOfCleaner)
            {
                case ETypeOfCleaner.WaterBottle:
                    Debug.Log("registered WaterBootle");
                    cleaner.OnCleaning += ReduceTransparancyOnHit;                   
                    break;

                case ETypeOfCleaner.GauzePads:
                    Debug.Log("Registered GauzePads");
                    cleaner.OnCleaning += ReduceGauzeTime;
                    break;
            }

        }
    }

    public void UnRegisterCleaner()
    {

        switch(cleaner.GetTypeOfService())
        {
            case ETypeOfCleaner.WaterBottle:
                cleaner.OnCleaning -= ReduceTransparancyOnHit;
                break;

            case ETypeOfCleaner.GauzePads:
                cleaner.OnCleaning -= ReduceGauzeTime;
                break;
        }

        

        cleaner = null;
    }

    private void ReduceTransparancyOnHit()
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

    private void ReduceGauzeTime()
    {
        if(curGauzePadTime < 0f)
        {
            isDoneCleaning = true;
            Debug.Log($"Done Clean -> {name}");

            return;
        }
        Debug.Log("REDUCING GAUZE PAADS!");

        curGauzePadTime -= _reduceMultiplier;
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
