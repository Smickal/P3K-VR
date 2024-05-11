using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZoneHandInteractablePositionChange : MonoBehaviour
{
    [SerializeField] GameObject[] HandInteractables;
    [SerializeField] Vector3[] HandInteractablesPositionAtStart;
    [SerializeField] Vector3[] HandInteractablesPositionChange;
    private bool wasChange;

    // private void Awake()
    // {
    //     // HandInteractablesPositionAtStart = new Vector3[HandInteractables.Length];
    //     // for(int i = 0; i< HandInteractables.Length; i++)
    //     // {
    //     //     HandInteractablesPositionAtStart[i] = HandInteractables[i].transform.position;
    //     // }
    // }
    public void ChangeWhenScale()
    {
        wasChange = true;
        for(int i = 0; i< HandInteractables.Length; i++)
        {
            HandInteractables[i].transform.localPosition = HandInteractablesPositionChange[i];
        }
        
    }
    public void ChangeBack()
    {
        if(!wasChange)return;
        wasChange = false;
        for(int i = 0; i< HandInteractables.Length; i++)
        {
            HandInteractables[i].transform.localPosition = HandInteractablesPositionAtStart[i];
            // Debug.Log(HandInteractables[i].transform.localPosition + "start" + HandInteractablesPositionAtStart[i]);
        }
    }

}
