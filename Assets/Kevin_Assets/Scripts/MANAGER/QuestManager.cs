using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private LevelPlayerData levelPlayerDataNow; // ambil di awal utk punya data level
    private LevelP3KType levelP3KTypeNow;
    [SerializeField]private PlayerManager playerManager;
    [SerializeField]private GameManager gameManager;

    [Header("Level Data")]
    [Tooltip("Urutan berdasarkan enum levelp3ktype")]
    public float[] timerInSecs;
    private IEnumerator chokingCourotine;
    private void Awake() 
    {
        levelP3KTypeNow = gameManager.levelTypeNow();
        levelPlayerDataNow = playerManager.GetLevelData((int)levelP3KTypeNow);
    }
    private void CheckStartQuest()
    {
        if(!levelPlayerDataNow.hasBeatenLevelOnce)
        {
            StartQuest();
        }
        else
        {
            //open ui
        }
    }
    public void StartQuest()
    {
        //fade in fade out - or reset scene
        PlayerManager.ChangeInGame_Mode_Now(InGame_Mode.FirstAid);
        if(levelP3KTypeNow == LevelP3KType.Choking)
        {
            // chokingCourotine = ChokingTutorial();
            StartCoroutine(chokingCourotine);
        }
        
        else if (levelP3KTypeNow == LevelP3KType.Bleeding)BleedingQuest();
        

    }

    // private IEnumerator ChokingTutorial()
    // {
    //     //buka ui bla bla
    //     // yield return new WaitUntil(); - tunggu sampai bekblow 5x

    //     //buka ui bla bla
    //     // yield return new WaitUntil(); - tunggu sampai heimlich 5x
    //     ChokingQuest();
    // }
    public void ChokingQuest()
    {
        
    }
    public void BleedingQuest()
    {
        
    }

    //bikin code start quest yg jalanin quest pas player ada di certain areanya - kalo ud pernah dijalanin buka ui trus minta jawaban kalo yes berarti yauda

    //masuk ke quest, kalo level 1, lakuin nyalain ui levelhelper, tutorial 2 kali, trus nyalain timer, trus br ganti gantian, timer slsain, dptin skor akhir, penilaian, abis liatin ksh liat glossary the end. - ui mungkin perlu dikasi jarak hm dn dia diem di tmpt doang

    //lsg timer sambil ui helper nyala, trus ngikutin terus sampe slsai, penilaian, liat glossary the end

}
