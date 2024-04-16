using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
public class UILangkahP3K : BaseKitUI, ITurnOffStatic
{
    const string SaveStateKey = "LangkahSaveData_Key";

    [Header("DescriptionDetails")]

    [SerializeField] TMP_Text _tutorialNameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] Image _tutorialIMG;

    [Header("Reference")]
    [SerializeField] GameObject _masterContainerOBJ;
    [SerializeField] GameObject _containerChoiceOBJ;
    [SerializeField] Transform _choiceTransform;
    [SerializeField] Transform _stepTransform;
    [SerializeField] RectTransform _contentDescSlider;
    private SOLangkahP3K lastSO;

    [Space(2)]
    [SerializeField] GameObject _containerDescOBJ;


    [Space(4)]
    [SerializeField] Button _backButton;

    [Header("Tutorial Data")]
    [SerializeField] SOLangkahP3K[] _soLangkahData;

    [Header("Prefab")]
    [SerializeField] UILangkahPrefab _langkahPrefab;
    [SerializeField] UILangkahDescPrefab _langkahDescPrefab;
    [SerializeField] Transform _langkahDescPrefabTransform;
    

    bool[] UnlockedLangkahSavedData;
    List<UILangkahPrefab> ListOfDataInstance = new List<UILangkahPrefab>();

    List<UILangkahDescPrefab> listOfLangkahDescPrefab = new List<UILangkahDescPrefab>();

    public static Action<SOLangkahP3K> UnlockLangkahSave;
    public static Action ResetSaveData;

    [Header("Debug")]
    public bool Unlock;
    public SOLangkahP3K saveExample;
    private void Awake() 
    {
        UnlockLangkahSave += UnlockLangkah;
        ResetSaveData += ResetSave;

        Load();
    }

    private void Start()
    {
        //Create Choice Data
        for(int i = 0; i < _soLangkahData.Length; i++)
        {
            UILangkahPrefab newUI = Instantiate(_langkahPrefab, _choiceTransform);
            newUI.SetData(_soLangkahData[i].ProcedureName, this, _soLangkahData[i]);
            newUI.SetState(UnlockedLangkahSavedData[i]);

            ListOfDataInstance.Add(newUI);
        }

        _backButton.onClick.AddListener(StartData);
    }

    private void Update() 
    {
        if(Unlock)
        {
            Unlock = false;
            UnlockLangkah(saveExample);
        }
        
    }

    public override void StartData()
    {
        _masterContainerOBJ.SetActive(true);

        _containerChoiceOBJ.SetActive(true);
        _containerDescOBJ.SetActive(false);

        ResetProcedureDescription();
    }

    public override void ResetData()
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
        // for(int i = 0; i < scriptableData.Procedures.Count; i++)
        // {
        //     UILangkahDescPrefab newUI = Instantiate(_langkahDescPrefab, _stepTransform);
        //     newUI.SetData((i+1).ToString(),
        //                   scriptableData.Procedures[i].StepsDescription, 
        //                   scriptableData.Procedures[i].StepsSprite);
        //     listOfLangkahDescPrefab.Add(newUI);
        // }
        if(lastSO != null)
        {
            if(lastSO != scriptableData)_contentDescSlider.localPosition = new Vector3(_contentDescSlider.position.x, 0, 0);
            
        }
        lastSO = scriptableData;
        _langkahDescPrefabTransform = Instantiate(scriptableData._prefabProcedure, _stepTransform);
        

    }


    private void ResetProcedureDescription()
    {
        //Check For Doubles
        // if (listOfLangkahDescPrefab.Count > 0)
        // {
        //     int listLength = listOfLangkahDescPrefab.Count;
        //     for(int i=listLength - 1;i>=0 ;i--)
        //     {
        //         UILangkahDescPrefab tempUI = listOfLangkahDescPrefab[i];
        //         listOfLangkahDescPrefab.Remove(tempUI);
        //         Destroy(tempUI);
        //     }

        //     listOfLangkahDescPrefab.Clear();
        // }

        if(_langkahDescPrefabTransform != null)
        {
            Destroy(_langkahDescPrefabTransform.gameObject);
            _langkahDescPrefabTransform = null;
        }
        
    }

    private void UnlockLangkah(SOLangkahP3K scriptableData)
    {
        int i=0;
        foreach(var kitInstance in ListOfDataInstance)
        {
            if(kitInstance.Data.ProcedureName == scriptableData.ProcedureName && kitInstance.State == false)
            {
                //Debug.Log(scriptableData.KitName + " Unlocked");

                kitInstance.SetState(true);

                UnlockedLangkahSavedData[i] = true;

                Save();
                break;
            }
            i++;
        }

    }

    private void Save()
    {
        string Json = JsonConvert.SerializeObject(UnlockedLangkahSavedData);
        PlayerPrefs.SetString(SaveStateKey, Json);
        PlayerPrefs.Save();
    }

    private void Load()
    {

        if (PlayerPrefs.HasKey(SaveStateKey))
        {
            string loadString = PlayerPrefs.GetString(SaveStateKey);
            UnlockedLangkahSavedData = JsonConvert.DeserializeObject<bool[]>(loadString);
        }

        else
        {
            UnlockedLangkahSavedData = new bool[_soLangkahData.Length];

        }

    }

    private void ResetSave()
    {
        UnlockedLangkahSavedData = new bool[_soLangkahData.Length];
        Save();
    }

    public void TurnOffStatic()
    {
        UnlockLangkahSave -= UnlockLangkah;
        ResetSaveData -= ResetSave;
    }
}
