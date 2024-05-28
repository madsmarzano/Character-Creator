using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{
    [Range(0.01f, 1)] //display as a slider in inspector
    [SerializeField] float radiusInner;

    [Range(0.01f, 1)]
    [SerializeField] float thickness;

    [Range(0.01f, 1)]
    [SerializeField] float length;

    [Range(3,32)]
    [SerializeField] int angularSegmentCount = 3; //detail level

    Mesh mesh;

    float RadiusOuter => radiusInner + thickness;
    int VertexCount => angularSegmentCount * 2;

    private void OnDrawGizmosSelected()
    {
        Gizmosfs.DrawWireCircle(transform.position, transform.rotation, radiusInner, angularSegmentCount);
        Gizmosfs.DrawWireCircle(transform.position, transform.rotation, RadiusOuter, angularSegmentCount);
    }

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void Update() => GenerateMesh(); //generate mesh every frame

    void GenerateMesh()
    {
        mesh.Clear(); //clear data from mesh

        int vCount = VertexCount;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2> ();

        for (int i = 0; i < angularSegmentCount+1; i++)
        {
            float t = i / (float)angularSegmentCount;
            float angRad = t * Mathfs.TAU;
            Vector2 dir = Mathfs.GetUnitVectorByAngle(angRad); //vector pointing in the direction of where two points will lie

            vertices.Add(dir * RadiusOuter); //add outer point first
            vertices.Add((Vector3)dir * radiusInner + new Vector3(0,0,length)); //add inner point
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            uvs.Add(new Vector2(t, 1));
            uvs.Add(new Vector2(t, 0));
        }

        List<int> triangleIndices = new List<int>();
        for (int i = 0; i < angularSegmentCount; i++)
        {
            int indexRoot = i * 2; //root = position on the outer ring we start from to generate the quad
            int indexInnerRoot = indexRoot + 1;
            int indexOuterNext = indexRoot + 2;
            int indexInnerNext = indexRoot + 3;

            //top triangle
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexInnerNext);

            //bottom triangle
            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexInnerRoot);

        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetNormals(normals); //this takes the place of RecalculateNormals; calculating the manually
        mesh.SetUVs(0, uvs);
    }
}
