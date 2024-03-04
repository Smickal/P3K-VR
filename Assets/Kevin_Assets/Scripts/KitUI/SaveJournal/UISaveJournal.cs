using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISaveJournal : BaseKitUI
{

    [Header("Reference")]
    [SerializeField] GameObject _masterContainer;
    [SerializeField] Button _saveButton;


    private void Start()
    {
        _saveButton.onClick.AddListener(Save);
    }

    private void Save()
    {
        Debug.Log("Save");
    }

    public override void ResetData()
    {
        _masterContainer.SetActive(false);
    }

    public override void StartData()
    {
        _masterContainer.SetActive(true);
    }
}
