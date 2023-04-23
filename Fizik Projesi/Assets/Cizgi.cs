using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cizgi : MonoBehaviour
{
    public int segmentCount = 20;
    public float segmentSpacing = 0.1f;
    public Material lineMaterial;
    public LineRenderer lineRenderer;
    public void DrawPath(float angleRad, float horizontalVelocity, float verticalVelocity)
    {
        Vector3[] positions = new Vector3[segmentCount + 1];
        positions[0] = transform.position;

        for (int i = 1; i <= segmentCount; i++)
        {
            float elapsedTime = i * segmentSpacing;
            Vector3 currentPosition = transform.position + (horizontalVelocity * elapsedTime * Vector3.right)
                                                         + (verticalVelocity * elapsedTime * Vector3.up)
                                                         + (0.5f * UnityEngine.Physics.gravity * elapsedTime * elapsedTime);

            positions[i] = currentPosition;
        }
        
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}
