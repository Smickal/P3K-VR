using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingTakeCorrectItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]DialogueManager dialogueManager;
    [SerializeField]private Patient_Bleeding patient_Bleeding;
    [SerializeField]private BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;
    [Header("Correct State For Item - item ini buat pas kapan; Kalo ga ada yg kedua jdiin none")]
    [SerializeField]private List<BleedingWithoutEmbeddedItem_State> _correctbleedingWithoutEmbeddedItem_State_List;

    private void Awake() 
    {
        if(dialogueManager == null)dialogueManager = FindAnyObjectByType<DialogueManager>();
        if(patient_Bleeding == null)patient_Bleeding = FindAnyObjectByType<Patient_Bleeding>();
        if(bleedingWithoutEmbeddedItem == null)bleedingWithoutEmbeddedItem = FindAnyObjectByType<BleedingWithoutEmbeddedItem>();
    }
    public void CheckingState()
    {
        // Debug.Log("Lewat sini ga checking");
        if(GameManager.CheckGameStateNow() != GameState.InGame || GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid)return;
        
        if(patient_Bleeding.BleedingQuest_State == BleedingQuest_State.WithItem)
        {
            if(!CorrectState(BleedingWithoutEmbeddedItem_State.BandageTime))
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithItem_Bandage);
                return;
            }
            else
            {
                DialogueManager.HideFinishedDialogue_AfterFinishingTask();
                return;
            }
            
        }
        else if(patient_Bleeding.BleedingQuest_State == BleedingQuest_State.WithoutItem)
        {
            // CleanHands, WearGloves, StopBleed, CleanBlood, DryWater, BandageTime, PuttingLegOnTopSomethingTime
            if(CorrectState(BleedingWithoutEmbeddedItem_State.None))
            {
                DialogueManager.HideFinishedDialogue_AfterFinishingTask();
                return;
            }
            
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.CleanHands)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_CleanHands);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.WearGloves)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_WearGloves);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.StopBleed)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_StopBleed);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.CleanBlood)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_CleanBlood);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.DryWater)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_DryWater);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.BandageTime)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_BandageTime);
                return;
            }
            if(bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State == BleedingWithoutEmbeddedItem_State.PuttingLegOnTopSomethingTime)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_WrongItem, DialogueListType_Bleeding_WrongItem.Bleeding_WrongItem_WithoutItem_PuttingLegOnTopSomethingTime);
                return;
            }
        }
    }
    public bool CorrectState(BleedingWithoutEmbeddedItem_State ReqState)
    {
        
        foreach(BleedingWithoutEmbeddedItem_State _correctbleedingWithoutEmbeddedItem_State in _correctbleedingWithoutEmbeddedItem_State_List)
        {
            if(ReqState == BleedingWithoutEmbeddedItem_State.None)
            {
                if(_correctbleedingWithoutEmbeddedItem_State == bleedingWithoutEmbeddedItem.BleedingWithoutEmbeddedItem_State)return true;
            }
            else
            {
                if(_correctbleedingWithoutEmbeddedItem_State == ReqState)return true;
            }
            
        }
        return false;
    }
}
