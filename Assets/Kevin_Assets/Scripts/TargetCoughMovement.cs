using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCoughMovement : MonoBehaviour
{
    [SerializeField] Transform _targetMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.LookAt( _targetMovement );
    }
}
