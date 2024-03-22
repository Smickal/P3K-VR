using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class SOTimelineList : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/SOTimelineList")]
    public static void QuickCreate()
    {
        SOTimelineList asset = CreateInstance<SOTimelineList>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Cutscene//SOTImelineList.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public TimelineAsset Home_Cutscene1;
}
