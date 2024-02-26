using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILangkahPrefab : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] TMP_Text _langkahName;
    [SerializeField] Image _imgLangkahState;
    [SerializeField] Button _buttonLangkah;

    UILangkahP3K UILangkahP3K;
    SOLangkahP3K scriptableData;
    public void SetData(string langkahName, UILangkahP3K uILangkahP3K, SOLangkahP3K data)
    {
        _langkahName.SetText(langkahName);
        UILangkahP3K = uILangkahP3K;
        scriptableData = data;

        _buttonLangkah.onClick.AddListener(() =>
        {
            UILangkahP3K.ActivateDescription(scriptableData);
        });
        
    }

    public void SetState(Sprite sprite, bool state)
    {
        _imgLangkahState.sprite = sprite;

        if (state)
        {
            _buttonLangkah.enabled = true;
        }
        else
        {
            _buttonLangkah.enabled = false;
        }

    }

    public void SubscribeButton(SOLangkahP3K scriptableData)
    {

    }
}
