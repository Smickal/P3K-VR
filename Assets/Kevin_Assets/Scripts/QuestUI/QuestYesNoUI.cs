using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestYesNoUI : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject _baseUI;
    [SerializeField] VisionFollower _vF;

    public void ActivateUI()
    {
        _baseUI.SetActive(true);
        _vF.Activate();
    }

    public void CloseUI()
    {
        _baseUI.SetActive(false);
        _vF.Deactivate();
    }
}
