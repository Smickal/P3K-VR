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

    [SerializeField]
    
    public List<SODialogueListContainer> dialogueListContainers;

    public SODialogue SearchDialogue<T>(DialogueListTypeParent dialogueListTypeParent, T enumValue) where T : struct, Enum
    {
        if(dialogueListTypeParent == DialogueListTypeParent.None)return null;
        DialogueListContainers[] chosenDialogue = null;
        foreach(SODialogueListContainer dialogueListContainers in dialogueListContainers)
        {
            // Debug.Log(dialogueListContainers.dialogueListTypeParent + " ada isi ga " + dialogueListContainers.dialogueListContainers.Length + " ya");
            if(dialogueListContainers.dialogueListTypeParent == dialogueListTypeParent)
            {
                chosenDialogue = dialogueListContainers.dialogueListContainers;
                break;
            }
        }
        // Debug.Log(chosenDialogue.Length + " Tes" + enumValue);
        if(chosenDialogue == null)return null;
        foreach(DialogueListContainers dialogueContainer in chosenDialogue)
        {
            
            //Home
            if(dialogueListTypeParent == DialogueListTypeParent.Home_Intro && dialogueContainer.dialogueListType_Home_Intro.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Home_Intro == DialogueListType_Home_Intro.None) return null;
                return dialogueContainer.dialogue;
            }
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
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Intro && dialogueContainer.dialogueListType_Choking_Intro.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Choking_Intro == DialogueListType_Choking_Intro.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Intro && dialogueContainer.dialogueListType_Bleeding_Intro.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Bleeding_Intro == DialogueListType_Bleeding_Intro.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Explanation && dialogueContainer.dialogueListType_Choking_Explanation.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Choking_Explanation == DialogueListType_Choking_Explanation.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Explanation && dialogueContainer.dialogueListType_Bleeding_Explanation.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Bleeding_Explanation == DialogueListType_Bleeding_Explanation.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Ending && dialogueContainer.dialogueListType_Bleeding_Explanation.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Bleeding_Explanation == DialogueListType_Bleeding_Explanation.None) return null;
                return dialogueContainer.dialogue;
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Ending && dialogueContainer.dialogueListType_Bleeding_Ending.Equals(enumValue))
            {
                if(dialogueContainer.dialogueListType_Bleeding_Ending == DialogueListType_Bleeding_Ending.None) return null;
                return dialogueContainer.dialogue;
            }
        }
        
        
        return null;
    }

}
