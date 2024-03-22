using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class Briefcase : MonoBehaviour
{
    // Start is called before the first frame update
    int OpenBriefHash = Animator.StringToHash("OpenCase");
    int CloseBriefHash = Animator.StringToHash("CloseCase");

    [Header("Reference")]
    [SerializeField] Animator _briefCaseAnim;
    [SerializeField] Button _briefCaseBtn_Controller;
    [SerializeField] InteractableUnityEventWrapper _briefCaseBtn_HandTrack;
    //[SerializeField] BoxCollider _boxCollider;

    bool isOpen = false;
    int i= 0;

    private void Start()
    {
        _briefCaseBtn_Controller.onButtonDown.AddListener(TriggerOpenCloseAnim);
        _briefCaseBtn_HandTrack.WhenSelect.AddListener(TriggerOpenCloseAnim);

    }

    public void TriggerOpenCloseAnim()
    {
        i++;
        Debug.Log("test" + i);
        
        if (isOpen == false)
        {
            _briefCaseAnim.SetTrigger(OpenBriefHash);
            isOpen = true;
        }
        else
        {
            _briefCaseAnim.SetTrigger(CloseBriefHash);
            isOpen = false;
        }
    }

}
