using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOFirstAid : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/FirstAidData")]
    public static void QuickCreate()
    {
        SOFirstAid asset = CreateInstance<SOFirstAid>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//FirstAidData.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    [Header("Data Contents")]
    public string FirstKitName;
    [TextArea(5, 7)] public string KitDescription;


}
