using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDialogueListType : MonoBehaviour
{
    [Header("Di atas 0")]
    public int enumNumber;
    public DialogueListTypeParent dialogueListTypeParent;
    public Enum dialogueSceneTypeNow{
        get 
        { 
            if(dialogueListTypeParent == DialogueListTypeParent.Home_Intro)
            {
                if(dialogueListType_Home_Intro == null)dialogueListType_Home_Intro = (DialogueListType_Home_Intro[])Enum.GetValues(typeof(DialogueListType_Home_Intro));

                if(enumNumber > dialogueListType_Home_Intro.Length)return dialogueListType_Home_Intro[0];
                return dialogueListType_Home_Intro[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Home_Quiz)
            {
                if(dialogueListType_Home_Quiz == null)dialogueListType_Home_Quiz = (DialogueListType_Home_Quiz[])Enum.GetValues(typeof(DialogueListType_Home_Quiz));

                if(enumNumber > dialogueListType_Home_Quiz.Length)return dialogueListType_Home_Quiz[0];
                return dialogueListType_Home_Quiz[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Home_QuizExplanation)
            {
                if(dialogueListType_Home_QuizExplanation == null)dialogueListType_Home_QuizExplanation = (DialogueListType_Home_QuizExplanation[])Enum.GetValues(typeof(DialogueListType_Home_QuizExplanation));

                if(enumNumber > dialogueListType_Home_QuizExplanation.Length)return dialogueListType_Home_QuizExplanation[0];
                return dialogueListType_Home_QuizExplanation[enumNumber];
            }
            return dialogueListType_Home_Intro[0];
        }
    } 
    private DialogueListType_Home_Intro[] dialogueListType_Home_Intro;
    private DialogueListType_Home_Quiz[] dialogueListType_Home_Quiz;
    private DialogueListType_Home_QuizExplanation[] dialogueListType_Home_QuizExplanation;
    private DialogueListType_Bleeding_WrongItem[] dialogueListType_Bleeding_WrongItems;
    private DialogueListType_Choking_Intro[] dialogueListType_Choking_Intros;
    private DialogueListType_Bleeding_Intro[] dialogueListType_Bleeding_Intros;
    private DialogueListType_Choking_Explanation[] dialogueListType_Choking_Explanations;
    private DialogueListType_Bleeding_Explanation[] dialogueListType_Bleeding_Explanations;
    private DialogueListType_Choking_Ending[] dialogueListType_Choking_Endings;
    private DialogueListType_Bleeding_Ending[] dialogueListType_Bleeding_Endings;

}
