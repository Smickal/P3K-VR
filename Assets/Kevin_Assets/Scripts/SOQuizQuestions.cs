using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SOQuizQuestions : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/QuizQuestions")]
    public static void QuickCreate()
    {
        SOQuizQuestions asset = CreateInstance<SOQuizQuestions>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//QuizQtn.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    [TextArea(2, 7)] public string Question;

    [Header("A")]
    [TextArea(2, 7)] public string AnswerA;

    [Header("B")]
    [TextArea(2, 7)] public string AnswerB;

    [Header("C")]
    [TextArea(2, 7)] public string AnswerC;

    [Header("D")]
    [TextArea(2, 7)] public string AnswerD;


    [Header("Answer")]
    public EAnswerType AnswerType = EAnswerType.A;

    [Header("Explanation")]
    public Sprite[] ExplanationSprites;
    [Header("Dialogue")]
    public DialogueListType dialogueListTypeQuestionStart;
    public DialogueListType[] dialogueListTypeExplanations;

    public string GetAnswer()
    {
        if (AnswerType == EAnswerType.A) return AnswerA;
        else if(AnswerType == EAnswerType.B) return AnswerB;
        else if(AnswerType == EAnswerType.C)   return AnswerC;
        else if(AnswerType ==EAnswerType.D) return AnswerD;

        return null;
    }

}
