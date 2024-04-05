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
    [Header("Container")]
    [SerializeField] private GameObject[] _endingContainer;
    [SerializeField] private GameObject _evalContainer, _GlossaryContainer;
    [SerializeField] private Button _nextButton, _restartButton, _quitButton;
    [Header("Evaluation")]
    [SerializeField] private Image _scoreIMG; 
    [SerializeField] private TMP_Text _scoreDescText;
    [SerializeField] private Sprite[] _scoreSprite;
    [SerializeField] private TMP_Text _scoreNameText;
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
        UILangkahP3K.UnlockLangkahSave(selectedData);

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
        
        //Create Step Procedure
        for(int i = 0; i < scriptableData.Procedures.Count; i++)
        {
            UILangkahDescPrefab newUI = Instantiate(_langkahDescPrefab, _stepTransform);
            newUI.SetData((i+1).ToString(),
                          scriptableData.Procedures[i].StepsDescription, 
                          scriptableData.Procedures[i].StepsSprite);
            listOfLangkahDescPrefab.Add(newUI);
        }

    }
    public void ActivateQuestEnding()
    {
        CloseUI();
        _kitUIMgr.ActivateBaseUI("Evaluation");
        _kitUIMgr.OpenQuestEnding();
        _evalContainer.gameObject.SetActive(true);
    }
    public void ShowGlossary()
    {
        _evalContainer.gameObject.SetActive(false);
        _GlossaryContainer.gameObject.SetActive(true);
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
