using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh; //stores mesh

    Vector3[] vertices; //stores vertices of the mesh
    int[] triangles; //stores faces of the mesh

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; //add new mesh into the mesh filter

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 ( 0, 0, 0 ),
            new Vector3 ( 0, 0, 1 ),
            new Vector3 ( 1, 0, 0 ),
            new Vector3 ( 1, 0, 1 )
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
    }

    void UpdateMesh()
    {
        mesh.Clear(); //clear mesh from previous data

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
