using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : BaseKitUI
{

    [Header("Slider")]
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    [Header("Button")]
    [SerializeField] Button _higherHeightButton;
    [SerializeField] Button _resetHeightButton;
    [SerializeField] Button _lowerHeightButton;
    [SerializeField] Button _resetPlayerSaveButton;
    [SerializeField] Button _resetPlayerSaveButton_Yes;
    [SerializeField] Button _resetPlayerSaveButton_No;

    [Header("Reference")]
    [SerializeField] GameObject _masterContainerOBJ;
    [SerializeField] GameObject _resetYesNoContainerOBJ;

    private void Start() 
    {

        _higherHeightButton.onClick.AddListener
        (
            ()=>
            {
                PlayerHeight(true);
            }
        );
        _lowerHeightButton.onClick.AddListener
        (
            ()=>
            {
                PlayerHeight(false);
            }
        );
        _resetHeightButton.onClick.AddListener(ResetPlayerHeight);

        _resetPlayerSaveButton.onClick.AddListener(ShowResetYesNo);
        _resetPlayerSaveButton_Yes.onClick.AddListener(ResetPlayerSaveData);
        _resetPlayerSaveButton_No.onClick.AddListener(HideResetYesNo);
    }

    public override void ResetData()
    {
        _masterContainerOBJ.SetActive(false);
        if(_resetYesNoContainerOBJ.activeSelf)HideResetYesNo();
    }

    public override void StartData()
    {
        if(_resetYesNoContainerOBJ.activeSelf)HideResetYesNo();
        _masterContainerOBJ.SetActive(true);
    }

    public void PlayerHeight(bool addPlayerHeight)
    {
        PlayerHeightController.AddPlayerHeight(addPlayerHeight);
    }
    public void ResetPlayerHeight()
    {
        PlayerHeightController.ResetPlayerHeight();
    }
    public void ResetPlayerSaveData()
    {
        PlayerManager.ResetPlayerSave();
        if(_resetYesNoContainerOBJ.activeSelf)HideResetYesNo();
        GameManager.PauseGame();
        //fade out
        //scene ke home;
        //scenemanagement
    }
    public void ShowResetYesNo()
    {
        _resetYesNoContainerOBJ.SetActive(true);
    }
    public void HideResetYesNo()
    {
        _resetYesNoContainerOBJ.SetActive(false);
    }
    

}
