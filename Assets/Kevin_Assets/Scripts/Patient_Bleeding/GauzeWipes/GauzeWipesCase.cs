using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauzeWipesCase : MonoBehaviour
{
    [SerializeField] float _delayTime = 0.5f;
    [SerializeField] SnapZone _peelSnapZone;
    [SerializeField] SnapZone _gauzeSnapZone;

    bool isOpened = false;

    private void Start()
    {
        isOpened = false;
        _gauzeSnapZone.gameObject.SetActive(false);
        _peelSnapZone.gameObject.SetActive(true);
    }

    public void OpenedPeelCase()
    {
        isOpened = true;
        _peelSnapZone.gameObject.SetActive(false);
        StartCoroutine(StartCoroutineDelay());
    }

    public void OpenedGauzeWipes()
    {
        _gauzeSnapZone.gameObject.SetActive(false);
    }

    IEnumerator StartCoroutineDelay()
    {
        yield return new WaitForSeconds(_delayTime);
        _gauzeSnapZone.gameObject.SetActive(true);
        _gauzeSnapZone.GetComponent<SnapZoneControllerHelper>().SnapSelected_ConnectToHandTrackSnap();
    }
}
