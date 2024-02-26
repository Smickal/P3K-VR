using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILangkahDescPrefab : MonoBehaviour
{
    [SerializeField] TMP_Text _numText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] Image _tutorialIMG;


    public void SetData(string number, string desc, Sprite icon)
    {
        _numText.SetText(number);
        _descriptionText.SetText(desc);

        if(icon == null)
        {
            _tutorialIMG.gameObject.SetActive(false);
        }
        else
        {
            _tutorialIMG.sprite = icon;
        }
    }
}
