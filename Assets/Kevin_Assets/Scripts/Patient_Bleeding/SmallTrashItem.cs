using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmallTrashItem : MonoBehaviour
{
    public UnityEvent OnDestroyTrash;
    private bool isTrash = false;
    
    public void DestroyTrash()
    {
        OnDestroyTrash?.Invoke();
        Destroy(this.gameObject);
    }
    public void AddTrash()
    {
        if(!isTrash)
        {
            isTrash = true;
            if(TrashCountManager.Instance)TrashCountManager.Instance.AddTrash(this);
        }
    }
    public void RemoveTrashFromData()
    {
        if(isTrash)
        {
            isTrash = false;
            if(TrashCountManager.Instance)TrashCountManager.Instance.RemoveTrashFromData(this);
        }
    }

}
