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

    [Header("Reference")]
    [SerializeField] GameObject _masterContainerOBJ;

    public override void ResetData()
    {
        _masterContainerOBJ.SetActive(false);
    }

    public override void StartData()
    {
        _masterContainerOBJ.SetActive(true);
    }

}
