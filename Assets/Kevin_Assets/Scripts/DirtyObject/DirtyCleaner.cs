using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirtyCleaner : MonoBehaviour
{
    [SerializeField] ETypeOfCleaning _typeOfCleaner;

    DirtyObject currentDirtyObj;
    GameObject currDirtyGameOBJ;
    public UnityAction OnCleaning;

    public ETypeOfCleaning GetTypeOfCleaner()
    {
        return _typeOfCleaner;
    }

    public void TryRegisterADirtyObject(GameObject obj)
    {


        if(currDirtyGameOBJ != obj && currDirtyGameOBJ != null)
        {
            //Release the prev OBJ
            currentDirtyObj.UnRegisterCleaner();
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

}
