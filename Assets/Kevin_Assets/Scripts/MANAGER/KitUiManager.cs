using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitUiManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] BaseKitUI[] _baseUIs;

    public void OpenUI(BaseKitUI KitUI)
    {
        foreach (var kit in _baseUIs)
        {
            kit.ResetData();
        }

        KitUI.StartData();
    }
}
