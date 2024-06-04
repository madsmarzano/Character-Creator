using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSphere : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private int size = 1;
    [Range(0, 7)][SerializeField] private int resolution = 1;

    private static Vector3[] directions =
    {
        Vector3.up,
        Vector3.down,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private MeshFilter[] filter;
    private MeshRenderer[] meshRenderer;

    private int currentResolution;
    private int currentSize;

    private void Start()
    {
        GenerateInitalMesh();
    }

    private void OnValidate()
    {
        if (filter == null || meshRenderer == null)
            GenerateInitalMesh();

        if (resolution != currentResolution || currentSize != size)
            for (int i = 0; i < 6; i++) // why 6??
                ShapeGenerator.UpdateSphereMesh(filter[i], resolution, size, directions[i]);

        currentResolution = resolution;
        currentSize = size;
                
    }

    private void GenerateInitalMesh()
    {
        this.name = "CubeSphere";

        filter = new MeshFilter[6];
        meshRenderer = new MeshRenderer[6];

        for (int i = 0; i < 6; i++)
        {
            var children = new GameObject("Sphere Face");
            children.transform.parent = this.transform;
            filter[i] = children.AddComponent<MeshFilter>();
            meshRenderer[i] = children.AddComponent<MeshRenderer>();

            ShapeGenerator.GenerateSphereMesh(meshRenderer[i], filter[i], resolution, size, directions[i]);
        }

        currentResolution = resolution;
        currentSize = size;
    }
}
