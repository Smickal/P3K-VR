using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOKotakP3K : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/KitP3K")]
    public static void QuickCreate()
    {
        SOLangkahP3K asset = CreateInstance<SOLangkahP3K>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//KitP3K.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public string kitName;
    public Sprite kitIMG;

    [TextArea(7, 7)] public string kitDescText;

}
