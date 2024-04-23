using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    int IdleHashAnim = Animator.StringToHash("Idle");
    int ForwardHashAnim = Animator.StringToHash("Forward");
    int BackwardHashAnim = Animator.StringToHash("Backward");
    int FrozeHashAnim = Animator.StringToHash("Froze");

    [SerializeField] Animator _robotAnimator;

    
    public void TriggerIdleAnim()
    {
        _robotAnimator.SetBool(IdleHashAnim, true);
        _robotAnimator.SetBool(FrozeHashAnim, false);
        _robotAnimator.SetBool(ForwardHashAnim, false);
        _robotAnimator.SetBool(BackwardHashAnim, false);
    }

    public void TriggerForwardAnim()
    {
        _robotAnimator.SetBool(FrozeHashAnim, false);
        _robotAnimator.SetBool(ForwardHashAnim, true);
        _robotAnimator.SetBool(BackwardHashAnim, false);
    }

    public void TriggerBackwardAnim()
    {
        _robotAnimator.SetBool(FrozeHashAnim, false);
        _robotAnimator.SetBool(BackwardHashAnim, true);
        _robotAnimator.SetBool(ForwardHashAnim, false);
    }

    public void TriggerFrozeAnim()
    {
        _robotAnimator.SetBool(FrozeHashAnim, true);
        _robotAnimator.SetBool(IdleHashAnim, false);
    }

}
