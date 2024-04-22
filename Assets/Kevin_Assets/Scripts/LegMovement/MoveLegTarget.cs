using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegTarget : MonoBehaviour
{
    [SerializeField] Transform _target;


    private void Start() 
    {
        transform.position = _target.position;
    }
    void Update()
    {
        transform.position = _target.position;
    }
}
