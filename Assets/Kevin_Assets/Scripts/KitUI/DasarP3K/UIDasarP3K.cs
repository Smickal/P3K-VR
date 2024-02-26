using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDasarP3K : MonoBehaviour, IKitUI
{
    [TextArea(5,7)][SerializeField] string textData;

    [Header("Reference")]
    [SerializeField] GameObject _containerGO;
    [SerializeField] TMP_Text _dasarP3kText;

    private void Start()
    {
        _dasarP3kText.SetText(textData);
    }


    public void StartData()
    {
        _containerGO.SetActive(true);       
    }

    public void ResetData()
    {
        _containerGO.SetActive(false);
    }

}
