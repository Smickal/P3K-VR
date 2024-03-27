using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Choking_QuestManager : QuestManager
{
    private IEnumerator chokingCourotine;
    [Header("Reference")]
    [SerializeField] private PatientBackBlowHeimlich patientChoking;
    [Header("Data")]
    [SerializeField]private float progressTotal;
    private float progressNow;
    [SerializeField]private float[] timerTarget;
    
    public static Action<float> AddProgressBar;
    // public static Action Questt;
    protected override void Awake() 
    {
        base.Awake();
        AddProgressBar += AddProgress;
        // Questt += StartQuest;
    }
    protected override void Quest()
    {
        questManagerUI.OpenHelper_Choking();
        chokingCourotine = ChokingStart();
        StartCoroutine(chokingCourotine);

    }

    protected override void ScoreCounter()
    {
        if(timerInSecs <= timerTarget[0])
        {
            score = ScoreName.Big_Happy_Face;
            
        }
        else if(timerInSecs <= timerTarget[1])
        {
            score = ScoreName.Small_Happy_Face;
        }
        else
        {
            score = ScoreName.Sad_Face;
        }
        PlayerManager.HasBeatenLvl((int)levelP3KTypeNow, score);
    }

    private IEnumerator ChokingStart()
    {
        // Debug.Log("Start BangNGGGGGGGGGGGGGGGGG");
        Debug.Log("ShowBekblow");
        yield return new WaitUntil(()=> patientChoking.BackBlowDone);
        Debug.Log("ShowGeimlich");
        yield return new WaitUntil(()=> patientChoking.HeimlichDone);
        //do the dialogue
        questManagerUI.ShowTimer();
        isQuestStart = true;
        chokingCourotine = null;
    }

    private void AddProgress(float progress)
    {
        if(isQuestStart)
        {
            progressNow += progress;
            if(progressNow >= progressTotal)
            {
                questManagerUI.ShowTimer();
                QuestDone();
                Debug.Log("Quest Selesai coii");
            }
        }
        
    }

    protected override void ResetQuest()
    {
        if(chokingCourotine != null)StopCoroutine(chokingCourotine);
        base.ResetQuest();
    }
}
