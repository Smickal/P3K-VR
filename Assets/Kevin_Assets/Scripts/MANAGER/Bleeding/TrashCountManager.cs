using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using BNG;
using UnityEngine;

public class TrashCountManager : MonoBehaviour
{
    [SerializeField]private PlayerRestriction playerRestriction;
    [SerializeField]private List<SmallTrashItem> SmallTrashItems;
    public static TrashCountManager Instance { get;private set;}
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        SmallTrashItem smallTrash = other.GetComponent<SmallTrashItem>();
        if(smallTrash != null)
        {
            if(SmallTrashItems.Contains(smallTrash))
            {
                playerRestriction.DeleteFromExistingData(smallTrash.gameObject);
                SmallTrashItems.Remove(smallTrash);
                smallTrash.DestroyTrash();
            }
            else
            {
                ReturnToSnapZone returnToSnapZone = other.GetComponent<ReturnToSnapZone>();
                if(returnToSnapZone != null)
                {
                    if(returnToSnapZone.enabled == false)
                    {
                        returnToSnapZone.OnlyReturnOnce = true;
                        returnToSnapZone.enabled = true;
                    }
                    
                }
                // else
                // {
                //     Debug.Log("harusnya ga trjadi");
                // }
            }
        }
    }
    public bool IsThereAnySmallTrash()
    {
        return SmallTrashItems.Count != 0;
    }
    public int TotalSmallTrash()
    {
        return SmallTrashItems.Count;
    }
    public void AddTrash(SmallTrashItem item)
    {
        if(!SmallTrashItems.Contains(item))
        {
            SmallTrashItems.Add(item);
        }
    }
    public void RemoveTrashFromData(SmallTrashItem item)
    {
        if(SmallTrashItems.Contains(item))
        {
            SmallTrashItems.Remove(item);
        }
    }
}
