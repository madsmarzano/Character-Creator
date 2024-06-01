using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (MeshFilter))]
public class QuadStack : MonoBehaviour
{
    Mesh mesh; //stores mesh

    Vector3[] vertices; //stores vertices of the mesh
    int[] triangles; //stores faces of the mesh

    [Range(1, 5)] //display as a slider in inspector
    [SerializeField] int height;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadStack";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        CreateShape();
    }

    void CreateShape()
    {
        mesh.Clear();

        List<Vector3> verts = new List<Vector3>();

        for (int i = 0; i <= height; i++)
        {
            verts.Add(new Vector3(0, i, 0));
            verts.Add(new Vector3(1, i, 0));
        }

        List<int> triangleIndices = new List<int>();

        for (int i = 0; i < height; i++)
        {
            int root = i * 2;
            int topLeft = root + 2;
            int topRight = root + 3;
            int bottomRight = root + 1;

            //triangle 1
            triangleIndices.Add(root);
            triangleIndices.Add(topLeft);
            triangleIndices.Add(topRight);

            //triangle 2
            triangleIndices.Add(root);
            triangleIndices.Add(topRight);
            triangleIndices.Add(bottomRight);
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.RecalculateNormals();

    }
}
