using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GloveChangeManager : MonoBehaviour
{
    [SerializeField] HandModelSelector _handModelSelector;
    [SerializeField] ChangeHandMaterial changeHandMaterial;
    [SerializeField] int _defaultHandIdx = 0;
    [SerializeField] int _gloveHandIdx = 1;
    private bool isDoneChanging;
    public bool IsDoneChanging{get{return isDoneChanging;}}

    public UnityAction OnGrabberChangeModel;

    public void ChangeToGlove()
    {
        // if(GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
        //     GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid || 
        //     BleedingWithoutEmbeddedItem.StateFirstAidNow() != BleedingWithoutEmbeddedItem_State.WearGloves)return;
        _handModelSelector.ChangeHandsModel(_gloveHandIdx, false);
        changeHandMaterial.ChangeHandsMaterial(HandsMaterial.Gloves);
        isDoneChanging = true;
        //Trigger Something Through Event
        OnGrabberChangeModel?.Invoke();
    }

}
