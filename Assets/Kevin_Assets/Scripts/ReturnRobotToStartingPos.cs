using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnRobotToStartingPos : MonoBehaviour
{
    [SerializeField] Transform _startingPos;
    [SerializeField] float _lerpSpeed = 15f;
    [SerializeField] float _snapDistance = 0.05f;

    
    Rigidbody rigid;
    bool isMovingToSnapZone = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isMovingToSnapZone)
        {
            rigid.useGravity = false;

            transform.position = Vector3.MoveTowards(transform.position, _startingPos.transform.position, Time.deltaTime * _lerpSpeed);

            if (Vector3.Distance(transform.position, _startingPos.transform.position) <= _snapDistance)
            {
                rigid.velocity = Vector3.zero;
                isMovingToSnapZone = false;
            }
        }
    }

    public void MoveToSnapZone()
    {
        isMovingToSnapZone = true;
    }

    public void StopMoveToSnapZone()
    {
        isMovingToSnapZone = false;
    }
}
