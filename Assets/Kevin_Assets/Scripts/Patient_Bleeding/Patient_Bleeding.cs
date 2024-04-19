using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BleedingQuest_State
{
    None, WithItem, WithoutItem
}
public class Patient_Bleeding : MonoBehaviour
{
    bool isDoneFirstAid;
    public bool IsDoneFirstAid{get{return isDoneFirstAid;}}
    
    [Header("Reference")]
    [SerializeField]private QuestManagerUI questManagerUI;
    [SerializeField]BandageWithItemManager BleedWithItemMgr;
    [SerializeField]BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;
    [SerializeField]BleedingQuest_State bleedingQuest_State;
    public BleedingQuest_State BleedingQuest_State{get{return bleedingQuest_State;}}

    private void Start() 
    {
        ActivateBleedWithoutItem();
    }

    public void ActivateBleedWithoutItem()
    {
        bleedingQuest_State = BleedingQuest_State.WithoutItem;
        bleedingWithoutEmbeddedItem.ActivateFirstAid();
        BleedWithItemMgr.DeactivateBandageWithItem();
    }
    public void ActivateGlassShard()
    {
        bleedingQuest_State = BleedingQuest_State.WithItem;
        BleedWithItemMgr.ActivateBandageWithItem();
    }

    public void FirstAid_BleedingWithItemDone()
    {
        BleedWithItemMgr.DeactivateBandageWithItem();
        bleedingQuest_State = BleedingQuest_State.None;
        isDoneFirstAid = true;
    }
    public void FirstAid_BleedingWithoutItemDone()
    {
        questManagerUI.CloseHelper_Bleeding_WithoutItem();
        questManagerUI.OpenHelper_Bleeding_WithItem();
        
        ActivateGlassShard();
    }
    
    public void StopCoroutines()
    {
        bleedingWithoutEmbeddedItem.DeactivateFirstAid();
        BleedWithItemMgr.DeactivateBandageWithItem();
    }

}
