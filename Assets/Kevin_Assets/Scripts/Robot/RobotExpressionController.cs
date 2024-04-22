using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RobotExpressionController : MonoBehaviour
{
    [SerializeField] DecalProjector _eyeProjector;


    [Header("Material")]
    [SerializeField] Material _normalExpressionMaterial;


    // Start is called before the first frame update
    void Start()
    {
        _eyeProjector.material = _normalExpressionMaterial;
    }


}
