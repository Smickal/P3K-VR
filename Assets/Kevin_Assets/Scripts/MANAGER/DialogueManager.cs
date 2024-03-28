using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private SODialogueList SODialogueList;
    [SerializeField]private DialogueHolder dialogueHolder; // dialogueholder nerima scene dialogue, trus berdasarkan itu dikirim textnya beserta data berdasarkan namanya ke dialogue line
    private DialogueListType dialogueSceneTypeNow;
    public static Action DoSomethingAfterFinish, HideFinishedDialogue_AfterFinishingTask;
    public static Action<DialogueListType> PlaySceneDialogue;
    
    //di sini main berdasarkan yg diminta org sesuai dgn type listnya dan yg nyambungin skrg yg lg nyala dialogue yg mana, dn kalo dialogue itu perlu yg dimatiin lsg ga ato prlu apa dl br dimatiin dialoguenya, dn dia nyambungin buat matiinnya lwt sini

    // kayaknya utk skrg main amannya player gabisa interact apa apa dulu kalo dialog main ??

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
        if(dialogueSceneTypeNow == DialogueListType.Home_Introduction1)
        {
            dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction1_dialogue);
        }
        else if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
        {
            dialogueHolder.ShowDialogue(SODialogueList.Home_Introduction2_dialogue);
        }
    }

    private void DoSomethingAfterDialogueFinish()
    {
        if(dialogueSceneTypeNow == DialogueListType.Home_Introduction1)
        {
            TimelineManager.StartTimeline(TimelineType.Home_Cutscene1);
        }
        else if (dialogueSceneTypeNow == DialogueListType.Home_Introduction2)
        {
            Debug.Log("Click That Bool Yo to HideTheDialogue");
        }
    }

    private void HideFinishedDialogueNow()
    {
        dialogueHolder.StopCoroutineAbruptly();
        dialogueHolder.HideDialogue();
    }
    
}
