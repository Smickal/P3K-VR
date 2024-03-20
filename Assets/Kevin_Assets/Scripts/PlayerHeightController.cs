using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHeightController : MonoBehaviour
{
    [SerializeField]private GameObject[] _heightConnectOBJ;
    [SerializeField]private float minHeigth, maxHeight, addHeight = 0.05f;
    public static Action<bool> AddPlayerHeight;
    public static Action ResetPlayerHeight;
    private void Awake() 
    {
        AddPlayerHeight += PlayerHeightControl;
        ResetPlayerHeight += ResetHeight;
    }

    private void PlayerHeightControl(bool addPlayerHeight)
    {
        if(addPlayerHeight)
        {
            for(int i=0;i < _heightConnectOBJ.Length;i++)
            {
                float newScale = Mathf.Clamp(_heightConnectOBJ[i].transform.localScale.x + addHeight, minHeigth, maxHeight);
                _heightConnectOBJ[i].transform.localScale = new Vector3(newScale,newScale,newScale);
            }
        }
        else
        {
            for(int i=0;i < _heightConnectOBJ.Length;i++)
            {
                float newScale = Mathf.Clamp(_heightConnectOBJ[i].transform.localScale.x - addHeight, minHeigth, maxHeight);
                _heightConnectOBJ[i].transform.localScale = new Vector3(newScale,newScale,newScale);
            }
        }
    }

    private void ResetHeight()
    {
        for(int i=0;i < _heightConnectOBJ.Length;i++)
        {
            _heightConnectOBJ[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
