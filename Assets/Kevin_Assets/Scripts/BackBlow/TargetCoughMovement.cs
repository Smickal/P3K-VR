using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCoughMovement : MonoBehaviour
{
    [SerializeField] Transform _targetMovement;
    [SerializeField] Vector3 _offset = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
       transform.LookAt(_targetMovement.position + _offset);
    }
}
