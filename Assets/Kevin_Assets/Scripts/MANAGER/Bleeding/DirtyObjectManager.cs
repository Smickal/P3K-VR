using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyObjectManager : MonoBehaviour
{
    public static DirtyObjectManager Instance {get; private set;}
    bool isDoneCleaning = false;
    public bool IsDoneCleaning {  get { return isDoneCleaning; } }
    [SerializeField]private List<DirtyObject> dirtyObjects = new List<DirtyObject>();
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddDirtyObject(DirtyObject obj)
    {
        foreach(DirtyObject dirtyObject in dirtyObjects)
        {
            if(dirtyObject == obj)return;
        }
        dirtyObjects.Add(obj);
    }

    public void CheckIsDoneCleaning()
    {
        foreach(DirtyObject dirtyObject in dirtyObjects)
        {
            if(!dirtyObject.IsDoneCleaning)return;
        }
        isDoneCleaning = true;
    }
    
}
