using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeleportLevelUI : MonoBehaviour
{
    [SerializeField] TMP_Text _levelTitle;
    [SerializeField] Image _levelIMG;
    [SerializeField] GameObject _lockedLevel;
    [SerializeField] GameObject[] _starsBright;
    [SerializeField] Button _levelButton;

    private string levelName;

    public void SetData(LevelPlayerData levelPlayerData, int level)
    {
        levelName = levelPlayerData.levelType.ToString();

        _levelTitle.SetText("Level " + level);
        
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
        }
        else
        {
            _lockedLevel.SetActive(false);
        }

        for(int i=0;i<levelPlayerData.totalScore;i++)
        {
            _starsBright[i].SetActive(true);
        }
    }

    public void TeleportToLevel()
    {
        Debug.Log("Teleport to" + levelName);
    }
}
