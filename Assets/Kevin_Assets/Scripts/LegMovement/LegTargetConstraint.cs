using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LegTargetConstraint : MonoBehaviour
{
    [SerializeField] Color32 _lineColor = Color.green;

    [SerializeField] Transform[] _targetConstraints;
    [SerializeField] Transform _middlePoint;

    [SerializeField] Transform _edgeTransform;
    [SerializeField] Transform _outsideTrans;

    [SerializeField] bool isHorizontal = false;
    //IDX LISTING
    // 0 -> Down-Left
    // 1 -> Up-Left
    // 2 -> Up-Right
    // 3 -> Down-Right


    private void Update()
    {
        //Debug.Log("is it inside ? = " + IsTargetWithinConstraint(_outsideTrans.localPosition));

        if(isHorizontal)
        {
            Vector2 pos = GetLocalHorizontalEdge(_outsideTrans.localPosition);    
            _edgeTransform.localPosition = new Vector3(pos.x, _edgeTransform.localPosition.y, pos.y);

        }
        else
        {
            Vector2 pos = GetLocalVerticalEdge(_outsideTrans.localPosition);    
            _edgeTransform.localPosition = new Vector3(0f, pos.x, pos.y);
        }
    }




    public bool IsTargetWithinConstraint(Vector3 constraintPoint)
    {
        //Convert constraints from Vector3 to 2
        Vector2[] constraintPos = new Vector2[_targetConstraints.Length];

        for(int i = 0; i < _targetConstraints.Length; i++)
        {
            constraintPos[i] = new Vector2(_targetConstraints[i].localPosition.y, _targetConstraints[i].localPosition.z);
        }

        int count = 0;
        Vector2 point = new Vector2(constraintPoint.y, constraintPoint.z);

        // Iterate through each edge of the quadrilateral
        for (int i = 0, j = _targetConstraints.Length - 1; i < _targetConstraints.Length; j = i++)
        {
            // Check if the point is to the left of the edge
            if ((constraintPos[i].y > point.y) != (constraintPos[j].y > point.y) &&
                (point.x < (constraintPos[j].x - constraintPos[i].x) * (point.y - constraintPos[i].y) / (constraintPos[j].y - constraintPos[i].y) + constraintPos[i].x))
            {
                count++;
            }
        }


        return count % 2 == 1;
    }

    public Vector2 GetEdgePositionFromConstraint(Vector3 constraintPoint)
    {
        Vector2 point = new Vector2(constraintPoint.y, constraintPoint.z);

        //Get edge Line IDX
        int edgePos = GetEdgePositition(constraintPoint);

        Vector2 startEdgePoint = Vector2.zero;
        Vector2 endEdgePoint = Vector2.zero;

        Debug.Log(edgePos);

        //LeftEdge
        if(edgePos == 0)
        {
            startEdgePoint = _targetConstraints[0].position;
            endEdgePoint = _targetConstraints[1].position;
        }

        //TopEdge
        else if(edgePos == 1)
        {
            startEdgePoint = _targetConstraints[1].position;
            endEdgePoint = _targetConstraints[2].position;
        }

        //RightEdge
        else if(edgePos == 2)
        {
            startEdgePoint = _targetConstraints[2].position;
            endEdgePoint = _targetConstraints[3].position;
        }

        //BotEdge
        else if(edgePos == 3)
        {
            startEdgePoint = _targetConstraints[0].position;
            endEdgePoint = _targetConstraints[3].position;
        }

        else
        {
            return Vector2.zero;
        }

        //Find Middle intersecPoint

        //--> Formula : y = mx + b
        //->Find m
        //->Find b

        float y1 = 1f;
        float m1 = (endEdgePoint.y - startEdgePoint.y) / (endEdgePoint.x - startEdgePoint.x);
        float b1 = (endEdgePoint.y) - (endEdgePoint.x * m1);

        float y2 = 1f;
        float m2 = (point.y - _middlePoint.localPosition.y) / (point.x - _middlePoint.localPosition.x);
        float b2 = (point.y) - (point.x * m2);

        //float intersecX = ((y1 * - b2) - (y2 * b1)) / ((-m1 * y2) - (-m2 * y1));
        //float intersecY = ((-m1 * y2) - (-m2 * y1)) / ((-m1 * y2) - (-m2 * y1)); 

        float intersecX = (b2 - b1) / (m1 - m2);
        float intersecY = (intersecX * m1) + b1;

        return new Vector2(intersecX, intersecY);

    }

    private int GetEdgePositition(Vector2 curPos)
    {
        //Idx = 0 --> left
        //Idx = 1 --> Top
        //Idx = 2 --> Right
        //Idx = 3 --> Bot

        int countX = 0;
        int countY = 0;

        foreach(Transform transform in _targetConstraints)
        {
            if(curPos.x > transform.position.x)
            {
                countX++;
            }
        }

        // countX = 0 --> LeftSide
        // countX = 2 --> MiddleSide
        // countX = 4 --> RightSide

        foreach(Transform transform in _targetConstraints)
        {
            if(curPos.y > transform.position.y)
            {
                countY++;
            }
        }

        //countY = 0 --> Bottom
        //countY = 2 --> middle
        //countY = 4 --> Top

        //Debug.Log($"CountX = {countX}, CountY = {countY}");


        if (countX == 0)
        {
            return 0;
        }

        else if (countX == 2)
        {
            if (countY == 0)
            {
                return 3;
            }

            else if (countY == 2)
            {
                // Inside The Constraint
            }

            else if (countY == 4)
            {
                return 1;
            }
        }

        else if (countX == 4)
        {
            return 2;
        }

        return -1;

    }

    public Vector2 GetLocalVerticalEdge(Vector3 point)
    {
        Vector2 curPos = new Vector2(point.y, point.z);

        //leftSide
        if (curPos.x <= _targetConstraints[0].localPosition.y)
        {
            curPos = new Vector2(_targetConstraints[0].localPosition.y, curPos.y);
        }

        //RightSide
        else if (curPos.x >= _targetConstraints[2].localPosition.y)
        {
            curPos = new Vector2(_targetConstraints[2].localPosition.y, curPos.y);
        }

        //leftSide
        if (curPos.y <= _targetConstraints[0].localPosition.z)
        {
            curPos = new Vector2(curPos.x, _targetConstraints[0].localPosition.z);
        }

        //RightSide
        else if (curPos.y >= _targetConstraints[1].localPosition.z)
        {
            curPos = new Vector2(curPos.x, _targetConstraints[1].localPosition.z);
        }

        return curPos;
    }

    public Vector2 GetLocalHorizontalEdge(Vector3 point)
    {
        Vector2 curPos = new Vector2(point.x, point.z);
        //Debug.Log(curPos + ",  " + _targetConstraints[1].localPosition);

        //leftSide
        if (curPos.x <= _targetConstraints[1].localPosition.x)
        {
            curPos = new Vector2(_targetConstraints[1].localPosition.x, curPos.y);    
        }

        //RightSide
        else if (curPos.x >= _targetConstraints[0].localPosition.x)
        {
            curPos = new Vector2(_targetConstraints[0].localPosition.x, curPos.y);
        }

        //bot
        if (curPos.y <= _targetConstraints[1].localPosition.z)
        {
            curPos = new Vector2(curPos.x, _targetConstraints[1].localPosition.z);
        }

        //top
        else if (curPos.y >= _targetConstraints[3].localPosition.z)
        {
            curPos = new Vector2(curPos.x, _targetConstraints[3].localPosition.z);
        }

            return curPos;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = _lineColor;

        for (int i = 0; i < _targetConstraints.Length; i++)
        {
            if (i + 1 == _targetConstraints.Length)
            {
                Gizmos.DrawLine(_targetConstraints[i].position, _targetConstraints[0].position);
            }
            else
            {
                Gizmos.DrawLine(_targetConstraints[i].position, _targetConstraints[i + 1].position);
            }

        }
    }
}
