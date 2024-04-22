using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class SnapZoneSmallTrash : MonoBehaviour
{
    [SerializeField]SnapZone snapZone;
    SmallTrashItem smallTrashItem;
    private void Awake() {
        if(snapZone == null)snapZone = GetComponent<SnapZone>();
    }
    public void AddTrashSnapOut()
    {
        if(smallTrashItem == null)return;
        Debug.Log("Ada Sampah" + smallTrashItem.name);
        smallTrashItem.AddTrash();
        smallTrashItem = null;
    }
    public void RemoveTrashSnapIn()
    {
        if(snapZone.HeldItem == null)return;
        smallTrashItem = snapZone.HeldItem.GetComponent<SmallTrashItem>();
        if(smallTrashItem == null)return;
        Debug.Log("Bukan Sampah" + smallTrashItem.name);
        smallTrashItem.RemoveTrashFromData();
        
    }
}
