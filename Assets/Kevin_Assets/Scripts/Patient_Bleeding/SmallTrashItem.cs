using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmallTrashItem : MonoBehaviour
{
    public UnityEvent OnDestroyTrash;
    private bool isTrash = false;
    [Tooltip("Nyalakan ini kalo ternyata barangnya 1 paket, baru jadikan trash kalo barangnya itu ud kebuka")]
    [SerializeField]private bool isBigCase = false;
    public bool IsBigCase{get{return isBigCase;}}
    
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
