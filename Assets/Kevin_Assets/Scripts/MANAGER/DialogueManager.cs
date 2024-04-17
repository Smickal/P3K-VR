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

[Serializable]
public class DialogueFinishActions_Home_Intro
{
    public DialogueListType_Home_Intro dialogueListType_Home_Intro;
    public UnityEvent OnDialogueFinish;
}
[Serializable]
public class DialogueFinishActions_Home_Quiz
{
    public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
    public UnityEvent OnDialogueFinish;
}
[Serializable]
public class DialogueFinishActions_Home_QuizExp1
{
    public DialogueListType_Home_QuizExp1 dialogueListType_Home_QuizExp1;
    public UnityEvent OnDialogueFinish;
}
[Serializable]
public class DialogueFinishActions_Home_QuizExp2
{
    public DialogueListType_Home_QuizExp2 dialogueListType_Home_QuizExp2;
    public UnityEvent OnDialogueFinish;
}
[Serializable]
public class DialogueFinishActions_Home_QuizExp3
{
    public DialogueListType_Home_QuizExp3 dialogueListType_Home_QuizExp3;
    public UnityEvent OnDialogueFinish;
}


public class DialogueManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private SODialogueList SODialogueList;
    [SerializeField]private DialogueHolder dialogueHolder; // dialogueholder nerima scene dialogue, trus berdasarkan itu dikirim textnya beserta data berdasarkan namanya ke dialogue line
    private DialogueListType dialogueSceneTypeNow;
    [Header("Dialogue List Type")]
    private DialogueListTypeParent dialogueSceneTypeNow_Parent;    
    private DialogueListType_Home_Intro dialogueSceneTypeNow_Home_Intro;
    private DialogueListType_Home_Quiz dialogueSceneTypeNow_Home_Quiz;
    private DialogueListType_Home_QuizExp1 dialogueSceneTypeNow_Home_QuizExp1;
    private DialogueListType_Home_QuizExp2 dialogueSceneTypeNow_Home_QuizExp2;
    private DialogueListType_Home_QuizExp3 dialogueSceneTypeNow_Home_QuizExp3;

    public static Action DoSomethingAfterFinish, HideFinishedDialogue_AfterFinishingTask;
    public static Action<DialogueListType> PlaySceneDialogue;
    
    [Header("Action To Do When DialogueFinish")]
    [SerializeField]private DialogueFinishActions[] dialogueFinishActions;
    //HOME
    [SerializeField]private DialogueFinishActions_Home_Intro[] dialogueFinishActions_Home_Intro;
    [SerializeField]private DialogueFinishActions_Home_Quiz[] dialogueFinishActions_Home_Quiz;
    [SerializeField]private DialogueFinishActions_Home_QuizExp1[] dialogueFinishActions_Home_QuizExp1;
    [SerializeField]private DialogueFinishActions_Home_QuizExp2[] dialogueFinishActions_Home_QuizExp2;
    [SerializeField]private DialogueFinishActions_Home_QuizExp3[] dialogueFinishActions_Home_QuizExp3;

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

    //PLAY DIALOGUE
    private void PlayDialogueScene_Home_Intro(DialogueListType_Home_Intro inputDialogueSceneType)
    {
        dialogueSceneTypeNow_Parent = DialogueListTypeParent.Home_Intro;
        dialogueSceneTypeNow_Home_Intro = inputDialogueSceneType;

        SODialogue chosenDialogue = SODialogueList.SearchDialogue_Home_Intro(dialogueSceneTypeNow_Home_Intro);

        // OnFinishDialogue.RemoveAllListeners();
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
    }
    // private void PlayDialogueSceneOnEvent_Home_Intro(GetDialogueListType inputDialogueSceneType)
    // {
    //     dialogueSceneTypeNow_Parent = DialogueListTypeParent.Home_Intro;
    //     dialogueSceneTypeNow_Home_Intro = inputDialogueSceneType;

    //     SODialogue chosenDialogue = SODialogueList.SearchDialogue_Home_Intro(dialogueSceneTypeNow_Home_Intro);

    //     // OnFinishDialogue.RemoveAllListeners();
        
    //     if(chosenDialogue)
    //     {
    //         dialogueHolder.ShowDialogue(chosenDialogue);
    //     }
    // }
    private void DoSomethingAfterDialogueFinish()
    {
        //kalo ga mo lwt code bs lewat inspector, kalo mo lwt code gausa tambahin di inspector, well sbnrnya tinggal ilangin return kalo mo 2-2nya
        if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro)
        {
            foreach(DialogueFinishActions_Home_Intro action in dialogueFinishActions_Home_Intro)
            {
                if(action.dialogueListType_Home_Intro == dialogueSceneTypeNow_Home_Intro)
                {
                    action.OnDialogueFinish.Invoke();
                    return;
                }
                
            }
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Quiz)
        {
            foreach(DialogueFinishActions_Home_Quiz action in dialogueFinishActions_Home_Quiz)
            {
                if(action.dialogueListType_Home_Quiz == dialogueSceneTypeNow_Home_Quiz)
                {
                    action.OnDialogueFinish.Invoke();
                    return;
                }
                
            }
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation_1)
        {
            foreach(DialogueFinishActions_Home_QuizExp1 action in dialogueFinishActions_Home_QuizExp1)
            {
                if(action.dialogueListType_Home_QuizExp1 == dialogueSceneTypeNow_Home_QuizExp1)
                {
                    action.OnDialogueFinish.Invoke();
                    return;
                }
                
            }
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation_2)
        {
            foreach(DialogueFinishActions_Home_QuizExp2 action in dialogueFinishActions_Home_QuizExp2)
            {
                if(action.dialogueListType_Home_QuizExp2 == dialogueSceneTypeNow_Home_QuizExp2)
                {
                    action.OnDialogueFinish.Invoke();
                    return;
                }
                
            }
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation_3)
        {
            foreach(DialogueFinishActions_Home_QuizExp3 action in dialogueFinishActions_Home_QuizExp3)
            {
                if(action.dialogueListType_Home_QuizExp3 == dialogueSceneTypeNow_Home_QuizExp3)
                {
                    action.OnDialogueFinish.Invoke();
                    return;
                }
                
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
        PlaySceneDialogue -= PlayDialogueScene;
    }
    
}
