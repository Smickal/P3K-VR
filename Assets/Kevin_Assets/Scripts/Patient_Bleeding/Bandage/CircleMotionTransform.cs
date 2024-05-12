using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleMotionTransform : MonoBehaviour
{
    [SerializeField] int _maxCircleCount = 1;
    [SerializeField] float _circleRadius;
    [SerializeField] int _maxStep;
    [SerializeField] float _zAxisOffset = 0.025f;

    [Header("Reference")]
    [SerializeField] Transform _parentTransform;
    [SerializeField] GameObject _circlePlaceholderPrefabs;
    [SerializeField] Transform _customBandageMidTransform;
    [SerializeField] Transform _customBandageLeftTransform;
    [SerializeField] Transform _customBandageRightTransform;
    [Tooltip("False kalau gamau di reset hasil karya nya")]
    [SerializeField] bool wantToRecreateCircleBeforeStart = true;

    public List<Transform> CircleTransforms = new List<Transform>();
    public Transform CustomMidTransform { get { return _customBandageMidTransform; } }
    public Transform CustomLeftTransform {  get { return _customBandageLeftTransform;} }
    public Transform CustomRightTransform { get { return _customBandageRightTransform;} }

    void Start()
    {
        if(wantToRecreateCircleBeforeStart)RecreateCircle();
    }


    public void RecreateCircle()
    {
        //RESET Or DELETE all transforms, make a new one
        float currentZAxisOffset = 0f;
        float offset = _zAxisOffset * 0.001f;

        while(_parentTransform.childCount > 0)
        {
            DestroyImmediate(_parentTransform.GetChild(0).gameObject);
        }
        CircleTransforms.Clear();


        //Create GO for Objects
        for(int i = 0; i < _maxStep * _maxCircleCount; i++)
        {
            GameObject instantiatedObj =  Instantiate(_circlePlaceholderPrefabs, _parentTransform);
            CircleTransforms.Add(instantiatedObj.transform);
        }

        int circleIdx = 0;

        for(int curCircleCount = 0; curCircleCount < _maxCircleCount; curCircleCount++)
        {
            //Re-Order transform
            for(int currentStep = 0; currentStep < _maxStep; currentStep++)
            {
                float circumferenceProgress = (float)currentStep / _maxStep;

                float currentRadian = circumferenceProgress * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(currentRadian);
                float yScaled = Mathf.Sin(currentRadian);

                float x = xScaled * _circleRadius;
                float y = yScaled * _circleRadius;

                Vector3 curPos = new Vector3(x, y, currentZAxisOffset);
                currentZAxisOffset += offset;

                CircleTransforms[circleIdx].localPosition = curPos;

                //if(circleIdx > 0 && currentStep < _maxStep)
                //    CircleTransforms[circleIdx - 1].LookAt(CircleTransforms[circleIdx]);

                circleIdx++;
            }
        }
    }


    public void MoveCustomTransform(Transform midTrans, Transform leftTrans, Transform rightTrans)
    {
        _customBandageMidTransform.position = midTrans.position;
        _customBandageMidTransform.rotation = midTrans.rotation;

        _customBandageLeftTransform.position = leftTrans.position;
        _customBandageLeftTransform.rotation = midTrans.rotation;

        _customBandageRightTransform.position = rightTrans.position;
        _customBandageRightTransform.rotation = midTrans.rotation;


    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(CircleMotionTransform))]
public class CircleMotionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var cmt = (CircleMotionTransform)target;
        EditorGUI.BeginChangeCheck();
        
        base.OnInspectorGUI();


        if(EditorGUI.EndChangeCheck() || GUILayout.Button("Recreate"))
        {
            cmt.RecreateCircle();
        }
    }


}
#endif