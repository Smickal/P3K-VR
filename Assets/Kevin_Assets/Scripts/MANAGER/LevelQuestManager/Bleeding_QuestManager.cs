using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using BNG;
using UnityEngine;

public class Bleeding_QuestManager : QuestManager, IPersistanceDataSave
{
    [Header("target time")]
    [SerializeField]private float timeTarget_WithoutItem, timeTarget_WithItem;

    [SerializeField] bool isTrashEverywhere = true;
    [SerializeField] int totalDissatisfactionMax;

    [Header ("All Bleeding GameObject and Manager - Without Item")]
    [SerializeField]private Patient_Bleeding patient_Bleeding;

    [SerializeField]PatientBleedingQuestUI patientBleedingQuestUI;
    public AudioClip SoundOnDissatisfy;
    [SerializeField]private float timeToFinish_WithoutItem, timeToFinish_WithItem; 
    

    IEnumerator bleedingCoroutine;
    private int totalDissatisfaction;



    protected override void Quest()
    {
        OnStartQuest.Invoke();// krn ud ga berhubungan ama questmanager jd hrsnya aman..
        timerInSecs = 0;
        timeToFinish_WithoutItem = 0;
        timeToFinish_WithItem = 0;
        questManagerUI.SetTimerSlider(timerInSecsMax);
        
        
        
        // questManagerUI.OpenHelper_Bleeding_All();
        
        // DialogueManager.PlaySceneDialogue(DialogueListType.Home_QuizExplanation_3_14);
        
        questManagerUI.OpenHelper_Bleeding_WithoutItem();
        

        bleedingCoroutine = Bleeding();
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Explanation, DialogueListType_Bleeding_Explanation.Bleeding_WithoutItem_Start);
        StartCoroutine(bleedingCoroutine);
    }
    public void StartTimer()
    {
        isQuestStart = true;
    }
    protected override void ScoreCounter()
    {
        // if(TrashCountManager.Instance)
        // {
        //     isTrashEverywhere = TrashCountManager.Instance.IsThereAnySmallTrash();
        // }
        questManagerUI.CloseHelper_Bleeding_WithItem();
        if(isTimerUp)
        {
            score = ScoreName.Sad_Face;
            if(bleedingCoroutine != null)StopCoroutine(bleedingCoroutine);
            patient_Bleeding.StopCoroutines();
            
            if(patient_Bleeding.IsDoneFirstAid)
            {
                score = ScoreName.Small_Happy_Face;
                // if(!isTrashEverywhere && totalDissatisfaction <= totalDissatisfactionMax) score = ScoreName.Big_Happy_Face;
                if(timeToFinish_WithoutItem <= timeTarget_WithoutItem && timeToFinish_WithItem <= timeTarget_WithItem)score = ScoreName.Big_Happy_Face;
                
            }
        }
        base.ScoreCounter();
    }

    private IEnumerator Bleeding()
    {
        yield return new WaitUntil(()=> patient_Bleeding.IsDoneFirstAid && isQuestStart);
        patientBleedingQuestUI.CloseALL();
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
    public void PatientDissatisfy()
    {
        dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Explanation, DialogueListType_Bleeding_Explanation.Bleeding_PatientDissatisfied);
        PlaySoundDissatisfy();
        totalDissatisfaction++;
    }

    public void PlaySoundDissatisfy()
    {
        if (SoundOnDissatisfy) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnDissatisfy, transform.position, 0.75f);
            }
        }
    }
    public void SaveFinish_WithoutItem()
    {
        timeToFinish_WithoutItem = timerInSecs;
        if(timeToFinish_WithoutItem <= timeTarget_WithoutItem)
        {
            questManagerUI.Change_BleedingEmotionScoreTracker_Place();
        }
    }
    public void SaveFinish_WithItem()
    {
        timeToFinish_WithItem = timerInSecs;
    }


    public void SaveData(GameData data)
    {
        data.Level2Name = LevelP3KType.Bleeding.ToString();

        data.Level2Score = score.ToString();
        data.Level2TimeToFinish = timerInSecs;
        data.Level2TimeToFinish_WithoutItem = timeToFinish_WithoutItem;
        data.Level2TimeToFinish_WithItem = timeToFinish_WithItem;
        data.TotalSmallTrashCount = TrashCountManager.Instance.TotalSmallTrash();
        data.TotalDissastifaction = totalDissatisfaction;

        data.IsLevel2Done = true;
    }

    public void LoadData(GameData data)
    {
        
    }

}
