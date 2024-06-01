using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


//--by madison marzano 

public class QuadGrid : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices; //stores vertices of the mesh
    int[] triangles; //stores faces of the mesh

    [Range(1, 5)] 
    [SerializeField] int height;

    [Range(1, 5)] 
    [SerializeField] int length;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadGrid";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        CreateGrid();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < height; i++)
        {
            int yPos = i;

            for (int j = 0; j < length; j++)
            {
                int xPos = j;
                Gizmos.DrawSphere(new Vector3 (xPos, yPos), .05f);
            }
        }
    }

    void CreateGrid()
    {
        mesh.Clear();

        List<Vector3> verts = new List<Vector3>();

        for (int i = 0; i <= height; i++)
        {
            int yPos = i;

            for (int j = 0; j <= length; j++)
            {
                int xPos = j;
                Vector3 position = new Vector3(xPos, yPos, 0);
                verts.Add(position);
            }
        }

        List<int> triangleIndices = new List<int>();

        for (int i = 0; i < length; i++)
        {
            int p0 = i;
            int p1 = i + (length + 1);
            int p2 = i + 1;

            triangleIndices.Add(p0);
            triangleIndices.Add(p1);
            triangleIndices.Add(p2);

        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.RecalculateNormals();

    }
}
