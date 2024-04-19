using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



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
    [SerializeField]
    
    public List<SODialogueListContainer> dialogueListContainers;

    // [Header("Home")]
    // public DialogueListContainers[] dialogueListContainers_Home_Intro;
    // public DialogueListContainers[] dialogueListContainers_Home_Quiz;
    // public DialogueListContainers[] dialogueListContainers_Home_QuizExp_1;
    // public DialogueListContainers[] dialogueListContainers_Home_QuizExp_2;
    // public DialogueListContainers[] dialogueListContainers_Home_QuizExp_3;

    // [Header("Choking")]
    // public DialogueListContainer[] dialogueList_Choke_Intro;

    public SODialogue SearchDialogue<T>(DialogueListTypeParent dialogueListTypeParent, T enumValue) where T : struct, Enum
    {
        if(dialogueListTypeParent == DialogueListTypeParent.None)return null;
        DialogueListContainers[] chosenDialogue = null;
        foreach(SODialogueListContainer dialogueListContainers in dialogueListContainers)
        {
            Debug.Log(dialogueListContainers.dialogueListTypeParent + " ada isi ga " + dialogueListContainers.dialogueListContainers.Length + " ya");
            if(dialogueListContainers.dialogueListTypeParent == dialogueListTypeParent)
            {
                chosenDialogue = dialogueListContainers.dialogueListContainers;
                break;
            }
        }
        Debug.Log(chosenDialogue.Length + " Tes" + enumValue);
        if(chosenDialogue == null)return null;
        foreach(DialogueListContainers dialogueContainer in chosenDialogue)
        {
            
            //Home
            if(dialogueListTypeParent == DialogueListTypeParent.Home_Intro && dialogueContainer.dialogueListType_Home_Intro.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Home_Intro == DialogueListType_Home_Intro.None) return null;
                return dialogueContainer.dialogue;
            }
            // Debug.Log(enumValue + " Kok Bisa tidak masukkkk ???" + dialogueContainer.dialogueListType_Home_Quiz.Equals(enumValue));
            // Debug.Log(dialogueContainer.dialogueListType_Home_Quiz + " dan " + enumValue);
            if(dialogueListTypeParent == DialogueListTypeParent.Home_Quiz && dialogueContainer.dialogueListType_Home_Quiz.Equals(enumValue))
            {
                // Debug.Log("Emang ga masuk sini kah ?" + enumValue);
                if(dialogueContainer.dialogueListType_Home_Quiz == DialogueListType_Home_Quiz.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Home_QuizExplanation && dialogueContainer.dialogueListType_Home_QuizExplanation.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Home_QuizExplanation == DialogueListType_Home_QuizExplanation.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_WrongItem && dialogueContainer.dialogueListType_Bleeding_WrongItem.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Bleeding_WrongItem == DialogueListType_Bleeding_WrongItem.None) return null;
                return dialogueContainer.dialogue;
            }
        }
        
        
        return null;
    }
    // public SODialogue SearchDialogue(DialogueListType dialogueListTypeNow)
    // {
    //     if(dialogueListTypeNow == DialogueListType.None)return null;
    //     foreach(DialogueListContainer dialogueContainer in dialogueListContainers)
    //     {
    //         if(dialogueContainer.dialogueListType == dialogueListTypeNow)
    //         {
    //             return dialogueContainer.dialogue;
    //         }
    //     }
    //     return null;
    // }

}
