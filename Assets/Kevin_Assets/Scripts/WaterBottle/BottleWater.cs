using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleWater : MonoBehaviour
{
    [SerializeField] float _waterSpillThreshold = 1f;


    [Header("Reference")]
    [SerializeField] SnapZone _bottleCapSnapZone;
    [SerializeField] Transform _dropWaterTransform;


    bool isBottleOpened = false;

    public void OpenedCapBottle()
    {
        isBottleOpened = true;
    }


    private void Update()
    {
        //Check if is bottled cap is opened
        if (!isBottleOpened) return;

        //Check if the bottle is upside down
        if (transform.up.y > -_waterSpillThreshold) return;

        

    }


}
