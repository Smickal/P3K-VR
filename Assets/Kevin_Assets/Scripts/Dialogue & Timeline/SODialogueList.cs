using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogueListContainer
{
    public DialogueListType dialogueListType;
    public SODialogue dialogue;
}
[Serializable]
public class DialogueListContainer_Home
{
    public DialogueListType_Home_Intro dialogueListType_Home_Intro;
    public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
    public DialogueListType_Home_QuizExp1 dialogueListType_Home_QuizExp1;
    public DialogueListType_Home_QuizExp2 dialogueListType_Home_QuizExp2;
    public DialogueListType_Home_QuizExp3 dialogueListType_Home_QuizExp3;
    public SODialogue dialogue;
}
public class SODialogueList : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/DialogueList")]
    public static void QuickCreate()
    {
        SODialogueList asset = CreateInstance<SODialogueList>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Cutscene//DialogueList.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public SODialogue Home_Introduction1_dialogue;

    public SODialogue Home_Introduction2_dialogue;
    public DialogueListContainer[] dialogueListContainers;

    [Header("Home")]
    public DialogueListContainer_Home[] dialogueList_Home_Intro;
    public DialogueListContainer_Home[] dialogueList_Home_Quiz;
    public DialogueListContainer_Home[] dialogueList_Home_QuizExp_1;
    public DialogueListContainer_Home[] dialogueList_Home_QuizExp_2;
    public DialogueListContainer_Home[] dialogueList_Home_QuizExp_3;
    [Header("Choking")]
    public DialogueListContainer[] dialogueList_Choke_Intro;

    public SODialogue SearchDialogue(DialogueListType dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType.None)return null;
        foreach(DialogueListContainer dialogueContainer in dialogueListContainers)
        {
            if(dialogueContainer.dialogueListType == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
    //HOME
    public SODialogue SearchDialogue_Home_Intro(DialogueListType_Home_Intro dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType_Home_Intro.None)return null;
        foreach(DialogueListContainer_Home dialogueContainer in dialogueList_Home_Intro)
        {
            if(dialogueContainer.dialogueListType_Home_Intro == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
    public SODialogue SearchDialogue_Home_Quiz(DialogueListType_Home_Quiz dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType_Home_Quiz.None)return null;
        foreach(DialogueListContainer_Home dialogueContainer in dialogueList_Home_Quiz)
        {
            if(dialogueContainer.dialogueListType_Home_Quiz == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
    public SODialogue SearchDialogue_Home_QuizExp1(DialogueListType_Home_QuizExp1 dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType_Home_QuizExp1.None)return null;
        foreach(DialogueListContainer_Home dialogueContainer in dialogueList_Home_QuizExp_1)
        {
            if(dialogueContainer.dialogueListType_Home_QuizExp1 == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
    public SODialogue SearchDialogue_Home_QuizExp2(DialogueListType_Home_QuizExp2 dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType_Home_QuizExp2.None)return null;
        foreach(DialogueListContainer_Home dialogueContainer in dialogueList_Home_QuizExp_2)
        {
            if(dialogueContainer.dialogueListType_Home_QuizExp2 == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
    public SODialogue SearchDialogue_Home_QuizExp3(DialogueListType_Home_QuizExp3 dialogueListTypeNow)
    {
        if(dialogueListTypeNow == DialogueListType_Home_QuizExp3.None)return null;
        foreach(DialogueListContainer_Home dialogueContainer in dialogueList_Home_QuizExp_3)
        {
            if(dialogueContainer.dialogueListType_Home_QuizExp3 == dialogueListTypeNow)
            {
                return dialogueContainer.dialogue;
            }
        }
        return null;
    }
}
