using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirtyCleaner : MonoBehaviour
{
    [SerializeField] ETypeOfCleaner _typeOfService;
    [SerializeField] ETypeOfCleaning _typeOfCleaner;

    DirtyObject currentDirtyObj;
    GameObject currDirtyGameOBJ;
    public UnityAction OnCleaning;

    public ETypeOfCleaning GetTypeOfCleaning()
    {
        return _typeOfCleaner;
    }

    public ETypeOfCleaner GetTypeOfService()
    {
        return _typeOfService;
    }

    public void TryRegisterADirtyObject(GameObject obj)
    {


        if(currDirtyGameOBJ != obj && currDirtyGameOBJ != null)
        {
            //Release the prev OBJ
            currentDirtyObj.UnRegisterCleaner(this);
            currDirtyGameOBJ = null;
        }

        if(currDirtyGameOBJ == null)
        {
            Register(obj);
        }
    }

    private void Register(GameObject obj)
    {
        currDirtyGameOBJ = obj;
        currentDirtyObj = currDirtyGameOBJ.GetComponent<DirtyObject>();
        currentDirtyObj.RegisterTheCleanerType(this);
    }

    public void DestroyObject()
    {
        if(currDirtyGameOBJ != null)
        {
            //Release the prev OBJ
            currentDirtyObj.UnRegisterCleaner(this);
            currDirtyGameOBJ = null;
        }
    }

}
