using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : MonoBehaviour
{

    [SerializeField] Transform startingMeshTransform;
    [SerializeField] Transform _leftMaxTransform;
    [SerializeField] Transform _rightMaxTransform;
    BandageMovement bandageMovement;

    public BandageMovement BandageMovement { get { return bandageMovement; } }
    public Transform StartingMeshTransform { get { return startingMeshTransform; } }
    public Transform LeftMaxTransform {  get { return _leftMaxTransform; } }
    public Transform RightMaxTransform {  get { return _rightMaxTransform; } }

    public void RegisterBandageToMove(BandageMovement bandageMovement)
    {
        this.bandageMovement = bandageMovement;
    }
}
