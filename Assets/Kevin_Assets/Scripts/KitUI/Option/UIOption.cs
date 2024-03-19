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
    [SerializeField] Button _lowerHeightButton;

    [Header("Reference")]
    [SerializeField] GameObject _masterContainerOBJ;
    [Header("Player")]
    BNG.VREmulator vrEmulator;

    private void Start() 
    {
        vrEmulator = FindObjectOfType<BNG.VREmulator>();
    }

    public override void ResetData()
    {
        _masterContainerOBJ.SetActive(false);
    }

    public override void StartData()
    {
        _masterContainerOBJ.SetActive(true);
    }

    public void PlayerHeight(bool addPlayerHeight)
    {
        vrEmulator.PlayerHeightControl(addPlayerHeight);
    }
    

}
