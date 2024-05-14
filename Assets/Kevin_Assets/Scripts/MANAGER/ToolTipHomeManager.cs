using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipHomeManager : MonoBehaviour
{
    [SerializeField]GameObject[] _tooltipContainers;
    [SerializeField]GameObject[] _toolTipOpenWhenFinishQuest;
    [SerializeField]GameObject[] _toolTipWhenBriefCaseOpen;
    [SerializeField]GameObject _toolTipWhenBriefCaseClose;

    public void CloseAll()
    {
        foreach(GameObject container in _tooltipContainers)
        {
            container.SetActive(false);
        }
    }
    public void Activate()
    {
        foreach(GameObject container in _toolTipOpenWhenFinishQuest)
        {
            container.SetActive(true);
        }
    }
    public void ActivateWhenOpen()
    {
        _toolTipWhenBriefCaseClose.SetActive(false);
        foreach(GameObject container in _toolTipWhenBriefCaseOpen)
        {
            container.SetActive(true);
        }
    }
    public void ActivateWhenClose()
    {
        _toolTipWhenBriefCaseClose.SetActive(true);
        foreach(GameObject container in _toolTipWhenBriefCaseOpen)
        {
            container.SetActive(false);
        }
    }
}
