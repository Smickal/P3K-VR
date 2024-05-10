using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogueTalker
{
    public DialogueTalkerType dialogueTalkerType;
    public CharaName charaName;
    public string name;
    public Color _textColor;

}

public class SODialogueTalker : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/DialogueTalker")]
    public static void QuickCreate()
    {
        SODialogueTalker asset = CreateInstance<SODialogueTalker>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Cutscene//DialogueTalker.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
    
    [Header("Samakan Urutannya dengan enumCharaname")]
    public List<DialogueTalker> dialogueTalkers;

}
