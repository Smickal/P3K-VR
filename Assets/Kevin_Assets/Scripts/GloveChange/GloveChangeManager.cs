using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GloveChangeManager : MonoBehaviour
{
    [SerializeField] HandModelSelector _handModelSelector;
    [SerializeField] int _defaultHandIdx = 0;
    [SerializeField] int _gloveHandIdx = 1;

    public UnityAction OnGrabberChangeModel;

    public void ChangeToGlove()
    {
        _handModelSelector.ChangeHandsModel(_gloveHandIdx, false);


        //Trigger Something Through Event
        OnGrabberChangeModel?.Invoke();
    }

}
