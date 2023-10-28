using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class MazeColliderAdjuster : MonoBehaviour
{
    private MeshCollider meshCollider;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();

        if (meshCollider != null)
        {
            AdjustColliderToCorners();
        }
        else
        {
            Debug.LogError("No MeshCollider component found. Attach a MeshCollider to the GameObject.");
        }
    }

    void AdjustColliderToCorners()
    {
        Mesh mesh = meshCollider.sharedMesh;

        if (mesh == null)
        {
            Debug.LogError("No mesh found on the MeshCollider.");
            return;
        }

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Check if the vertex is close to a corner (based on a threshold value).
            if (IsCloseToCorner(vertices[i]))
            {
                // Move the vertex to the nearest corner point.
                vertices[i] = FindNearestCorner(vertices[i]);
            }
        }

        // Update the mesh collider with the modified vertices.
        mesh.vertices = vertices;
        meshCollider.sharedMesh = mesh;
    }

    bool IsCloseToCorner(Vector3 vertex)
    {
        // You need to define your logic to check if a vertex is close to a corner.
        // You can use a simple distance threshold or other criteria.
        // For this example, we'll use a distance threshold of 0.1 units.
        float threshold = 0.1f;

        // Replace this with your actual corner positions.
        Vector3[] cornerPositions = GetCornerPositions();

        foreach (Vector3 corner in cornerPositions)
        {
            if (Vector3.Distance(vertex, corner) < threshold)
            {
                return true;
            }
        }

        return false;
    }

    Vector3 FindNearestCorner(Vector3 vertex)
    {
        // You need to define your logic to find the nearest corner to the vertex.
        // For this example, we'll assume there are corner positions already defined.
        Vector3[] cornerPositions = GetCornerPositions();

        Vector3 nearestCorner = Vector3.zero;
        float minDistance = float.MaxValue;

        foreach (Vector3 corner in cornerPositions)
        {
            float distance = Vector3.Distance(vertex, corner);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCorner = corner;
            }
        }

        return nearestCorner;
    }

    Vector3[] GetCornerPositions()
    {
        // Define the positions of the corners of your maze here.
        // This will depend on your specific maze layout.
        // For this example, we'll provide a simple set of corners.

        Vector3[] corners = new Vector3[]
        {
            new Vector3(1, 0, 1),   // Example corner 1
            new Vector3(9, 0, 1),   // Example corner 2
            new Vector3(1, 0, 9),   // Example corner 3
            new Vector3(9, 0, 9)    // Example corner 4
        };

        return corners;
    }
}
