using UnityEngine;

using ProceduralMeshes;
using ProceduralMeshes.Generators;
using ProceduralMeshes.Streams;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Capsule : MonoBehaviour
{
    Mesh mesh;

    [SerializeField, Range(1, 50)]
    int resolution;

    //ring count for capsule


    Vector3[] vertices, normals;
    Vector4[] tangents;

    //gizmos configurations
    [System.Flags]
    public enum GizmoMode { Nothing = 0, Vertices = 1, Normals = 0b10, Tangents = 0b100 };
    [SerializeField] GizmoMode gizmos;

    private void OnDrawGizmos()
    {
        if (gizmos == GizmoMode.Nothing || mesh == null)
        {
            return;
        }

        bool drawVertices = (gizmos & GizmoMode.Vertices) != 0;
        bool drawNormals = (gizmos & GizmoMode.Normals) != 0;
        bool drawTangents = (gizmos & GizmoMode.Tangents) != 0;

        if (vertices == null)
        {
            vertices = mesh.vertices;
        }
        if (drawNormals && normals == null)
        {
            normals = mesh.normals;
        }
        if (drawTangents && tangents == null)
        {
            tangents = mesh.tangents;
        }

        Transform t = transform;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 position = t.TransformPoint(vertices[i]);
            if (drawVertices)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(position, 0.02f);
            }
            if (drawNormals)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(position, t.TransformDirection(normals[i]) * 0.2f);
            }
            if (drawTangents)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(position, t.TransformDirection(tangents[i]) * 0.2f);
            }
        }
    }

    private void Awake()
    {
        mesh = new Mesh
        {
            name = "Procedural Capsule"
        };
        GetComponent<MeshFilter>().mesh = mesh; //assign to mesh filter

    }

    private void OnValidate() => enabled = true;

    private void Update()
    {
        GenerateMesh();
        enabled = false;

        vertices = null;
        normals = null;
        tangents = null;

        //GetComponent<MeshRenderer>().material = materials[(int)material];
    }

    private void GenerateMesh()
    {

    }
}
