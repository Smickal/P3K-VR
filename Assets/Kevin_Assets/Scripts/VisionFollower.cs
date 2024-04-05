using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] Transform _robotTransform;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _distanceFromRobot = 1f;
    [SerializeField] float _distanceToUIFromCamera = 1f;
    [SerializeField] float _moveSpeed = 0.025f;
    [SerializeField] Vector3 _followOffet;
    public bool isActivate = false;


    private void Update()
    {

        //UI Follows Player Positiion
        if(isActivate)
        {
            //Find Posistion
            Vector3 targetPos = _cameraTransform.position + (_cameraTransform.forward * _distanceToUIFromCamera) + _followOffet;
            //Move
            transform.position += (targetPos - transform.position) * _moveSpeed;      
            
            // transform.LookAt(_cameraTransform);
        }

        //UI Tethered to Robot
        else
        {
            Vector3 targetPos = _robotTransform.position + (_robotTransform.forward * _distanceFromRobot);
            transform.position += (targetPos - transform.position) * _moveSpeed;
        }

        //Look at
        transform.LookAt(_cameraTransform.position);

    }

    public void Activate()
    {
        isActivate = true;
    }

    public void Deactivate()
    {
        isActivate = false;
    }
}
