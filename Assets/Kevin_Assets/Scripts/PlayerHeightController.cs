using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using UnityEngine;

public class PlayerHeightController : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private BNGPlayerController _heightConnectOBJ;
    [SerializeField]private float minHeigth, maxHeight, addHeight = 0.001f;
    private float startHeight;
    public static Action<bool> AddPlayerHeight;
    public static Action ResetPlayerHeight;
    private void Awake() 
    {
        startHeight = _heightConnectOBJ.CharacterControllerYOffset;
        AddPlayerHeight += PlayerHeightControl;
        ResetPlayerHeight += ResetHeight;
    }
    public void TurnOffStatic()
    {
        AddPlayerHeight -= PlayerHeightControl;
        ResetPlayerHeight -= ResetHeight;
    }

    private void PlayerHeightControl(bool addPlayerHeight)
    {
        if(addPlayerHeight)
        {
            if(_heightConnectOBJ.CharacterControllerYOffset + addHeight > maxHeight)return;
            _heightConnectOBJ.ChangeCharacterControllerY(addHeight);
        }
        else
        {
            if(_heightConnectOBJ.CharacterControllerYOffset - addHeight < minHeigth)return;
            _heightConnectOBJ.ChangeCharacterControllerY(-addHeight);
        }
    }

    private void ResetHeight()
    {
        _heightConnectOBJ.ResetCharacterController(startHeight);
    }
}
