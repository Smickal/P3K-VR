using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleMotionTransform : MonoBehaviour
{
    [SerializeField] float _circleRadius;
    [SerializeField] int _maxStep;

    [Header("Reference")]
    [SerializeField] Transform _parentTransform;
    [SerializeField] GameObject _circlePlaceholderPrefabs;

    public List<Transform> CircleTransforms = new List<Transform>();



    void Start()
    {
        RecreateCircle();
    }


    public void RecreateCircle()
    {
        //RESET Or DELETE all transforms, make a new one

        while(_parentTransform.childCount > 0)
        {
            DestroyImmediate(_parentTransform.GetChild(0).gameObject);
        }
        CircleTransforms.Clear();


        //Create GO for Objects
        for(int i = 0; i < _maxStep; i++)
        {
            GameObject instantiatedObj =  Instantiate(_circlePlaceholderPrefabs, _parentTransform);
            CircleTransforms.Add(instantiatedObj.transform);
        }

        //Re-Order transform
        for(int currentStep = 0; currentStep < _maxStep; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / _maxStep;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * _circleRadius;
            float y = yScaled * _circleRadius;

            Vector3 curPos = new Vector3(x, y, 0f);

            CircleTransforms[currentStep].localPosition = curPos;
        }



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