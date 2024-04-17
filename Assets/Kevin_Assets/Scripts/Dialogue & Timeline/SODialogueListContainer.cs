using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DialogueListContainers))]
public class DialogueListContainersPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        label.tooltip = " Element ";


        Rect parentPos = new Rect(position.x, EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        SerializedProperty parent = property.FindPropertyRelative("dialogueListTypeParent");
        EditorGUI.PropertyField(parentPos, parent);

        DialogueListTypeParent dialogueListTypeParent = (DialogueListTypeParent)parent.enumValueIndex;

        
        

        if(dialogueListTypeParent == DialogueListTypeParent.Home_Intro)
        {
            Rect childPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
            SerializedProperty child = property.FindPropertyRelative("dialogueListType_Home_Intro");
            EditorGUI.PropertyField(childPos, child);

            DialogueListType_Home_Intro dialogueFinishActions_Home_Intro = (DialogueListType_Home_Intro)child.enumValueIndex;
            
        }
        else if(dialogueListTypeParent == DialogueListTypeParent.Home_Quiz)
        {
            Rect childPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
            SerializedProperty child = property.FindPropertyRelative("dialogueListType_Home_Quiz");
            EditorGUI.PropertyField(childPos, child);

            DialogueListType_Home_Quiz dialogueFinishActions_Home_Quiz = (DialogueListType_Home_Quiz)child.enumValueIndex;
        }
        else if(dialogueListTypeParent == DialogueListTypeParent.Home_QuizExplanation)
        {
            Rect childPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
            SerializedProperty child = property.FindPropertyRelative("dialogueListType_Home_QuizExplanation");
            EditorGUI.PropertyField(childPos, child);

            DialogueListType_Home_QuizExplanation dialogueFinishActions_Home_QuizExp1 = (DialogueListType_Home_QuizExplanation)child.enumValueIndex;
        }
        else if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_WrongItem)
        {
            Rect childPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
            SerializedProperty child = property.FindPropertyRelative("dialogueListType_Bleeding_WrongItem");
            EditorGUI.PropertyField(childPos, child);

            DialogueListType_Bleeding_WrongItem dialogueFinishActions_Home_QuizExp1 = (DialogueListType_Bleeding_WrongItem)child.enumValueIndex;
        }
        

        Rect dialoguePos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight);
        SerializedProperty dialogue = property.FindPropertyRelative("dialogue");
        EditorGUI.PropertyField(dialoguePos, dialogue);

        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 4;
    }
}
#endif
[Serializable]
public class DialogueListContainers{
    public DialogueListTypeParent dialogueListTypeParent;
    public DialogueListType_Home_Intro dialogueListType_Home_Intro;
    public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
    public DialogueListType_Home_QuizExplanation dialogueListType_Home_QuizExplanation;
    public DialogueListType_Bleeding_WrongItem dialogueListType_Bleeding_WrongItem;
    public SODialogue dialogue;
}
public class SODialogueListContainer : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/DialogueListContainer")]
    public static void QuickCreate()
    {
        SODialogueListContainer asset = CreateInstance<SODialogueListContainer>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Cutscene//DialogueListContainer.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public DialogueListTypeParent dialogueListTypeParent;
    public DialogueListContainers[] dialogueListContainers;
}
