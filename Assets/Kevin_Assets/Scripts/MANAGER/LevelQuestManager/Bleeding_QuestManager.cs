using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bleeding_QuestManager : QuestManager
{
    IEnumerator bleedingWithItemCourotine, bleedingWithoutItemCourotine;
    public bool DebugChooseWithItemFirst, DebugChooseWithoutItemFirst;
    public bool isBWIDone, isBWDone;
    public bool hasDissatisfaction = true;
    
    protected override void Quest()
    {
        OnStartQuest.Invoke();// krn ud ga berhubungan ama questmanager jd hrsnya aman..
        timerInSecs = 0;
        questManagerUI.SetTimerSlider(timerInSecs);
        
        isQuestStart = true;
        
        // questManagerUI.OpenHelper_Bleeding_All();
        
        
        questManagerUI.OpenHelper_Bleeding_WithoutItem();
        bleedingWithoutItemCourotine = BleedingWithoutItem();
        StartCoroutine(bleedingWithoutItemCourotine);
    }
    protected override void Update()
    {
        base.Update();
        // if(DebugChooseWithItemFirst)
        // {
        //     //aslinya dijalanin dgn dia ngelakuin langkah prtm yg mana dulu.
        //     DebugChooseWithItemFirst = false;
        //     questManagerUI.CloseHelper_Bleeding_WithoutItem();
            
        //     //matiin collider si yg ga ada item 

        //     bleedingWithItemCourotine = BleedingWithItem();
        //     StartCoroutine(bleedingWithItemCourotine);
        // }
        // if(DebugChooseWithoutItemFirst)
        // {
            
        //     //aslinya dijalanin dgn dia ngelakuin langkah prtm yg mana dulu.
        //     DebugChooseWithoutItemFirst = false;
        //     questManagerUI.CloseHelper_Bleeding_WithItem();
            
        //     //matiin collider si yg ga ada item 

            
        // }
    }
    protected override void ScoreCounter()
    {
        questManagerUI.CloseHelper_Bleeding_WithItem();
        if(isTimerUp)
        {
            score = ScoreName.Sad_Face;
            if(isBWDone && isBWIDone)
            {
                score = ScoreName.Small_Happy_Face;
                if(!hasDissatisfaction) score = ScoreName.Big_Happy_Face;
            }
        }
        base.ScoreCounter();
    }

    private IEnumerator BleedingWithItem()
    {
        yield return new WaitUntil(()=> isBWIDone);
        bleedingWithItemCourotine = null;
        if(!isBWDone)
        {
            
            //nyalakan collider part 2
            questManagerUI.CloseHelper_Bleeding_WithItem();
            questManagerUI.OpenHelper_Bleeding_WithoutItem();
            bleedingWithoutItemCourotine = BleedingWithoutItem();
            StartCoroutine(bleedingWithoutItemCourotine);
        }
        else
        {
            if(!hasDissatisfaction)
            {
                isTimerUp = true;
                QuestDone();
            }
        }
    }  
    private IEnumerator BleedingWithoutItem()
    {
        yield return new WaitUntil(()=> isBWDone);
        if(!isBWIDone)
        {
            questManagerUI.OpenHelper_Bleeding_WithItem();
            
            //nyalakan collider part 2
            questManagerUI.CloseHelper_Bleeding_WithoutItem();
            bleedingWithItemCourotine = BleedingWithItem();
            StartCoroutine(bleedingWithItemCourotine);
        }
        else
        {
            if(!hasDissatisfaction)
            {
                isTimerUp = true;
                QuestDone();
            }
        }
    }
    protected override void ResetQuest()
    {
        if(bleedingWithoutItemCourotine != null)StopCoroutine(bleedingWithoutItemCourotine);
        if(bleedingWithItemCourotine != null)StopCoroutine(bleedingWithItemCourotine);
        base.ResetQuest();
    }
}
