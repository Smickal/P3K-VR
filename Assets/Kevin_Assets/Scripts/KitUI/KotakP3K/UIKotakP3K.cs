using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class UIKotakP3K : BaseKitUI
{
    const string SaveStateKey = "toolsSaveData_Key";

    [Header("DescriptionDetails")]
    [SerializeField] TMP_Text _toolsNameText;
    [SerializeField] Image _descIconIMG;
    [SerializeField] TMP_Text _descText;


    [Header("Reference")]
    [SerializeField] GameObject _containerMasterOBJ;
    [SerializeField] GameObject _choiceContainerOBJ;
    [SerializeField] GameObject _descContainerOBJ;
    [SerializeField] Button _backBtn;

    [SerializeField] Transform _contentTransform;

    [Header("Prefabs")]
    [SerializeField] UIKotakP3KDescPrefab _descPrefab;

    [Header("KotakP3K_Scriptables")]
    [SerializeField] SOKotakP3K[] _scriptableDatas;

    public static Action<SOKotakP3K> CheckUnlock;

    bool[] UnlockedKitSavedData;
    List<UIKotakP3KDescPrefab> ListOfDataInstance = new List<UIKotakP3KDescPrefab>();

    private void Awake()
    {
        CheckUnlock += UnlockAKit;

        

        Load();
    }

    private void Start()
    {
        _backBtn.onClick.AddListener(StartData);

        for(int i = 0; i < _scriptableDatas.Length; i++)
        {
            UIKotakP3KDescPrefab newUI = Instantiate(_descPrefab, _contentTransform);
            newUI.SetData(_scriptableDatas[i], this);
            newUI.SetState(UnlockedKitSavedData[i]);

            ListOfDataInstance.Add(newUI);
        }
    }

    public override void StartData()
    {
        _containerMasterOBJ.SetActive(true);

        _choiceContainerOBJ.SetActive(true);
        _descContainerOBJ.SetActive(false);
    }
    public override void ResetData()
    {
        _containerMasterOBJ.SetActive(false);

        _choiceContainerOBJ.SetActive(true);
        _descContainerOBJ.SetActive(false);
    }

    public void ActivateDescription(SOKotakP3K scriptableData)
    {
        _choiceContainerOBJ.SetActive(false);
        _descContainerOBJ.SetActive(true);

        _toolsNameText.SetText(scriptableData.KitName.ToString());
        _descIconIMG.sprite = scriptableData.KitIMG;
        _descText.SetText(scriptableData.KitDescText);

    }

    private void UnlockAKit(SOKotakP3K scriptableData)
    {
        foreach(var kitInstance in ListOfDataInstance)
        {
            if(kitInstance.Data.KitName == scriptableData.KitName && kitInstance.State == false)
            {
                //Debug.Log(scriptableData.KitName + " Unlocked");

                kitInstance.SetState(true);
                UnlockedKitSavedData[(int)kitInstance.Data.KitName] = true;

                Save();
                break;
            }
        }

    }


    private void Save()
    {
        string Json = JsonConvert.SerializeObject(UnlockedKitSavedData);
        PlayerPrefs.SetString(SaveStateKey, Json);
        PlayerPrefs.Save();
    }

    private void Load()
    {

        if (PlayerPrefs.HasKey(SaveStateKey))
        {
            string loadString = PlayerPrefs.GetString(SaveStateKey);
            UnlockedKitSavedData = JsonConvert.DeserializeObject<bool[]>(loadString);
        }

        else
        {
            UnlockedKitSavedData = new bool[_scriptableDatas.Length];

        }

    }
}