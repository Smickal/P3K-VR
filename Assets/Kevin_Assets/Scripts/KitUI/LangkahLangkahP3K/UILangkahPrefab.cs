using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILangkahPrefab : MonoBehaviour
{
    const string UnknownKitName = "? ? ?";

    [Header("Reference")]
    [SerializeField] TMP_Text _langkahName;
    [SerializeField] Image _imgLangkahState;
    [SerializeField] Button _buttonLangkah;
    [SerializeField] GameObject _lockedImage;
    [SerializeField] Color _textColorLocked, _textColorUnlocked;
    UILangkahP3K UILangkahP3K;
    SOLangkahP3K scriptableData;
    bool state = false;

    public SOLangkahP3K Data { get { return scriptableData; } }
    public bool State { get { return state; } }
    public void SetData(string langkahName, UILangkahP3K uILangkahP3K, SOLangkahP3K data)
    {
        
        UILangkahP3K = uILangkahP3K;
        scriptableData = data;

        _buttonLangkah.onClick.AddListener(() =>
        {
            UILangkahP3K.ActivateDescription(scriptableData);
        });
        
    }

    public void SetState(bool state)
    {
        this.state = state;
        

        if (state)
        {
            _langkahName.SetText(scriptableData.ProcedureShortName);
            _langkahName.color = _textColorUnlocked;
            _imgLangkahState.sprite = scriptableData.ProcedureIcon;

            Color iconColor = _imgLangkahState.color;
            iconColor.a = 1f;
            _imgLangkahState.color = iconColor;
        }
        else
        {
            _langkahName.SetText(UnknownKitName);
            _langkahName.color = _textColorUnlocked;
            _imgLangkahState.sprite = scriptableData.ProcedureIcon;

            Color iconColor = _imgLangkahState.color;
            iconColor.a = 0.5f;
            _imgLangkahState.color = iconColor;
            
        }
        _lockedImage.SetActive(!state);
        _buttonLangkah.enabled = state;
    }

    // public void SubscribeButton(SOLangkahP3K scriptableData)
    // {

    // }
}
