using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing.Printing;
using UnityEngine.Events;
using System;

public class KitUiManager : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] GameObject _baseUI;
    [Space(3)]
    [SerializeField] GameObject _pauseMenuContainer;
    [SerializeField] GameObject _quizContainer;
    [SerializeField] GameObject _levelHelperContainer;
    [SerializeField] GameObject _outsideGlossaryContainer;

    [Header("UI")]
    [SerializeField] GameObject[] _baseUIContainers;
    [SerializeField] BaseKitUI[] _basePauseMenuContainer;
    [SerializeField] TMP_Text _titleBase;

    [Header("Reference")]
    [SerializeField] VisionFollower _vF;


    public static event UnityAction OnActivate;
    public static event UnityAction OnDeactivate;

    public void OpenPauseMenuUI(BaseKitUI KitUI)
    {
        foreach (var kit in _basePauseMenuContainer)
        {
            kit.ResetData();
        }

        KitUI.StartData();
    }

    public void ActivateBaseUI(String titleBase)
    {
        _titleBase.text = titleBase;
        _baseUI.SetActive(true);
    }

    public void DeactivateBaseUI()
    {
        _baseUI.SetActive(false);
    }

    public void OpenPauseUI()
    {
        CloseUI();
        _pauseMenuContainer.SetActive(true);
        _vF.Activate();
    }

    public void OpenQuizUI()
    {
        CloseUI();
        _quizContainer.SetActive(true);
        _vF.Deactivate();
    }

    public void OpenLevelHelperUI()
    {
        CloseUI();
        _levelHelperContainer.SetActive(true);
        _vF.Deactivate();
    }

    public void OpenDesc_OutsideGlossary()
    {
        CloseUI();
        _outsideGlossaryContainer.SetActive(true);
    }

    public bool IsBaseUIOpen()
    {
        return _baseUI.activeSelf;
    }

    private void CloseUI()
    {
        foreach (var obj in _baseUIContainers)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
