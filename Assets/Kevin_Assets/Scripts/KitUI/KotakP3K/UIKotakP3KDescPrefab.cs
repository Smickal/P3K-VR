using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKotakP3KDescPrefab : MonoBehaviour
{
    [SerializeField] Image _iconIMG;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] Button _toolsBtn;

    SOKotakP3K scriptableData;

    public void SetData(string name, Sprite iconSprite, SOKotakP3K data, UIKotakP3K UIKotakP3K)
    {
        _nameText.text = name;
        _iconIMG.sprite = iconSprite;

        scriptableData = data;

        _toolsBtn.onClick.AddListener(() =>
        {
            UIKotakP3K.ActivateDescription(scriptableData);
        });
    }
}
