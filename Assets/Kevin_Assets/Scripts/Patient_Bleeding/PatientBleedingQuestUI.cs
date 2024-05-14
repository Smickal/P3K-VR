using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientBleedingQuestUI : MonoBehaviour
{
    [SerializeField]GameObject[] _tooltipContainers;
    [SerializeField]GameObject _tooltipBriefCase;
    [SerializeField]GameObject _tooltipBriefItem;
    [SerializeField]GameObject _tooltipWithoutItem;
    [SerializeField]GameObject[] _tooltipWithoutItemContainers;
    [SerializeField]GameObject _tooltipWithItem;
    [SerializeField]GameObject[] _tooltipWithItemContainers;
    [SerializeField]GameObject _toolTipTrashcan;

    public void ActivateBriefCase()
    {
        CloseAllUI();
        _tooltipBriefCase.SetActive(true);
    }
    public void DeactivateBriefCase()
    {
        _tooltipBriefCase.SetActive(false);
    }
    public void ActivateBriefItem()
    {
        _tooltipBriefItem.SetActive(true);
    }
    public void DeactivateBriefItem()
    {
        _tooltipBriefItem.SetActive(false);
    }
    public void ActivateTrashCan()
    {
        _toolTipTrashcan.SetActive(true);
    }
    public void DeactivateTrashCan()
    {
        _toolTipTrashcan.SetActive(false);
    }
    public void ActivateToolTipWithoutItem(int containerNumber)
    {
        if(!_tooltipWithoutItem.activeSelf)
        {
            CloseAllUI();
            _tooltipWithoutItem.SetActive(true);
        }
        Close_ToolTipWithoutItemContainers();
        _tooltipWithoutItemContainers[containerNumber].SetActive(true);
    }
    public void DeactivateToolTipWithoutItem()
    {
        _tooltipWithoutItem.SetActive(false);
        Close_ToolTipWithoutItemContainers();
    }
    public void ActivateToolTipWithItem(int containerNumber)
    {
        if(!_tooltipWithItem.activeSelf)
        {
            CloseAllUI();
            _tooltipWithItem.SetActive(true);
        }
        Close_ToolTipWithItemContainers();
        _tooltipWithItemContainers[containerNumber].SetActive(true);
    }
    public void DeactivateToolTipWithItem()
    {
        _tooltipWithItem.SetActive(false);
        Close_ToolTipWithItemContainers();
    }
    public void CloseAllUI()
    {
        foreach(GameObject tooltipContainer in _tooltipContainers)
        {
            tooltipContainer.SetActive(false);
        }
    }
    public void Close_ToolTipWithoutItemContainers()
    {
        foreach(GameObject tooltipContainer in _tooltipWithoutItemContainers)
        {
            tooltipContainer.SetActive(false);
        }
    }
    public void Close_ToolTipWithItemContainers()
    {
        foreach(GameObject tooltipContainer in _tooltipWithItemContainers)
        {
            tooltipContainer.SetActive(false);
        }
    }
    public void CloseALL()
    {
        foreach(GameObject tooltipContainer in _tooltipContainers)
        {
            tooltipContainer.SetActive(false);
        }
        DeactivateBriefItem();
        DeactivateTrashCan();
    }

    
}
