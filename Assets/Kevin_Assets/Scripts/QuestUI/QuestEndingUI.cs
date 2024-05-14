using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class QuestEndingUI : MonoBehaviour, ITurnOffStatic
{
    [Header("Reference")]
    [SerializeField] private KitUiManager _kitUIMgr;
    [SerializeField] private UILangkahP3K _uiLangkahP3K;
    [SerializeField] Transform _stepTransform;
    [SerializeField] DialogueManager dialogueManager;
    private ScoreName scoreNowName;
    [Header("Container")]
    [SerializeField] private GameObject[] _endingContainer;
    [SerializeField] private GameObject _evalContainer, _GlossaryContainer;
    [SerializeField] private Button _nextButton, _restartButton, _quitButton;
    [Header("Evaluation")]
    [SerializeField] private Image _scoreIMG; 
    [SerializeField] private TMP_Text _scoreDescText;
    [SerializeField] private Sprite[] _scoreSprite;
    [SerializeField] private TMP_Text _scoreNameText;
    [SerializeField] private GameObject endButton;
    [Header("Glossary")]
    [SerializeField]private TMP_Text _procedureNameText;
    [SerializeField]private Image _procedureIMG;
    [SerializeField]private TMP_Text _procedureDescText;
    [SerializeField] UILangkahDescPrefab _langkahDescPrefab;
    List<UILangkahDescPrefab> listOfLangkahDescPrefab = new List<UILangkahDescPrefab>();

    [Header("Tutorial Data")]
    [SerializeField] SOLangkahP3K[] _soLangkahData;

    public static Action<ScoreName, LevelP3KType> SetUIData;
    public static Action ShowQuestEnding;
    private void Awake() 
    {
        SetUIData += SetData;
        ShowQuestEnding += ActivateQuestEnding;
    }
    public void TurnOffStatic()
    {
        SetUIData -= SetData;
        ShowQuestEnding -= ActivateQuestEnding;
    }
    private void Start() 
    {
        _nextButton.onClick.AddListener(ShowGlossary);
        _restartButton.onClick.AddListener(CloseQuestEnding);
        _quitButton.onClick.AddListener(CloseQuestEnding);
    }
    
    public void SetData(ScoreName scoreNow, LevelP3KType levelP3KTypeNow)
    {
        SOLangkahP3K selectedData = _soLangkahData[(int)levelP3KTypeNow-1];
        if(UILangkahP3K.UnlockLangkahSave != null)UILangkahP3K.UnlockLangkahSave(selectedData);
        scoreNowName = scoreNow;
        _scoreIMG.sprite = _scoreSprite[(int)scoreNow-1];

        string scoreName = scoreNow.ToString();
        scoreName = scoreName.Replace('_',' ');
        _scoreNameText.SetText(scoreName);

        _scoreDescText.text = selectedData._scoreNoteDesc[(int)scoreNow];
        SetGlossary(selectedData);
    }
    public void SetGlossary(SOLangkahP3K scriptableData)
    {
        _procedureNameText.SetText(scriptableData.ProcedureName);
        _procedureIMG.sprite = scriptableData.ProcedureIMG;
        _procedureDescText.SetText(scriptableData.ProcedureDescription);
        
        Transform _langkahDescPrefabTransform = Instantiate(scriptableData._prefabProcedure, _stepTransform);
        endButton.transform.SetAsLastSibling();
    }
    public void ActivateQuestEnding()
    {
        CloseUI();
        _kitUIMgr.ActivateBaseUI("Evaluation");
        _kitUIMgr.OpenQuestEnding();
        _evalContainer.gameObject.SetActive(true);
        if(GameManager.CheckLevelTypeNow == null)return;
        if(GameManager.CheckLevelTypeNow() == LevelP3KType.Choking)
        {
            if(scoreNowName == ScoreName.Sad_Face)
            {
                
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Ending, DialogueListType_Choking_Ending.Ending_Sad);
                // Debug.Log("Sad");
            }
            else if(scoreNowName == ScoreName.Small_Happy_Face)
            {
                
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Ending, DialogueListType_Choking_Ending.Ending_SmallHappy);
                // Debug.Log("Small_Happy_Face");
            }
            else if(scoreNowName == ScoreName.Big_Happy_Face)
            {
                
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Ending, DialogueListType_Choking_Ending.Ending_Happy);
                // Debug.Log("Big_Happy_Face");
            }
            
        }
        else if (GameManager.CheckLevelTypeNow() == LevelP3KType.Bleeding)
        {
            if(scoreNowName == ScoreName.Sad_Face)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Ending, DialogueListType_Bleeding_Ending.Ending_Sad);
            }
            else if(scoreNowName == ScoreName.Small_Happy_Face)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Ending, DialogueListType_Bleeding_Ending.Ending_SmallHappy);
            }
            else if(scoreNowName == ScoreName.Big_Happy_Face)
            {
                dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Ending, DialogueListType_Bleeding_Ending.Ending_Happy);
            }
        }
        
    }
    public void ShowGlossary()
    {
        _evalContainer.gameObject.SetActive(false);
        _GlossaryContainer.gameObject.SetActive(true);
        if(GameManager.CheckLevelTypeNow == null)return;
        if(GameManager.CheckLevelTypeNow() == LevelP3KType.Choking)
        {
            dialogueManager.PlayDialogueScene(DialogueListTypeParent.Choking_Ending, DialogueListType_Choking_Ending.EndingAfter);
            
        }
        else if (GameManager.CheckLevelTypeNow() == LevelP3KType.Bleeding)
        {
            dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Ending, DialogueListType_Bleeding_Ending.EndingAfter);
        }
    }
    public void CloseQuestEnding()
    {
        _kitUIMgr.DeactivateBaseUI();
    }
    public void CloseUI()
    {
        foreach (var obj in _endingContainer)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
