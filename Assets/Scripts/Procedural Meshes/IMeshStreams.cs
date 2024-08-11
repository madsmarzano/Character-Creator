using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UIElements;

namespace ProceduralMeshes
{
    public interface IMeshStreams
    {
        //Initialize mesh data
        void Setup(Mesh.MeshData data, Bounds bounds, int vertexCount, int indexCount);

        //Copy vertex data to the mesh's vertex buffer
        void SetVertex(int index, Vertex data);

        void SetTriangle(int index, int3 triangle);
    }
}
