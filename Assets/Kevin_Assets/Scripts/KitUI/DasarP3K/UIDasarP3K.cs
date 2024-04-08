using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDasarP3K : BaseKitUI
{

    [Header("Reference")]
    [SerializeField] GameObject _containerGO;
    [SerializeField] TMP_Text _dasarP3kText;
    [SerializeField] GameObject _containerStart, _containerTambahan;
    [SerializeField] GameObject[] _containerTambahanList;
    [SerializeField] GameObject _containerFirstAidAim, _containerRole, _containerEmergencyAP, _containerAPPrimer, _containerAPSec;

    [Header("Button")]
    [SerializeField] Button _backBtn;


    public override void StartData()
    {
        CloseUI();
        _containerStart.SetActive(true);
        _containerGO.SetActive(true);       
    }

    public override void ResetData()
    {
        _containerGO.SetActive(false);
    }

    public void ActivateFirstAidAim()
    {
        CloseUI();
        _containerFirstAidAim.SetActive(true);
        _containerTambahan.SetActive(true);
        _backBtn.onClick.AddListener(BackToStart);
    }
    public void ActivateFirstAidRole()
    {
        CloseUI();
        _containerRole.SetActive(true);
        _containerTambahan.SetActive(true);
        _backBtn.onClick.AddListener(BackToStart);
    }
    public void ActivateFirstAidEmergencyAP()
    {
        CloseUI();
        _containerEmergencyAP.SetActive(true);
        _containerTambahan.SetActive(true);
        _backBtn.onClick.AddListener(BackToStart);
    }
    public void BackToStart()
    {
        CloseUI();
        _containerStart.SetActive(true);
    }
    public void ActivateFirstAidEmergencyAP_Primer()
    {
        CloseUI();
        _containerAPPrimer.SetActive(true);
        _containerTambahan.SetActive(true);
        _backBtn.onClick.AddListener(ActivateFirstAidEmergencyAP);
    }
    public void ActivateFirstAidEmergencyAP_Sec()
    {
        CloseUI();
        _containerAPSec.SetActive(true);
        _containerTambahan.SetActive(true);
        _backBtn.onClick.AddListener(ActivateFirstAidEmergencyAP);
    }

    private void CloseUI()
    {
        _backBtn.onClick.RemoveAllListeners();
        foreach (var obj in _containerTambahanList)
        {
            obj.gameObject.SetActive(false);
        }
        _containerStart.SetActive(false);
        _containerTambahan.SetActive(false);
    }
}
