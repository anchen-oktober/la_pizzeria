using UnityEngine;
using UnityEngine.AI;

public class NavMeshVisualizer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        Gizmos.color = Color.cyan;

        for (int i = 0; i < navMeshData.indices.Length; i += 3)
        {
            Vector3 a = navMeshData.vertices[navMeshData.indices[i]];
            Vector3 b = navMeshData.vertices[navMeshData.indices[i + 1]];
            Vector3 c = navMeshData.vertices[navMeshData.indices[i + 2]];

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, a);
        }
    }
}