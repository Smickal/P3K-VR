using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public enum BleedingQuest_State
{
    None, WithItem, WithoutItem
}
public class Patient_Bleeding : MonoBehaviour
{
    bool isDoneFirstAid;
    public bool IsDoneFirstAid{get{return isDoneFirstAid;}}
    
    [Header("Reference")]
    [SerializeField]private Bleeding_QuestManager bleeding_QuestManager;
    [SerializeField]private QuestManagerUI questManagerUI;
    [SerializeField]BandageWithItemManager BleedWithItemMgr;
    [SerializeField]BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;
    [SerializeField]BleedingQuest_State bleedingQuest_State;
    [SerializeField]PatientBleedingQuestUI patientBleedingQuestUI;
    [SerializeField]DialogueManager dialogueManager;
    public BleedingQuest_State BleedingQuest_State{get{return bleedingQuest_State;}}
    public AudioClip SoundOnFinish;
    [Header("TambahanUntuk Bleed With Item")]
    [Header("CleanHands")]
    [SerializeField]AlcoholCleanManager alcoholCleanManager;
    public bool isCleanHandsDone_WithItem = false;
    IEnumerator FirstAidCoroutine_WithItem;
    

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
        alcoholCleanManager.Reset();

        patientBleedingQuestUI.ActivateToolTipWithItem(0);
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Explanation, DialogueListType_Bleeding_Explanation.Bleeding_WithItem_Start);
        FirstAidCoroutine_WithItem = FirstAid_CleanHands_WithItem();

        StartCoroutine(FirstAidCoroutine_WithItem);
    }

    public void FirstAid_BleedingWithItemDone()
    {
        BleedWithItemMgr.DeactivateBandageWithItem_WithoutSnapZone();
        bleedingQuest_State = BleedingQuest_State.None;
        patientBleedingQuestUI.ActivateToolTipWithItem(3);
        patientBleedingQuestUI.ActivateTrashCan();
        
        bleeding_QuestManager.SaveFinish_WithItem();
        PlaySoundFinish();
        isDoneFirstAid = true;
    }
    public void FirstAid_BleedingWithoutItemDone()
    {
        questManagerUI.CloseHelper_Bleeding_WithoutItem();
        questManagerUI.OpenHelper_Bleeding_WithItem();
        bleeding_QuestManager.SaveFinish_WithoutItem();
        ActivateGlassShard();
    }
    
    public void StopCoroutines()
    {
        bleedingWithoutEmbeddedItem.DeactivateFirstAid();
        if(FirstAidCoroutine_WithItem != null)StopCoroutine(FirstAidCoroutine_WithItem);
        BleedWithItemMgr.DeactivateBandageWithItem_WithoutSnapZone();
        patientBleedingQuestUI.CloseALL();
    }
    private IEnumerator FirstAid_CleanHands_WithItem()
    {
        yield return new WaitUntil(()=> alcoholCleanManager.IsDoneCleaning);
        PlaySoundFinish();
        isCleanHandsDone_WithItem = true;
        patientBleedingQuestUI.ActivateToolTipWithItem(1);
        BleedWithItemMgr.ActivateBandageWithItem();
        FirstAidCoroutine_WithItem = null;
    }

    public void PlaySoundFinish()
    {
        if (SoundOnFinish) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnFinish, transform.position, 0.75f);
            }
        }
    }
}
