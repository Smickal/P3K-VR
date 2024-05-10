using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUIManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] SceneMoveManager sceneMoveManager;
    [SerializeField] Transform _contentTransform;
    [SerializeField] GameObject _containerDoorOBJ;
    [Header("Prefabs")]
    [SerializeField] TeleportLevelUI _teleportLevelPrefab;
    private List<TeleportLevelUI> ListOfDataInstance = new List<TeleportLevelUI>();

    [Header("Debug Only")]
    public bool canShow;
    private bool hasClickTeleport;
    public bool HasClickTeleport{get{return hasClickTeleport;}set{hasClickTeleport = value;}}

    private void Start() 
    {
        int totalLevel = PlayerManager.TotalLevels();
        hasClickTeleport = false;
        for(int i = 1; i < totalLevel; i++)
        {
            // Debug.Log(i + " what" + totalLevel);
            TeleportLevelUI newUI = Instantiate(_teleportLevelPrefab, _contentTransform);
            newUI.SetData(PlayerManager.LevelDataNow(i), i, sceneMoveManager, this);

            ListOfDataInstance.Add(newUI);
        }
    }

    // private void Update() 
    // {
    //     if(canShow && !_containerDoorOBJ.activeSelf) Show();
    //     else if(!canShow && _containerDoorOBJ.activeSelf) Hide();
    // }

    public void Show()
    {
        // if(GameManager.CheckGameStateNow() != GameState.InGame) return;
        _containerDoorOBJ.SetActive(true);
    }
    public void Hide()
    {
        _containerDoorOBJ.SetActive(false);
    }
}
