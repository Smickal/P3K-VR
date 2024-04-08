using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKotakP3KDescPrefab : MonoBehaviour
{
    const string UnknownKitName = "???";

    
    [SerializeField] Image _iconIMG;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] Button _toolsBtn;
    [SerializeField] GameObject _lockedImage;
    [SerializeField] Color _textColorLocked, _textColorUnlocked;

    SOKotakP3K scriptableData;
 
    bool state = false;

    public SOKotakP3K Data { get { return scriptableData; } }
    public bool State { get { return state; } }

    public void SetData(SOKotakP3K data, UIKotakP3K UIKotakP3K)
    {
        scriptableData = data;

        _toolsBtn.onClick.AddListener(() =>
        {
            UIKotakP3K.ActivateDescription(scriptableData);
        });
    }

    public void SetState(bool state)
    {
        this.state = state;

        //If True Then show Image, if false, then make
        if (state)
        {
            _nameText.SetText(scriptableData.KitName.ToString());
            _nameText.color = _textColorUnlocked;
            _iconIMG.sprite = scriptableData.KitIMG;
        }

        else
        {
            _nameText.SetText(UnknownKitName);
            _nameText.color = _textColorLocked;
            _iconIMG.sprite = null;
        }
        _lockedImage.SetActive(!state);
        _toolsBtn.enabled = this.state;
    }
}
