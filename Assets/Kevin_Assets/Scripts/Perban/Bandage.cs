using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bandage : MonoBehaviour
{
    [Header("Quads")]
    [SerializeField] private Color _color;
    [SerializeField] Material _material;
    [SerializeField] float _quadWidth = 2;

    [Header("Reference")]
    [SerializeField] CircleMotionTransform _circleMotion;
    [SerializeField] Transform _CircleMeshParent;

    List<GameObject> circleMeshesOBJ = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateAllMeshes()
    {
        //clear all child
        while(_CircleMeshParent.childCount > 0)
        {
            DestroyImmediate(_CircleMeshParent.GetChild(0).gameObject);
        }
        circleMeshesOBJ.Clear();


        //create child
        for(int i = 0; i < _circleMotion.CircleTransforms.Count; i++)
        {
            GameObject tempObj = new GameObject($"CircleMesh{i}");
            tempObj.transform.parent = _CircleMeshParent;


            if(i == _circleMotion.CircleTransforms.Count - 1)
            {
                CreateCircleMesh(tempObj, _circleMotion.CircleTransforms[i].position, _circleMotion.CircleTransforms[0].position);
            }
            else
            {
                CreateCircleMesh(tempObj, _circleMotion.CircleTransforms[i].position, _circleMotion.CircleTransforms[i + 1].position);

            }


            circleMeshesOBJ.Add(tempObj);
        }
    }


    private void CreateCircleMesh(GameObject obj, Vector3 startPos, Vector3 endPos)
    {
        Mesh mesh = new Mesh();
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();

        mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        int[] triangleTemplate = new int[6] { 0, 1, 2, 2, 1, 3 };

        //Top Right Vertice location
        vertices[0] = new Vector3(startPos.x, startPos.y, startPos.z + (-_quadWidth * 0.5f));

        //top left Vertice location
        vertices[1] = new Vector3(endPos.x, endPos.y, endPos.z + (-_quadWidth * 0.5f));

        //bot right Vertice location
        vertices[2] = new Vector3(startPos.x, startPos.y, startPos.z + (_quadWidth * 0.5f));

        //top right Vertice location
        vertices[3] = new Vector3(endPos.x, endPos.y, endPos.z + (_quadWidth * 0.5f));


        mesh.vertices = vertices;
        mesh.triangles = triangleTemplate;
        filter.mesh = mesh;
        renderer.material = _material;

    }


}
[CustomEditor(typeof(Bandage))]
public class BandageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var bandage = (Bandage)target;
        
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        if (GUILayout.Button("Render Lines"))
        {
           bandage.CreateAllMeshes();
        }
    }
}