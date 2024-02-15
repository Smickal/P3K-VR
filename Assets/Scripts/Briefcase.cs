using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefcase : MonoBehaviour
{
    // Start is called before the first frame update
    int OpenBriefHash = Animator.StringToHash("OpenCase");
    int CloseBriefHash = Animator.StringToHash("CloseCase");

    [Header("Reference")]
    [SerializeField] Animator _briefCaseAnim;
    [SerializeField] Button _briefCaseBtn;

    bool isOpen = false;

    private void Start()
    {
        _briefCaseBtn.onButtonDown.AddListener(TriggerOpenCloseAnim);

    }

    public void TriggerOpenCloseAnim()
    {

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
