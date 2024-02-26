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



    public void SetData(string name, Sprite iconSprite)
    {
        _nameText.text = name;
        _iconIMG.sprite = iconSprite;


    }
}
