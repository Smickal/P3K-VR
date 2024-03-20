using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public enum LevelP3KType{
    None, Choking, Bleeding
}
[Serializable]
public class LevelData
{
    public LevelP3KType levelType;
    [Tooltip("0-1-2-3")]
    public int totalScore;
    public bool unlocked;
    public bool hasFinishIntro;
    [Tooltip("Kalo ud pernah slsain, pas deketin quest br muncul ui do you want to do it again")]
    public bool hasBeatenLevelOnce;
    public Sprite levelSprite;

}
public class SOPlayerFile : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/PlayerFile")]
    public static void QuickCreate()
    {
        SOPlayerFile asset = CreateInstance<SOPlayerFile>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//PlayerFile.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    [Tooltip("utk cek player pernah slsain tutorial ga")]
    public bool isTutorialFinish;
    public List<LevelData> levelDataList;


}
