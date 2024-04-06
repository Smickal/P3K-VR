using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDasarP3K : BaseKitUI
{
    [TextArea(5,7)][SerializeField] string textData;

    [Header("Reference")]
    [SerializeField] GameObject _containerGO;
    [SerializeField] TMP_Text _dasarP3kText;
    [SerializeField] GameObject[] _containerDasar;
    [SerializeField] GameObject _containerFirstAidAim, _containerRole, _containerEmergencyAP;

    private void Start()
    {
        _dasarP3kText.SetText(textData);
    }


    public override void StartData()
    {
        _containerGO.SetActive(true);       
    }

    public override void ResetData()
    {
        _containerGO.SetActive(false);
    }
    
}
