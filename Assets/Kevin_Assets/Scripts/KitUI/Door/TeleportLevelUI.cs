using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TeleportLevelUI : MonoBehaviour
{
    [SerializeField] TMP_Text _levelTitle;
    [SerializeField] Image _levelIMG;
    [SerializeField] GameObject _lockedLevel, _casualtyContainer;
    [SerializeField] Sprite[] _scoreEmoticon;
    [SerializeField] Image _scoreIMG;
    [SerializeField] Button _levelButton;
    [SerializeField] SceneMoveManager sceneMoveManager;
    [SerializeField] DoorUIManager doorUIManager;

    private string levelName;

    public void SetData(LevelPlayerData levelPlayerData, int level, SceneMoveManager sceneMoveManagers, DoorUIManager doorUIManagers)
    {
        sceneMoveManager = sceneMoveManagers;
        doorUIManager = doorUIManagers;

        _levelTitle.SetText(levelPlayerData.levelName);
        levelName = levelPlayerData.levelName;
        _levelButton.onClick.AddListener(TeleportToLevel);

        if(levelPlayerData.levelSprite == null)
        {
            _levelIMG.gameObject.SetActive(false);
        }
        else
        {
            _levelIMG.sprite = levelPlayerData.levelSprite;
        }

        if(!levelPlayerData.unlocked)
        {
            _lockedLevel.SetActive(true);
            _levelTitle.SetText("? ? ?");
            _levelIMG.gameObject.SetActive(false);
            _casualtyContainer.SetActive(false);
        }
        else
        {
            _lockedLevel.SetActive(false);
        }

        _scoreIMG.sprite = _scoreEmoticon[(int)levelPlayerData.score];
        
    }

    public void TeleportToLevel()
    {
        Debug.Log("Teleport to" + levelName);
        if(doorUIManager.HasClickTeleport)return;
        doorUIManager.HasClickTeleport = true;
        if(sceneMoveManager)sceneMoveManager.GoToScene(levelName);
    }
}
