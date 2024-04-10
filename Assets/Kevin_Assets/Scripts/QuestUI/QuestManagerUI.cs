using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagerUI : MonoBehaviour, ITurnOffStatic
{
    const string titleChoking = "Choking First Aid";
    const string titleChokingBackblow = "Backblow";
    const string titleChokingHeimlich = "Heimlich";
    const string titleBleedingWithItem = "Bleeding With Embedded Item First Aid";
    const string _counterStartBackBlow = "Counter Backblow : 0";
    const string _counterStartHeimlich = "Counter Heimlich : 0";
    [TextArea(5,7)][SerializeField]private string _followBackBlow;
    [TextArea(5,7)][SerializeField]private string _followHeimlich;
    [Header("Base UI")]
    [SerializeField] GameObject _baseUI;
    [SerializeField] TMP_Text _titleBase;
    [SerializeField] GameObject[] _baseContainers;
    [SerializeField] GameObject _restartBtn, _quitBtn;
    [Header("Timer Countdown UI")]
    [SerializeField] Slider _timerSlider;
    private float _maxTimer;
    [Header("Choking")]
    
    [SerializeField] GameObject _chokingHelperContainer;
    [SerializeField] Image _chokingDescIMG;
    [SerializeField] TMP_Text _counterText, _followProcedureText;
    [Tooltip("0 - Backblow, 1 - Heimlich")]
    [SerializeField] Sprite[] _chokingDescSprite;
    [Header("Bleeding")]
    [SerializeField] GameObject _bleedingHelperContainer_WithItem;
    [SerializeField] GameObject _bleedingHelperContainer_WithoutItem;
    [SerializeField] Slider _timerSlider_bleeding;

    public static Action<String> ChangeHelperDesc_Choking;
    
    private void Awake() 
    {
        ActiveQuestBtn_Robot(false);
        ChangeHelperDesc_Choking += ChangeHelper_ChokingDesc;
    }
    public void TurnOffStatic()
    {
        ChangeHelperDesc_Choking -= ChangeHelper_ChokingDesc;
    }

    public void ActivateBaseUI()
    {
        _baseUI.SetActive(true);
    }

    public void DeactivateBaseUI()
    {
        _baseUI.SetActive(false);
    }

    public void OpenHelper_Choking()
    {
        ActivateBaseUI();
        CloseUI();
        _chokingHelperContainer.SetActive(true);

    }
    public void CloseHelper_Choking()
    {
        DeactivateBaseUI();
        _chokingHelperContainer.SetActive(false);
    }
    public void ChangeHelper_ChokingDesc(string choke_P3KType)
    {
        _titleBase.text = titleChoking + "-" + choke_P3KType;
        Debug.Log("ini maksudnya " + _chokingDescIMG.gameObject + "ilang???");
        if(choke_P3KType == titleChokingBackblow)
        {
            _chokingDescIMG.sprite = _chokingDescSprite[0];
            _counterText.text = _counterStartBackBlow;
            _followProcedureText.text = _followBackBlow;
        }
        else if(choke_P3KType == titleChokingHeimlich)
        {
            _chokingDescIMG.sprite = _chokingDescSprite[1];
            _counterText.text = _counterStartHeimlich;
            _followProcedureText.text = _followHeimlich;
        }
    }

    public void OpenHelper_Bleeding_All()
    {
        ActivateBaseUI();
        CloseUI();
        _titleBase.text = titleBleedingWithItem;
        _bleedingHelperContainer_WithItem.SetActive(true);
        ShowTimer();
        _bleedingHelperContainer_WithoutItem.SetActive(true);

    }
    public void OpenHelper_Bleeding_WithItem()
    {
        ActivateBaseUI();
        CloseUI();
        _titleBase.text = titleBleedingWithItem;
        ShowTimer();
        _bleedingHelperContainer_WithItem.SetActive(true);
    }
    public void OpenHelper_Bleeding_WithoutItem()
    {
        _bleedingHelperContainer_WithoutItem.SetActive(true);
    }
    public void CloseHelper_Bleeding_WithItem()
    {
        DeactivateBaseUI();
        _bleedingHelperContainer_WithItem.SetActive(false);
    }
    public void CloseHelper_Bleeding_WithoutItem()
    {
        _bleedingHelperContainer_WithoutItem.SetActive(false);
    }

    public void ShowTimer() {_timerSlider.gameObject.SetActive(true);}
    public void HideTimer() {_timerSlider.gameObject.SetActive(false);}
    public void SetTimerSlider(float timerMax)
    {
        _timerSlider.value = 1;
        if(GameManager.CheckLevelTypeNow() == LevelP3KType.Bleeding)_timerSlider_bleeding.value = 1;
        _maxTimer = timerMax;
    }
    public void ChangeTimerSlider(float curTime)
    {
        _timerSlider.value = curTime/_maxTimer;
        if(GameManager.CheckLevelTypeNow() == LevelP3KType.Bleeding)_timerSlider_bleeding.value = curTime/_maxTimer;
    }
    public void ActiveQuestBtn_Robot(bool change)
    {
        _restartBtn.SetActive(change);
        _quitBtn.SetActive(change);
    }
    private void CloseUI()
    {
        HideTimer();
        foreach (var obj in _baseContainers)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
