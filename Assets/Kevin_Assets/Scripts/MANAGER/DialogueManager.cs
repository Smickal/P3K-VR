using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueFinishActions
{
    public DialogueListType dialogueListType;
    public UnityEvent OnDialogueFinish;
}
public class DialogueManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private SODialogueList SODialogueList;
    [SerializeField]private DialogueHolder dialogueHolder; // dialogueholder nerima scene dialogue, trus berdasarkan itu dikirim textnya beserta data berdasarkan namanya ke dialogue line
    private DialogueListType dialogueSceneTypeNow;
    public static Action DoSomethingAfterFinish, HideFinishedDialogue_AfterFinishingTask;
    public static Action<DialogueListType> PlaySceneDialogue;
    
    [Header("Action To Do When DialogueFinish")]
    [SerializeField]private DialogueFinishActions[] dialogueFinishActions;

    [Header("DEBUG ONLY")]
    // public bool isPlayScene1;
    public bool isPlayScene2;
    public bool isFinishTaskScene2;

    private void Awake() 
    {
        DoSomethingAfterFinish += DoSomethingAfterDialogueFinish;
        HideFinishedDialogue_AfterFinishingTask += HideFinishedDialogueNow;
        PlaySceneDialogue += PlayDialogueScene;
    }

    private void Start() 
    {
        // PlayDialogueScene(DialogueListType.Home_Introduction1);
    }
    private void Update() 
    {
        if(isPlayScene2)
        {
            isPlayScene2 = false;
            PlayDialogueScene(DialogueListType.Home_Introduction2);
        }
        if(dialogueSceneTypeNow == DialogueListType.Home_Introduction2 && isFinishTaskScene2)
        {
            isFinishTaskScene2 = false;
            HideFinishedDialogueNow();
        }
    }
    private void PlayDialogueScene(DialogueListType inputDialogueSceneType)
    {
        dialogueSceneTypeNow = inputDialogueSceneType;
        SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow);

        // OnFinishDialogue.RemoveAllListeners();
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
        // if(dialogueSceneTypeNow == DialogueListType.Home_Introduction1)
        // {
        //     dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction1_dialogue);
        // }
        // else if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
        // {
        //     dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction2_dialogue);
        // }
    }
    
    public void PlayDialogueSceneOnEvent(GetDialogueListType inputDialogueSceneType)
    {
        dialogueSceneTypeNow = inputDialogueSceneType.dialogueListType;
        SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow);

        // OnFinishDialogue.RemoveAllListeners();
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
    }

    private void DoSomethingAfterDialogueFinish()
    {
        //kalo ga mo lwt code bs lewat inspector, kalo mo lwt code gausa tambahin di inspector, well sbnrnya tinggal ilangin return kalo mo 2-2nya
        foreach(DialogueFinishActions action in dialogueFinishActions)
        {
            if(action.dialogueListType == dialogueSceneTypeNow)
            {
                action.OnDialogueFinish.Invoke();
                return;
            }
        }

        if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
        {
            Debug.Log("Click That Bool Yo to HideTheDialogue");
        }
    }

    private void HideFinishedDialogueNow()
    {
        dialogueHolder.StopCoroutineAbruptly();
        dialogueHolder.HideDialogue();
    }
    
    public void TurnOffStatic()
    {
        DoSomethingAfterFinish -= DoSomethingAfterDialogueFinish;
        HideFinishedDialogue_AfterFinishingTask -= HideFinishedDialogueNow;
        PlaySceneDialogue -= PlayDialogueScene;
    }
    
}
