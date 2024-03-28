using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
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
}
