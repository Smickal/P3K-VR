using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LegTargetConstraint : MonoBehaviour
{
    [SerializeField] Color32 _lineColor = Color.green;

    [SerializeField] Transform[] _targetConstraints;
    [SerializeField] Transform _middlePoint;
    //IDX LISTING
    // 0 -> Down-Left
    // 1 -> Up-Left
    // 2 -> Up-Right
    // 3 -> Down-Right



    private void OnDrawGizmos()
    {
        Gizmos.color = _lineColor;

        for(int i = 0; i < _targetConstraints.Length; i++)
        {
            if(i+1 == _targetConstraints.Length)
            {
                Gizmos.DrawLine(_targetConstraints[i].position, _targetConstraints[0].position);
            }
            else
            {
                Gizmos.DrawLine(_targetConstraints[i].position, _targetConstraints[i+1].position);
            }

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

    //public Vector2 GetEdgePositionFromConstraint(Vector3 constraintPoint)
    //{
    //    Vector2 point = new Vector2(constraintPoint.y, constraintPoint.z);

    //    //Get edge Line





    //}

    //private int
}
