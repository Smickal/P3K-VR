using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Choking_QuestManager : QuestManager, IPersistanceDataSave, ITurnOffStatic
{
    private IEnumerator chokingCourotine;
    [Header("Reference")]
    [SerializeField] private PatientBackBlowHeimlich patientChoking;
    [Header("Data")]
    [SerializeField]private float progressTotal;
    private float progressNow;
    [SerializeField]private float _minusProgress;
    [SerializeField]private float[] timerTarget;
    [Header("Change Skin")]
    [SerializeField]SkinnedMeshRenderer skinnedMesh;
    [SerializeField]Material[] materials_RedSkin, materials_RedderSkin;
    bool hasChangeRedSkin, hasChangeRedderSkin;
    
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
            if(timerInSecs > timerTarget[1])
            {
                if(!hasChangeRedderSkin)
                {
                    hasChangeRedderSkin = true;
                    skinnedMesh.materials = materials_RedderSkin;

                    dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_GettingRedder_2);
                }
                
                
            }
            else if(timerInSecs > timerTarget[0])
            {
                
                if(!hasChangeRedSkin)
                {
                    hasChangeRedSkin = true;
                    skinnedMesh.materials = materials_RedSkin;

                    dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_GettingRedder);
                }
            }
        }
        
    }
    protected override void Quest()
    {
        OnStartQuest.Invoke();// krn ud ga berhubungan ama questmanager jd hrsnya aman..
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_Exp_1);
        timerInSecs = 0;
        questManagerUI.SetTimerSlider(timerInSecsMax);

        questManagerUI.OpenHelper_Choking();
        chokingCourotine = ChokingStart();
        StartCoroutine(chokingCourotine);

    }

    protected override void ScoreCounter()
    {
        score = ScoreName.Sad_Face;
        
        
        if(timerInSecs <= timerTarget[0])
        {
            score = ScoreName.Big_Happy_Face;
        }
        else if(timerInSecs <= timerTarget[1])
        {
            score = ScoreName.Small_Happy_Face;
        }
        base.ScoreCounter();
    }

    private IEnumerator ChokingStart()
    {
        yield return new WaitUntil(()=> patientChoking.BackBlowDone);
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_Exp_2);

        yield return new WaitUntil(()=> patientChoking.HeimlichDone);
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_Exp_3);
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
                // Debug.Log("Quest Selesai coii");
            }
        }
        
    }

    protected override void ResetQuest()
    {
        if(chokingCourotine != null)StopCoroutine(chokingCourotine);
        base.ResetQuest();
    }
    public void PlayDialogueBackBlow()
    {
        if(!isQuestStart)return;
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_Exp_Backblow);
    }
    public void PlayDialogueHeimlich()
    {
        if(!isQuestStart)return;
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Explanation, DialogueListType_Choking_Explanation.Choking_Exp_Heimlich);
    }

    public void SaveData(GameData data)
    {
        data.Level1Name = LevelP3KType.Choking.ToString();
        data.Level1Score = score.ToString();
        data.Level1TimeToFinish = timerInSecs;

        data.IsLevel1Done = true;
    }

    public void LoadData(GameData data)
    {
        
    }
}
