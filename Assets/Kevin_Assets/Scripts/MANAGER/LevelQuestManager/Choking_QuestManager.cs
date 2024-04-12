using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Choking_QuestManager : QuestManager, ITurnOffStatic
{
    private IEnumerator chokingCourotine;
    [Header("Reference")]
    [SerializeField] private PatientBackBlowHeimlich patientChoking;
    [Header("Data")]
    [SerializeField]private float progressTotal;
    private float progressNow;
    [SerializeField]private float _minusProgress;
    [SerializeField]private float[] timerTarget;
    
    public static Action<float> AddProgressBar;
    // public static Action Questt;
    protected override void Awake() 
    {
        base.Awake();
        AddProgressBar += AddProgress;
        // Questt += StartQuest;
    }
    public void TurnOffStatic()
    {
        AddProgressBar -= AddProgress;
    }
    protected override void Update()
    {
        base.Update();
        if(isQuestStart && gameManager.GameStateNow() == GameState.InGame)
        {
            if(progressNow > 0 && progressNow < progressTotal)
            {
                progressNow -= (Time.deltaTime * _minusProgress);
            }
            else if(progressNow < 0)
            {
                progressNow = 0;
            }
        }
    }
    protected override void Quest()
    {
        OnStartQuest.Invoke();// krn ud ga berhubungan ama questmanager jd hrsnya aman..
        timerInSecs = 0;
        questManagerUI.SetTimerSlider(timerInSecs);

        questManagerUI.OpenHelper_Choking();
        chokingCourotine = ChokingStart();
        StartCoroutine(chokingCourotine);

    }

    protected override void ScoreCounter()
    {
        score = ScoreName.Sad_Face;
        
        if(timerInSecs <= timerTarget[1])
        {
            score = ScoreName.Small_Happy_Face;
        }
        if(timerInSecs <= timerTarget[0])
        {
            score = ScoreName.Big_Happy_Face;
        }
        base.ScoreCounter();
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
                patientChoking.UnActivateAll();
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
