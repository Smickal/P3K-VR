using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using BNG;
using Oculus.Interaction.Locomotion;
using Oculus.Interaction.DistanceReticles;

[Serializable]
public class DialogueFinishActions
{
    public DialogueListTypeParent dialogueListTypeParent;
    [Space(5)]
    public DialogueListType_Home_Intro dialogueListType_Home_Intro;
    public DialogueListType_Home_Quiz dialogueListType_Home_Quiz;
    public DialogueListType_Home_QuizExplanation dialogueListType_Home_QuizExplanation;
    public DialogueListType_Bleeding_WrongItem dialogueListType_Bleeding_WrongItem;
    public DialogueListType_Choking_Intro dialogueListType_Choking_Intro;
    public DialogueListType_Bleeding_Intro dialogueListType_Bleeding_Intro;
    public DialogueListType_Choking_Explanation dialogueListType_Choking_Explanation;
    public DialogueListType_Bleeding_Explanation dialogueListType_Bleeding_Explanation;
    public DialogueListType_Choking_Ending dialogueListType_Choking_Ending;
    public DialogueListType_Bleeding_Ending dialogueListType_Bleeding_Ending;
    public UnityEvent OnDialogueFinish;
}




public class DialogueManager : MonoBehaviour, ITurnOffStatic
{
    [Header("Reference")]
    [SerializeField]private PlayerManager playerManager;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private ScreenFader screenFader;
    [SerializeField]private Robot robot;
    [Space(5)]
    [SerializeField]private SODialogueList SODialogueList;
    [SerializeField]private DialogueHolder dialogueHolder; // dialogueholder nerima scene dialogue, trus berdasarkan itu dikirim textnya beserta data berdasarkan namanya ke dialogue line
    [Header("Dialogue List Type")]
    
    private DialogueListTypeParent dialogueSceneTypeNow_Parent;  
    private System.Enum dialogueSceneTypeNow; 

    public static Action DoSomethingAfterFinish, HideFinishedDialogue_AfterFinishingTask;
    // public static Action<DialogueListTypeParent, System.Enum> PlaySceneDialogueNew;
    [Header("Action To Do When DialogueFinish")]
    [SerializeField]private DialogueFinishActions[] dialogueFinishActions;
    [Header("References for Action Finish")]
    [SerializeField]GameObject playerChecker, doorChecker;
    [SerializeField]PlayerTeleport playerTeleport;
    [SerializeField]Vector3 positionForIntro3Bleed;
    [SerializeField]Quaternion rotationforIntro3Bleed;
    [SerializeField]ToolTipHomeManager toolTipHomeManager;
    // [SerializeField]TeleportInteractable teleportInteractable;
    // [SerializeField]ReticleDataTeleport reticleDataTeleport;

    [Header("DEBUG ONLY")]
    public bool isPlayScene1AtStart;
    public bool isPlayScene2;
    public bool isFinishTaskScene2;

    private void Awake() 
    {
        DoSomethingAfterFinish += DoSomethingAfterDialogueFinish;
        HideFinishedDialogue_AfterFinishingTask += HideFinishedDialogueNow;
        // PlaySceneDialogueNew += PlayDialogueScene;
    }
    private void Update() {
        if(isPlayScene1AtStart)
        {
            isPlayScene1AtStart = false;
            PlayDialogueScene(DialogueListTypeParent.Home_QuizExplanation, DialogueListType_Home_QuizExplanation.Home_QuizExplanation_2_1);
        }
    }

    
    public void PlayDialogueScene<T>(DialogueListTypeParent dialogueListTypeParent, T enumValue) where T : struct, Enum
    {
        // dialogueSceneTypeNow = inputDialogueSceneType;
        dialogueSceneTypeNow_Parent = dialogueListTypeParent;
        dialogueSceneTypeNow = enumValue;

        SODialogue chosenDialogue = SODialogueList.SearchDialogue(dialogueListTypeParent, enumValue);
        // Debug.Log(chosenDialogue.name + "WOIII");
        // OnFinishDialogue.RemoveAllListeners();
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
    }
    
    public void PlayDialogueSceneOnEvent(GetDialogueListType inputDialogueSceneType)
    {
        dialogueSceneTypeNow_Parent = inputDialogueSceneType.dialogueListTypeParent;
        dialogueSceneTypeNow = inputDialogueSceneType.dialogueSceneTypeNow;
        SODialogue chosenDialogue = null;
        if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro)
        {
            DialogueListType_Home_Intro dialogueSceneType = (DialogueListType_Home_Intro)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Quiz)
        {
            DialogueListType_Home_Quiz dialogueSceneType = (DialogueListType_Home_Quiz)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation)
        {
            DialogueListType_Home_QuizExplanation dialogueSceneType = (DialogueListType_Home_QuizExplanation)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_WrongItem)
        {
            DialogueListType_Bleeding_WrongItem dialogueSceneType = (DialogueListType_Bleeding_WrongItem)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Intro)
        {
            DialogueListType_Choking_Intro dialogueSceneType = (DialogueListType_Choking_Intro)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Intro)
        {
            DialogueListType_Bleeding_Intro dialogueSceneType = (DialogueListType_Bleeding_Intro)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Explanation)
        {
            DialogueListType_Choking_Explanation dialogueSceneType = (DialogueListType_Choking_Explanation)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Explanation)
        {
            DialogueListType_Bleeding_Explanation dialogueSceneType = (DialogueListType_Bleeding_Explanation)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Ending)
        {
            DialogueListType_Choking_Ending dialogueSceneType = (DialogueListType_Choking_Ending)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
            
        }
        else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Ending)
        {
            DialogueListType_Bleeding_Ending dialogueSceneType = (DialogueListType_Bleeding_Ending)dialogueSceneTypeNow;
            chosenDialogue = SODialogueList.SearchDialogue(dialogueSceneTypeNow_Parent, dialogueSceneType);
        }

        
        
        if(chosenDialogue)
        {
            dialogueHolder.ShowDialogue(chosenDialogue);
        }
    }

    private void DoSomethingAfterDialogueFinish()
    {
        //kalo ga mo lwt code bs lewat inspector, kalo mo lwt code gausa tambahin di inspector, well sbnrnya tinggal ilangin return kalo mo 2-2nya
        foreach(DialogueFinishActions action in dialogueFinishActions)
        {
            if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro && action.dialogueListType_Home_Intro.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Quiz && action.dialogueListType_Home_Quiz.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_QuizExplanation && action.dialogueListType_Home_QuizExplanation.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_WrongItem && action.dialogueListType_Bleeding_WrongItem.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Intro && action.dialogueListType_Choking_Intro.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Intro && action.dialogueListType_Bleeding_Intro.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Explanation && action.dialogueListType_Choking_Explanation.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Explanation && action.dialogueListType_Bleeding_Explanation.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Ending && action.dialogueListType_Choking_Ending.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }
            else if(dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Ending && action.dialogueListType_Bleeding_Ending.Equals(dialogueSceneTypeNow))
            {
                action.OnDialogueFinish.Invoke();
            }

        }

        if (dialogueSceneTypeNow_Parent == DialogueListTypeParent.Choking_Intro)
        {
            if((DialogueListType_Choking_Intro)dialogueSceneTypeNow == DialogueListType_Choking_Intro.Choking_Intro_1)
            {
                PlayDialogueScene(DialogueListTypeParent.Choking_Intro, DialogueListType_Choking_Intro.Choking_Intro_2);
                return;
            }
            if((DialogueListType_Choking_Intro)dialogueSceneTypeNow == DialogueListType_Choking_Intro.Choking_Intro_2)
            {
                
                UnityAction afterIntro2_fadeOut = ()=>
                {
                    screenFader.ResetEvent();
                    gameManager.ChangeGameState(GameState.InGame);
                    if(PlayerRestriction.LiftAllRestriction != null)PlayerRestriction.LiftAllRestriction();
                    if(PlayerRestriction.LiftRotationRestriction != null)PlayerRestriction.LiftRotationRestriction();
                    robot.ActivateLookAt();
                    robot.ActivateFollowPlayer();
                    PlayDialogueScene(DialogueListTypeParent.Choking_Intro, DialogueListType_Choking_Intro.Choking_Intro_3);
                return;

                };
                UnityAction afterIntro2_fadeIn = ()=>
                {
                    screenFader.ResetEvent();
                    if(PlayerManager.HasFinishedIntroLevel != null)PlayerManager.HasFinishedIntroLevel((int)gameManager.LevelTypeNow());
                    if(EnvironmentLevelManager.SetEnvironment_AfterIntro != null)EnvironmentLevelManager.SetEnvironment_AfterIntro();
                    if(doorChecker)doorChecker.SetActive(true);
                    screenFader.AddEvent(afterIntro2_fadeOut);
                    screenFader.DoFadeOut();
                };
                screenFader.AddEvent(afterIntro2_fadeIn);
                screenFader.DoFadeIn();
                return;
            }
            
        }
        else if (dialogueSceneTypeNow_Parent == DialogueListTypeParent.Bleeding_Intro)
        {
            if((DialogueListType_Bleeding_Intro)dialogueSceneTypeNow == DialogueListType_Bleeding_Intro.Bleeding_Intro_1)
            {
                if(PlayerRestriction.LiftMovementRestriction != null)PlayerRestriction.LiftMovementRestriction();
                return;
            }
            if((DialogueListType_Bleeding_Intro)dialogueSceneTypeNow == DialogueListType_Bleeding_Intro.Bleeding_Intro_3_2)
            {
                
                UnityAction afterIntro2_fadeOut = ()=>
                {
                    screenFader.ResetEvent();
                    gameManager.ChangeGameState(GameState.InGame);
                    if(PlayerRestriction.LiftAllRestriction != null)PlayerRestriction.LiftAllRestriction();
                    if(PlayerRestriction.LiftRotationRestriction != null)PlayerRestriction.LiftRotationRestriction();
                    // teleportInteractable.AllowTeleport = true;
                    // reticleDataTeleport.ChangeReticleModeToValid();
                    robot.ActivateLookAt();
                    robot.ActivateFollowPlayer();
                    PlayDialogueScene(DialogueListTypeParent.Bleeding_Intro, DialogueListType_Bleeding_Intro.Bleeding_Intro_4);

                };
                UnityAction afterIntro2_fadeIn = ()=>
                {
                    screenFader.ResetEvent();
                    if(PlayerManager.HasFinishedIntroLevel != null)PlayerManager.HasFinishedIntroLevel((int)gameManager.LevelTypeNow());
                    playerChecker.SetActive(true);
                    if(doorChecker)doorChecker.SetActive(true);
                    screenFader.AddEvent(afterIntro2_fadeOut);
                    screenFader.DoFadeOut();
                };
                screenFader.AddEvent(afterIntro2_fadeIn);
                screenFader.DoFadeIn();
                return;
            }
        }
        else if (dialogueSceneTypeNow_Parent == DialogueListTypeParent.Home_Intro)
        {
            if((DialogueListType_Home_Intro)dialogueSceneTypeNow == DialogueListType_Home_Intro.Home_Intro_AfterQuiz)
            {
                
                UnityAction afterIntro2_fadeOut = ()=>
                {
                    screenFader.ResetEvent();
                    gameManager.ChangeGameState(GameState.InGame);
                    if(PlayerRestriction.LiftAllRestriction != null)PlayerRestriction.LiftAllRestriction();
                    if(PlayerRestriction.LiftRotationRestriction != null)PlayerRestriction.LiftRotationRestriction();
                    robot.ActivateLookAt();

                };
                UnityAction afterIntro2_fadeIn = ()=>
                {
                    screenFader.ResetEvent();
                    if(PlayerManager.HasFinishedTutorialMain != null)PlayerManager.HasFinishedTutorialMain();
                    toolTipHomeManager.Activate();
                    if(EnvironmentLevelManager.SetEnvironment_HomeAfterIntro != null)EnvironmentLevelManager.SetEnvironment_HomeAfterIntro();
                    screenFader.AddEvent(afterIntro2_fadeOut);
                    screenFader.DoFadeOut();
                };
                screenFader.AddEvent(afterIntro2_fadeIn);
                screenFader.DoFadeIn();
                return;
            }
        }
    }

    public void HideFinishedDialogueNow()
    {
        dialogueHolder.StopCoroutineAbruptly();
        dialogueHolder.HideDialogue();
    }
    
    public void TurnOffStatic()
    {
        DoSomethingAfterFinish -= DoSomethingAfterDialogueFinish;
        HideFinishedDialogue_AfterFinishingTask -= HideFinishedDialogueNow;
        // PlaySceneDialogue -= PlayDialogueScene;
    }

    public void PlayIntro()
    {
        if(!playerManager.IsFinish_TutorialMain() && gameManager.LevelModeNow() == LevelMode.Home)
        {
            gameManager.ChangeGameState(GameState.Cinematic);
            PlayDialogueScene(DialogueListTypeParent.Home_Intro, DialogueListType_Home_Intro.Home_Intro_Wake);
        }
        else if(gameManager.LevelModeNow() == LevelMode.Level)
        {
            if(!playerManager.IsFinish_IntroLevel((int)gameManager.LevelTypeNow()))
            {
                if(doorChecker)doorChecker.SetActive(false);
                if(gameManager.LevelTypeNow() == LevelP3KType.Choking)
                {
                    gameManager.ChangeGameState(GameState.Cinematic);
                    PlayDialogueScene(DialogueListTypeParent.Choking_Intro, DialogueListType_Choking_Intro.Choking_Intro_1);
                }
                else if(gameManager.LevelTypeNow() == LevelP3KType.Bleeding)
                {
                    gameManager.ChangeGameState(GameState.Cinematic);
                    PlayDialogueScene(DialogueListTypeParent.Bleeding_Intro, DialogueListType_Bleeding_Intro.Bleeding_Intro_1);
                }
            }
        }
    }
    public void PlayIntro3Bleed()
    {
        UnityAction afterIntro_fadeOut = ()=>
        {
            screenFader.ResetEvent();
            PlayDialogueScene(DialogueListTypeParent.Bleeding_Intro, DialogueListType_Bleeding_Intro.Bleeding_Intro_3_1);

        };
        UnityAction afterIntro_fadeIn = ()=>
        {
            screenFader.ResetEvent();
            if(PlayerRestriction.ApplyMovementRestriction != null)PlayerRestriction.ApplyMovementRestriction();
            if(PlayerRestriction.ApplyRotationRestriction != null)PlayerRestriction.ApplyRotationRestriction();

            HideFinishedDialogueNow();

            playerTeleport.TeleportPlayerAwake(positionForIntro3Bleed, rotationforIntro3Bleed);

            screenFader.AddEvent(afterIntro_fadeOut);
            screenFader.DoFadeOut();
        };
        screenFader.AddEvent(afterIntro_fadeIn);
        screenFader.DoFadeIn();
    }
    public void PlayAfterIntro3_1()
    {
        if(EnvironmentLevelManager.SetEnvironment_AfterIntro != null)EnvironmentLevelManager.SetEnvironment_AfterIntro();
        robot.SetPosForIntro();
        robot.ActivateLookAt();
        playerChecker.SetActive(false);
    }
    
}
