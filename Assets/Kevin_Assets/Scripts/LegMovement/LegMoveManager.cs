using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class LegMoveManager : MonoBehaviour
{
    [SerializeField] float _finishHeight = 0.15f;
    [SerializeField] float _maxDisplacementVelocity = 0.15f;

    [Space(4)]
    [SerializeField] SnapZone _brickSnapZone;
    [SerializeField] SnapZone _legSnapZone;
    [SerializeField] Transform _legGrabbable;
    [SerializeField] Transform _endFeetPosition;
    [SerializeField] Transform _targetTransform;

    [Space(4)]
    [Header("OnMovementDone-UnityEvent")]
    [Space(2)]
    public UnityEvent OnMovementDone = new UnityEvent();

    bool isGrabbingFoot = false;
    bool isDonePuttingBrick = false;

    float startGrabHeight;

    Vector3 startingPos;
    Rigidbody legGrabbableRB;
    Grabber currentGrabber;

    private void Awake()
    {
        startingPos = _targetTransform.position;
    }

    private void Start()
    {
        _brickSnapZone.gameObject.SetActive(false);
        legGrabbableRB = _legGrabbable.gameObject.GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        //Debug.Log(legGrabbableRB.velocity.magnitude);

        if (!isGrabbingFoot) return;
        if (legGrabbableRB.velocity.magnitude >= _maxDisplacementVelocity)
        {
            currentGrabber.TryRelease();
            OnReleaseFoot();
            return;
        }

        if (_legGrabbable.position.y - startGrabHeight >= _finishHeight)
        {
            _brickSnapZone.gameObject.SetActive(true);
        }

        else
        {
            _brickSnapZone.gameObject.SetActive(false);
        }

    }

    public void OnGrabFoot()
    {
        startGrabHeight = _legGrabbable.transform.position.y;
        isGrabbingFoot = true;
    }

    public void OnReleaseFoot()
    {
        if(!isDonePuttingBrick)
        {
            _brickSnapZone.gameObject.SetActive(false);
            currentGrabber = null;
        }

        isGrabbingFoot = false;
    }

    public void OnSnapBrick()
    {
        _brickSnapZone.gameObject.SetActive(true);
        //StopMoveLeg
        //DisableLegSnapZone
        //DisableBrickSnapZone

        isGrabbingFoot = false;
        _brickSnapZone.CanRemoveItem = false;

        _legSnapZone.transform.position = _endFeetPosition.position;
        _legSnapZone.CanRemoveItem = false;

        isDonePuttingBrick = true;

        OnMovementDone.Invoke();

    }

    public void RegisterGrab(Grabber grabber)
    {
       currentGrabber = grabber;
    }

}
