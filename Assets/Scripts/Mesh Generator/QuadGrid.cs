using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


//--by madison marzano 

public class QuadGrid : MonoBehaviour
{
    Mesh mesh;

    [Range(1, 10)] 
    [SerializeField] int height;

    [Range(1, 10)] 
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

        for (int i = 0; i < height - 1; i++)
        {
            int root = i * (length + 1);

            for (int j = 0; j < length - 1; j++)
            {
                int p0 = root + j;
                int p1 = p0 + (length + 1);
                int p2 = p0 + 1;
                int p3 = p1 + 1;

                triangleIndices.Add(p0);
                triangleIndices.Add(p1);
                triangleIndices.Add(p2);

                triangleIndices.Add(p1);
                triangleIndices.Add(p3);
                triangleIndices.Add(p2);

            }


        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.RecalculateNormals();

    }
}
