using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caps : MonoBehaviour
{
    Mesh mesh;

    [Range(1, 10)]
    [SerializeField]
    int capRingCount = 4;

    [Range(3, 32)]
    [SerializeField] int angularSegmentCount = 3;

    [Range(1, 10)]
    [SerializeField]
    private int maxRadius;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    private void Initialize()
    {
        mesh = new Mesh();
        mesh.name = "Cap";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void GenerateMesh()
    {
        mesh.Clear();

        float rIncrement = (float)maxRadius / (float)capRingCount; //increment radius by
        float dIncrement = 10 / capRingCount; //increment distance between rings by
        float zPos = 0;
        float radius = 0;

        List<Vector3> vertices = new List<Vector3>();

        //adding the pole as a vert at index 0
        vertices.Add(Vector3.zero);

        for (int i = 0; i < capRingCount; i++)
        {
            zPos += dIncrement;
            radius += rIncrement;

            for (int j = 0; j < angularSegmentCount; j++)
            {

                float t = j / (float)angularSegmentCount;
                float angRad = t * Mathfs.TAU;
                Vector2 dir = Mathfs.GetUnitVectorByAngle(angRad);

                vertices.Add((Vector3)dir * radius + new Vector3(0, 0, zPos));
            }
        }

        List<int> triangleIndices = new List<int>();

        //plot tris for first ring, NOT quads, all connect to pole
        for (int i = 0; i < angularSegmentCount; i++)
        {
            int root = 1;
            int p0;
            int p1;
            int p2;
            if (i == angularSegmentCount - 1)
            {
                p0 = root + i;
                p1 = 0;
                p2 = root;
            }
            else
            {
                p0 = root + i;
                p1 = 0;
                p2 = p0 + 1;
            }
            triangleIndices.Add(p0);
            triangleIndices.Add(p1);
            triangleIndices.Add(p2);
        }

        for (int i = 1; i < capRingCount; i++)
        {
            int root = (i * angularSegmentCount) + 1;

            for (int j = 0; j < angularSegmentCount; j++)
            {

                if (j == angularSegmentCount - 1) //last iteration of loop
                {
                    int p0 = root + j;
                    int p1 = p0 - angularSegmentCount;
                    int p2 = root - angularSegmentCount;
                    int p3 = root;

                    triangleIndices.Add(p0);
                    triangleIndices.Add(p1);
                    triangleIndices.Add(p3);

                    triangleIndices.Add(p1);
                    triangleIndices.Add(p2);
                    triangleIndices.Add(p3);
                }
                else
                {
                    int p0 = root + j;
                    int p1 = p0 - angularSegmentCount;
                    int p2 = p1 + 1;
                    int p3 = p0 + 1;

                    triangleIndices.Add(p0);
                    triangleIndices.Add(p1);
                    triangleIndices.Add(p3);

                    triangleIndices.Add(p1);
                    triangleIndices.Add(p2);
                    triangleIndices.Add(p3);
                }
            }
        }
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.RecalculateNormals();
    }
}
