using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPatientAnimationOnAState : MonoBehaviour
{
    [SerializeField]string TriggerName;
    [SerializeField]private Animator animator;
    private void Start() 
    {
        animator.SetTrigger(TriggerName);
    }
}
