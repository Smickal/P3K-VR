using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using UnityEngine;

public class PlayerHeightController : MonoBehaviour, ITurnOffStatic
{
    const string SaveStateKey = "HeightSaveKey";
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
    private void Start() 
    {
        float startsHeight = PlayerPrefs.GetFloat(SaveStateKey, startHeight);
        _heightConnectOBJ.ChangeCharacterControllerY(startsHeight);
        if(PlayerManager.LastInGameMode != null)if(PlayerManager.LastInGameMode() != InGame_Mode.FirstAid)ResetHeight();
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
            PlayerPrefs.SetFloat(SaveStateKey, _heightConnectOBJ.CharacterControllerYOffset+ addHeight);
        }
        else
        {
            if(_heightConnectOBJ.CharacterControllerYOffset - addHeight < minHeigth)return;
            _heightConnectOBJ.ChangeCharacterControllerY(-addHeight);
            PlayerPrefs.SetFloat(SaveStateKey, _heightConnectOBJ.CharacterControllerYOffset-addHeight);
        }
    }

    public void ResetHeight()
    {
        _heightConnectOBJ.ResetCharacterController(startHeight);
        PlayerPrefs.SetFloat(SaveStateKey, startHeight);
    }

}
