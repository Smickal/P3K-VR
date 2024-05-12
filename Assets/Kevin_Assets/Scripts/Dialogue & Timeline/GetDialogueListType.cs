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
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_WrongItem)
            {
                if(dialogueListType_Bleeding_WrongItems == null)dialogueListType_Bleeding_WrongItems = (DialogueListType_Bleeding_WrongItem[])Enum.GetValues(typeof(DialogueListType_Bleeding_WrongItem));

                if(enumNumber > dialogueListType_Bleeding_WrongItems.Length)return dialogueListType_Bleeding_WrongItems[0];
                return dialogueListType_Bleeding_WrongItems[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Intro)
            {
                if(dialogueListType_Choking_Intros == null)dialogueListType_Choking_Intros = (DialogueListType_Choking_Intro[])Enum.GetValues(typeof(DialogueListType_Choking_Intro));

                if(enumNumber > dialogueListType_Choking_Intros.Length)return dialogueListType_Choking_Intros[0];
                return dialogueListType_Choking_Intros[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Intro)
            {
                if(dialogueListType_Bleeding_Intros == null)dialogueListType_Bleeding_Intros = (DialogueListType_Bleeding_Intro[])Enum.GetValues(typeof(DialogueListType_Bleeding_Intro));

                if(enumNumber > dialogueListType_Bleeding_Intros.Length)return dialogueListType_Bleeding_Intros[0];
                return dialogueListType_Bleeding_Intros[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Explanation)
            {
                if(dialogueListType_Choking_Explanations == null)dialogueListType_Choking_Explanations = (DialogueListType_Choking_Explanation[])Enum.GetValues(typeof(DialogueListType_Choking_Explanation));

                if(enumNumber > dialogueListType_Choking_Explanations.Length)return dialogueListType_Choking_Explanations[0];
                return dialogueListType_Choking_Explanations[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Explanation)
            {
                if(dialogueListType_Bleeding_Explanations == null)dialogueListType_Bleeding_Explanations = (DialogueListType_Bleeding_Explanation[])Enum.GetValues(typeof(DialogueListType_Bleeding_Explanation));

                if(enumNumber > dialogueListType_Bleeding_Explanations.Length)return dialogueListType_Bleeding_Explanations[0];
                return dialogueListType_Bleeding_Explanations[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Choking_Ending)
            {
                if(dialogueListType_Choking_Endings == null)dialogueListType_Choking_Endings = (DialogueListType_Choking_Ending[])Enum.GetValues(typeof(DialogueListType_Choking_Ending));

                if(enumNumber > dialogueListType_Choking_Endings.Length)return dialogueListType_Choking_Endings[0];
                return dialogueListType_Choking_Endings[enumNumber];
            }
            if(dialogueListTypeParent == DialogueListTypeParent.Bleeding_Ending)
            {
                if(dialogueListType_Bleeding_Endings == null)dialogueListType_Bleeding_Endings = (DialogueListType_Bleeding_Ending[])Enum.GetValues(typeof(DialogueListType_Bleeding_Ending));

                if(enumNumber > dialogueListType_Bleeding_Endings.Length)return dialogueListType_Bleeding_Endings[0];
                return dialogueListType_Bleeding_Endings[enumNumber];
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
