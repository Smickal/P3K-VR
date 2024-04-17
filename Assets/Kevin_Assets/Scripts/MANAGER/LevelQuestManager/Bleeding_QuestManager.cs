using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bleeding_QuestManager : QuestManager
{
    IEnumerator bleedingCoroutine;
    public bool DebugChooseWithItemFirst, DebugChooseWithoutItemFirst;
    public bool isBWIDone, isBWDone;
    public bool hasDissatisfaction = true, isTrashEverywhere = false;
    [Header ("All Bleeding GameObject and Manager - Without Item")]
    [SerializeField]private Patient_Bleeding patient_Bleeding;
    
    protected override void Quest()
    {
        OnStartQuest.Invoke();// krn ud ga berhubungan ama questmanager jd hrsnya aman..
        timerInSecs = 0;
        questManagerUI.SetTimerSlider(timerInSecs);
        
        isQuestStart = true;
        
        // questManagerUI.OpenHelper_Bleeding_All();
        
        // DialogueManager.PlaySceneDialogue(DialogueListType.Home_QuizExplanation_3_14);
        
        questManagerUI.OpenHelper_Bleeding_WithoutItem();
        

        bleedingCoroutine = Bleeding();
        StartCoroutine(bleedingCoroutine);
    }
    protected override void ScoreCounter()
    {
        questManagerUI.CloseHelper_Bleeding_WithItem();
        if(isTimerUp)
        {
            score = ScoreName.Sad_Face;
            if(bleedingCoroutine != null)StopCoroutine(bleedingCoroutine);
            patient_Bleeding.StopCoroutines();
            
            if(patient_Bleeding.IsDoneFirstAid)
            {
                score = ScoreName.Small_Happy_Face;
                if(!hasDissatisfaction && !isTrashEverywhere) score = ScoreName.Big_Happy_Face;
            }
        }
        base.ScoreCounter();
    }

    private IEnumerator Bleeding()
    {
        yield return new WaitUntil(()=> patient_Bleeding.IsDoneFirstAid);
        bleedingCoroutine = null;
        if(isQuestStart)
        {
            isTimerUp = true;
            QuestDone();
        }
        
    }
    protected override void ResetQuest()
    {
        if(bleedingCoroutine != null)StopCoroutine(bleedingCoroutine);
        patient_Bleeding.StopCoroutines();
        base.ResetQuest();
    }
}
