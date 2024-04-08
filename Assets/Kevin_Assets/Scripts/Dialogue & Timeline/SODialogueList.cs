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
}
