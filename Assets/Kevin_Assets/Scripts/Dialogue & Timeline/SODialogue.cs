using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialoguePerLine
{
    public DialogueTalkerType dialogueTalkerType;
    public CharaName charaName;
    [TextArea(3, 7)]public string dialogueText;
}
public class SODialogue : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/Dialogue")]
    public static void QuickCreate()
    {
        SODialogue asset = CreateInstance<SODialogue>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Cutscene//Dialogue.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public bool isCloseAfterFinished;
    public List<DialoguePerLine> Scene_Dialogues;
}
