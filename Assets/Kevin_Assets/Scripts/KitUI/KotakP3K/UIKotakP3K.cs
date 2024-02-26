using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKotakP3K : MonoBehaviour, IKitUI
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


    private void Start()
    {
        _backBtn.onClick.AddListener(StartData);


    }

    public void StartData()
    {
        _containerMasterOBJ.SetActive(true);

        _choiceContainerOBJ.SetActive(true);
        _descContainerOBJ.SetActive(false);
    }
    public void ResetData()
    {
        _containerMasterOBJ.SetActive(false);

        _choiceContainerOBJ.SetActive(true);
        _descContainerOBJ.SetActive(false);
    }

}
