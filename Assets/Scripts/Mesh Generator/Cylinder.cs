using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// by Madison Marzano
/// 
/// Combined my QuadGrid algorithm with the QuadRing algorithm (by Freya Holmer) to create a procedurally generated cylinder.
/// 
/// </summary>

public class Cylinder : MonoBehaviour
{
    Mesh mesh;

    [Range(1, 10)]
    [SerializeField] int ringCount;

    [Range(3, 32)]
    [SerializeField] int angularSegmentCount = 3; //length; level of detail; whatever u wanna call it

    private int rSIZE = 11;
    [Range(1, 10)]
    [SerializeField] float[] radius;

    // parrallel array used to check if radius has been updated 
    private float[] currentRadius;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "Cylinder";
        GetComponent<MeshFilter>().mesh = mesh;
        currentRadius = new float[rSIZE];

        GenerateMesh();
    }

    //for debugging in the editor
    private void OnDrawGizmosSelected()
    {

        for (int i = 0; i <= ringCount; i++)
        {
            int zPos = i;

            for (int j = 0; j < angularSegmentCount; j++)
            {


                float t = j / (float)angularSegmentCount;
                float angRad = t * Mathfs.TAU;
                Vector2 dir = Mathfs.GetUnitVectorByAngle(angRad);

                Gizmos.DrawSphere((Vector3)dir * radius[i] + new Vector3(0, 0, zPos), 0.05f);
            }
        }
    }

    private void Update()
    {
        //GenerateMesh();
        for (int i = 0; i <= ringCount; i++)
        {
            if (currentRadius[i] != radius[i])
            {
                UpdateNeighbors(i);
            }
        }

    }

    private void UpdateNeighbors(int index)
    {
        if (index - 2 >= 0)
        {
            //update left neighbor
            radius[index-1] = 0.5f * (radius[index] + radius[index - 2]);
        }
        if (index + 2 <= ringCount)
        {
            //update right neighbor
            radius[index + 1] = 0.5f * (radius[index] + radius[index + 2]);
        }

        GenerateMesh();

    }

    private void GenerateMesh()
    {
        mesh.Clear();

        //radius = new float[ringCount + 1]; -- working on this

        List<Vector3> vertices = new List<Vector3>();

        for (int i = 0; i <= ringCount; i++)
        {
            int zPos = i;

            for (int j = 0; j < angularSegmentCount; j++)
            {


                float t = j / (float)angularSegmentCount;
                float angRad = t * Mathfs.TAU;
                Vector2 dir = Mathfs.GetUnitVectorByAngle(angRad);

                vertices.Add((Vector3)dir * radius[i] + new Vector3(0,0,zPos));

                currentRadius[i] = radius[i];
            }
        }

        List<int> triangleIndices = new List<int>();

        for (int i = 0; i < ringCount; i++)
        {
            int root = i * angularSegmentCount;

            for (int j = 0; j < angularSegmentCount; j++)
            {
                if (j == angularSegmentCount - 1) //last iteration of loop
                {
                    int p0 = root + j;
                    int p1 = root;
                    int p2 = p0 + angularSegmentCount;
                    int p3 = p1 + angularSegmentCount;

                    triangleIndices.Add(p0);
                    triangleIndices.Add(p1);
                    triangleIndices.Add(p2);

                    triangleIndices.Add(p1);
                    triangleIndices.Add(p3);
                    triangleIndices.Add(p2);
                }
                else
                {
                    int p0 = root + j;
                    int p1 = p0 + 1;
                    int p2 = p0 + angularSegmentCount;
                    int p3 = p2 + 1;

                    triangleIndices.Add(p0);
                    triangleIndices.Add(p1);
                    triangleIndices.Add(p2);

                    triangleIndices.Add(p1);
                    triangleIndices.Add(p3);
                    triangleIndices.Add(p2);
                }

            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangleIndices, 0);
            mesh.RecalculateNormals();
        }
    }
}
