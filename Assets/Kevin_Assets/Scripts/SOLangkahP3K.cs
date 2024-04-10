using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOLangkahP3K : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/LangkahLangkahP3K")]
    public static void QuickCreate()
    {
        SOLangkahP3K asset = CreateInstance<SOLangkahP3K>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//LangkahP3K.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    [Header("Langkah-Langkah Option")]
    public string ProcedureName;
    public string ProcedureShortName;
    public Sprite ProcedureIcon;
    public Sprite ProcedureIMG;
    [TextArea(3, 7)] public string ProcedureDescription;

    [Space(5)]
    [Header("StepsOfProcedure")]
    public Transform _prefabProcedure;
    public List<Procedure> Procedures = new List<Procedure>();
    [Tooltip("0-ga ada, 1-3 ada dr sad ke big happy")]
    [TextArea(3, 7)] public string[] _scoreNoteDesc; 
}

[System.Serializable]
public class Procedure
{
    [TextArea(2, 7)] public string StepsDescription;
    [SerializeField] public Sprite StepsSprite;
}