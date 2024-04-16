using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISaveJournal : BaseKitUI
{

    [Header("Reference")]
    [SerializeField] GameObject _masterContainer;

    public override void ResetData()
    {
        _masterContainer.SetActive(false);
    }

    public override void StartData()
    {
        _masterContainer.SetActive(true);
    }
}
