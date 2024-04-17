using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[Serializable]
public class DialogueFinishActions
{
    public DialogueListTypeParent dialogueListTypeParent;
    [Space(5)]
    public DialogueListType_Home_Intro dialogueListType_Home_Intro;
    public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
    public DialogueListType_Home_QuizExplanation dialogueListType_Home_QuizExplanation;
    public UnityEvent OnDialogueFinish;
}

// [Serializable]
// public class DialogueFinishActions_Home_Intro
// {
//     public DialogueListType_Home_Intro dialogueListType_Home_Intro;
//     public UnityEvent OnDialogueFinish;
// }
// [Serializable]
// public class DialogueFinishActions_Home_Quiz
// {
//     public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
//     public UnityEvent OnDialogueFinish;
// }
// [Serializable]
// public class DialogueFinishActions_Home_QuizExp1
// {
//     public DialogueListType_Home_QuizExp1 dialogueListType_Home_QuizExp1;
//     public UnityEvent OnDialogueFinish;
// }
// [Serializable]
// public class DialogueFinishActions_Home_QuizExp2
// {
//     public DialogueListType_Home_QuizExp2 dialogueListType_Home_QuizExp2;
//     public UnityEvent OnDialogueFinish;
// }
// [Serializable]
// public class DialogueFinishActions_Home_QuizExp3
// {
//     public DialogueListType_Home_QuizExp3 dialogueListType_Home_QuizExp3;
//     public UnityEvent OnDialogueFinish;
// }


public class DialogueManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private SODialogueList SODialogueList;
    [SerializeField]private DialogueHolder dialogueHolder; // dialogueholder nerima scene dialogue, trus berdasarkan itu dikirim textnya beserta data berdasarkan namanya ke dialogue line
    [Header("Dialogue List Type")]
    
    private DialogueListTypeParent dialogueSceneTypeNow_Parent;  
    private System.Enum dialogueSceneTypeNow; 

    public static Action DoSomethingAfterFinish, HideFinishedDialogue_AfterFinishingTask;
    // public static Action<DialogueListTypeParent, System.Enum> PlaySceneDialogueNew;
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
        // PlaySceneDialogueNew += PlayDialogueScene;
    }

    private void Start() 
    {
        PlayDialogueScene(DialogueListTypeParent.Home_Intro, DialogueListType_Home_Intro.Home_Introduction1);
    }
    private void Update() 
    {
        if(isPlayScene2)
        {
            isPlayScene2 = false;
            // PlayDialogueScene(DialogueListType.Home_Introduction2);
            PlayDialogueScene(DialogueListTypeParent.Home_Intro, DialogueListType_Home_Intro.Home_Introduction2);
        }
        if(DialogueListType_Home_Intro.Home_Introduction2.Equals(dialogueSceneTypeNow) && isFinishTaskScene2)
        {
            isFinishTaskScene2 = false;
            HideFinishedDialogueNow();
        }
        
    }
    // private void PlayDialogueScene(DialogueListType inputDialogueSceneType)
    // {
    //     dialogueSceneTypeNow = inputDialogueSceneType;
    //     SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow);

    //     // OnFinishDialogue.RemoveAllListeners();
        
    //     if(chosenDialogue)
    //     {
    //         dialogueHolder.ShowDialogue(chosenDialogue);
    //     }
    //     // if(dialogueSceneTypeNow == DialogueListType.Home_Introduction1)
    //     // {
    //     //     dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction1_dialogue);
    //     // }
    //     // else if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
    //     // {
    //     //     dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction2_dialogue);
    //     // }
    // }
    
    // public void PlayDialogueSceneOnEvent(GetDialogueListType inputDialogueSceneType)
    // {
    //     dialogueSceneTypeNow = inputDialogueSceneType.dialogueListType;
    //     SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow);

    //     // OnFinishDialogue.RemoveAllListeners();
        
    //     if(chosenDialogue)
    //     {
    //         dialogueHolder.ShowDialogue(chosenDialogue);
    //     }
    // }
    
    public void PlayDialogueScene<T>(DialogueListTypeParent dialogueListTypeParent, T enumValue) where T : struct, Enum
    {
        // dialogueSceneTypeNow = inputDialogueSceneType;
        dialogueSceneTypeNow_Parent = dialogueListTypeParent;
        dialogueSceneTypeNow = enumValue;

        SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueListTypeParent, enumValue);

        // OnFinishDialogue.RemoveAllListeners();
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
    }
    
    public void PlayDialogueSceneOnEvent(GetDialogueListType inputDialogueSceneType)
    {
        dialogueSceneTypeNow_Parent = inputDialogueSceneType.dialogueListTypeParent;
        dialogueSceneTypeNow = inputDialogueSceneType.dialogueSceneTypeNow;
        SODialogue chosenDialogue = null;
        if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro)
        {
            DialogueListType_Home_Intro dialogueSceneType = (DialogueListType_Home_Intro)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Quiz)
        {
            DialogueListType_Home_Quiz dialogueSceneType = (DialogueListType_Home_Quiz)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation)
        {
            DialogueListType_Home_QuizExplanation dialogueSceneType = (DialogueListType_Home_QuizExplanation)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        

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
            if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro && action.dialogueListType_Home_Intro.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
                return;
            }
            if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Quiz && action.dialogueListType_Home_Quiz.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
                return;
            }
            if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation && action.dialogueListType_Home_QuizExplanation.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
                return;
            }
            
        }

        // if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
        // {
        //     Debug.Log("Click That Bool Yo to HideTheDialogue");
        // }
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
        // PlaySceneDialogue -= PlayDialogueScene;
    }
    
}
