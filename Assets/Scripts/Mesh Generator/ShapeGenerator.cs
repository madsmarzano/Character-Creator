using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public static class ShapeGenerator 
{
    public static Mesh GenerateSphereMesh(MeshRenderer renderer, MeshFilter filter, int resolution, int size, Vector3 direction)
    {
        renderer.sharedMaterial = new Material(Shader.Find("Standard"));

        Mesh planeMesh = UpdateSphereMesh(filter, resolution, size, direction);

        return planeMesh;
    }

    public static Mesh UpdateSphereMesh(MeshFilter filter, int resolution, int size, Vector3 direction)
    {
        Mesh planeMesh = new Mesh();

        int vertexPerRow = VertexAndTriangles.GetVertexPerRow(resolution);
        int numberOfVertices = vertexPerRow * vertexPerRow;
        Vector3[] vertices = VertexAndTriangles.GetSphereVertices(vertexPerRow, numberOfVertices, size, direction);

        int[] triangles = VertexAndTriangles.GetTriangles(vertexPerRow, resolution);

        planeMesh.vertices = vertices;
        planeMesh.triangles = triangles;

        planeMesh.RecalculateNormals();

        filter.mesh = planeMesh;

        return planeMesh;
    }
}
