using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKotakP3K : BaseKitUI
{
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


    private void Start()
    {
        _backBtn.onClick.AddListener(StartData);

        for(int i = 0; i < _scriptableDatas.Length; i++)
        {
            UIKotakP3KDescPrefab newUI = Instantiate(_descPrefab, _contentTransform);
            newUI.SetData(_scriptableDatas[i].KitName, _scriptableDatas[i].KitIMG, _scriptableDatas[i], this);
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

        _toolsNameText.SetText(scriptableData.KitName);
        _descIconIMG.sprite = scriptableData.KitIMG;
        _descText.SetText(scriptableData.KitDescText);

    }

}
