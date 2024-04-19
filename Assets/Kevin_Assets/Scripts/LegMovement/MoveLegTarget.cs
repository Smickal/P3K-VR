using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegTarget : MonoBehaviour
{
    [SerializeField] Transform _target;


    // Update is called once per frame
    void Update()
    {
        transform.position = _target.position;
    }
}
