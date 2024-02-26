using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UILangkahP3K : MonoBehaviour, IKitUI
{
    [Header("DescriptionDetails")]

    [SerializeField] TMP_Text _tutorialNameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] Image _tutorialIMG;



    [Header("Reference")]
    [SerializeField] GameObject _masterContainerOBJ;
    [SerializeField] GameObject _containerChoiceOBJ;
    [SerializeField] Transform _choiceTransform;
    [SerializeField] Transform _stepTransform;

    [Space(2)]
    [SerializeField] GameObject _containerDescOBJ;


    [Space(4)]
    [SerializeField] Button _backButton;

    [Header("Tutorial Data")]
    [SerializeField] SOLangkahP3K[] _soLangkahData;

    [Header("Prefab")]
    [SerializeField] UILangkahPrefab _langkahPrefab;
    [SerializeField] UILangkahDescPrefab _langkahDescPrefab;


    List<UILangkahDescPrefab> listOfLangkahDescPrefab = new List<UILangkahDescPrefab>();

    private void Start()
    {
        //Create Choice Data
        for(int i = 0; i < _soLangkahData.Length; i++)
        {
            UILangkahPrefab newUI = Instantiate(_langkahPrefab, _choiceTransform);
            newUI.SetData(_soLangkahData[i].ProcedureName, this, _soLangkahData[i]);
            newUI.SetState(_soLangkahData[i].ProcedureIMG, true);

        }

        _backButton.onClick.AddListener(StartData);
    }

    public void StartData()
    {
        _masterContainerOBJ.SetActive(true);

        _containerChoiceOBJ.SetActive(true);
        _containerDescOBJ.SetActive(false);

        ResetProcedureDescription();
    }

    public void ResetData()
    {
        _masterContainerOBJ.SetActive(false);

        _containerChoiceOBJ.SetActive(true);
        _containerDescOBJ.SetActive(false);
    }

    public void ActivateDescription(SOLangkahP3K scriptableData)
    {
        _containerChoiceOBJ.SetActive(false);
        _containerDescOBJ.SetActive(true);


        _tutorialNameText.SetText(scriptableData.ProcedureName);
        _tutorialIMG.sprite = scriptableData.ProcedureIMG;
        _descriptionText.SetText(scriptableData.ProcedureDescription);

        ResetProcedureDescription();
        
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


    private void ResetProcedureDescription()
    {
        //Check For Doubles
        if (listOfLangkahDescPrefab.Count > 0)
        {
            foreach (UILangkahDescPrefab ui in listOfLangkahDescPrefab)
            {
                UILangkahDescPrefab tempUI = ui;
                listOfLangkahDescPrefab.Remove(ui);
                Destroy(tempUI);
            }

            listOfLangkahDescPrefab.Clear();
        }
    }
}
