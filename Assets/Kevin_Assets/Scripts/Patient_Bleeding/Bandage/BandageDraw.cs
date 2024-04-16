using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BandageDraw : MonoBehaviour
{
    [SerializeField] bool _isLooping = true;

    [Header("Quads")]
    [SerializeField] private Color _color;
    [SerializeField] Material _material;
    [SerializeField] float _quadWidth = 2;

    [Header("Reference")]
    [SerializeField] CircleMotionTransform _circleMotion;
    [SerializeField] Transform _CircleMeshParent;
    [SerializeField] Transform _circleMeshCustomParent;

    List<GameObject> circleMeshesOBJ = new List<GameObject>();
    GameObject customPositionMeshOBJ;
    Bandage currentBandage;

    public void DeleteAllMeshes(Transform parentTransform)
    {
        while (parentTransform.childCount > 0)
        {
            DestroyImmediate(parentTransform.GetChild(0).gameObject);
        }
        circleMeshesOBJ.Clear();
    }

    public void DeleteAllMeshes()
    {
        while (_CircleMeshParent.childCount > 0)
        {
            DestroyImmediate(_CircleMeshParent.GetChild(0).gameObject);
        }
        circleMeshesOBJ.Clear();
    }

    public void DeleteCustomPosMesh()
    {
        if (customPositionMeshOBJ != null)
        {
            Destroy(customPositionMeshOBJ);
        }
    }

    public void CreateMeshesByIndex(int startIndex, int endIndex)
    {
        DeleteAllMeshes(_CircleMeshParent);
        //create child
        for (int i = startIndex; i <= endIndex; i++)
        {
            GameObject tempObj = new GameObject($"CircleMesh{i}");
            tempObj.transform.parent = _CircleMeshParent;
            tempObj.transform.localPosition = Vector3.zero;
            _CircleMeshParent.transform.localRotation = Quaternion.identity;

            if (i == _circleMotion.CircleTransforms.Count - 1)
            {
                if (_isLooping)
                {
                    CreateLocalCircleMesh(tempObj, _circleMotion.CircleTransforms[i], _circleMotion.CircleTransforms[0]);
                }
                else
                {
                    break;
                }
            }
            else
            {
                CreateLocalCircleMesh(tempObj, _circleMotion.CircleTransforms[i], _circleMotion.CircleTransforms[i + 1]);

            }


            circleMeshesOBJ.Add(tempObj);
        }
    }

    public void CreateCustomMeshByPositions(Transform startingTransform)
    {
        DeleteCustomPosMesh();

        GameObject tempObj = new GameObject($"CircleMesh_PositionBased");
        tempObj.transform.parent = _circleMeshCustomParent;
        tempObj.transform.localPosition = Vector3.zero;
        //_circleMeshCustomParent.transform.localRotation = Quaternion.identity;

        customPositionMeshOBJ = tempObj;

        //CreateCustomPositionMesh(tempObj, _circleMeshCustomParent, endingTransform);



        CreateCustomPositionMesh(tempObj, startingTransform);
    }



    public void CreateAllMeshes()
    {
        DeleteAllMeshes(_CircleMeshParent);


        //create child
        for(int i = 0; i < _circleMotion.CircleTransforms.Count; i++)
        {
            GameObject tempObj = new GameObject($"CircleMesh{i}");
            tempObj.transform.parent = _CircleMeshParent;
            tempObj.transform.localPosition = Vector3.zero;
            _CircleMeshParent.transform.localRotation = Quaternion.identity;

            if(i == _circleMotion.CircleTransforms.Count - 1)
            {
                if(_isLooping)
                {
                    CreateLocalCircleMesh(tempObj, _circleMotion.CircleTransforms[i], _circleMotion.CircleTransforms[0]);
                }
                else
                {
                    break;
                }
            }
            else
            {
                CreateLocalCircleMesh(tempObj, _circleMotion.CircleTransforms[i], _circleMotion.CircleTransforms[i + 1]);

            }


            circleMeshesOBJ.Add(tempObj);
        }
    }


    private void CreateLocalCircleMesh(GameObject obj, Transform startTransform, Transform endTransform)
    {
        Mesh mesh = new Mesh();
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();

        mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        int[] triangleTemplate = new int[6] { 0, 1, 2, 2, 1, 3 };


        //Top Right Vertice location
        vertices[0] = new Vector3(startTransform.localPosition.x, startTransform.localPosition.y, startTransform.localPosition.z + (-_quadWidth * 0.5f));

        //top left Vertice location
        vertices[1] = new Vector3(endTransform.localPosition.x, endTransform.localPosition.y, endTransform.localPosition.z + (-_quadWidth * 0.5f));
        //bot right Vertice location
        vertices[2] = new Vector3(startTransform.localPosition.x, startTransform.localPosition.y, startTransform.localPosition.z + (_quadWidth * 0.5f ));

        //Bot left Vertice location
        vertices[3] = new Vector3(endTransform.localPosition.x, endTransform.localPosition.y, endTransform.localPosition.z + (_quadWidth * 0.5f));


        mesh.vertices = vertices;
        mesh.triangles = triangleTemplate;
        filter.mesh = mesh;
        renderer.material = _material;

        obj.transform.localEulerAngles = Vector3.zero;
    }

    private void CreateCustomPositionMesh(GameObject obj, Transform startTransform)
    {
        Mesh mesh = new Mesh();
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();

        mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        int[] triangleTemplate = new int[6] { 0, 1, 2, 2, 1, 3 };


        //Top Right Vertice location
        vertices[0] = new Vector3(startTransform.localPosition.x, startTransform.localPosition.y, startTransform.localPosition.z + (-_quadWidth * 0.5f));

        //top left Vertice location
        vertices[1] = new Vector3(_circleMotion.CustomLeftTransform.localPosition.x, _circleMotion.CustomLeftTransform.localPosition.y, _circleMotion.CustomLeftTransform.localPosition.z);
        //bot right Vertice location
        vertices[2] = new Vector3(startTransform.localPosition.x, startTransform.localPosition.y, startTransform.localPosition.z + (_quadWidth * 0.5f));


        //Bot left Vertice location
        vertices[3] = new Vector3(_circleMotion.CustomRightTransform.localPosition.x, _circleMotion.CustomRightTransform.localPosition.y, _circleMotion.CustomRightTransform.localPosition.z);


        mesh.vertices = vertices;
        mesh.triangles = triangleTemplate;
        filter.mesh = mesh;
        renderer.material = _material;

        obj.transform.localEulerAngles = Vector3.zero;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BandageDraw))]
public class BandageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var bandage = (BandageDraw)target;
        
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        if (GUILayout.Button("Preview Bandage"))
        {
           bandage.CreateAllMeshes();
        
        }


        EditorGUILayout.LabelField("PRESS THIS BEFORE PLAY IN INSPECTOR!", EditorStyles.boldLabel);
        if (GUILayout.Button("Delete Preview"))
        {
            bandage.DeleteAllMeshes();
        }
    }
}
#endif