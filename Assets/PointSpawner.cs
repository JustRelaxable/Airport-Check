using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public Transform startPoint;
    public Transform left;
    public Transform right;
    public Transform offset;
    private float zOffset = 0;
    private float xOffset = 0;

    private void Awake()
    {
        zOffset = offset.position.z - startPoint.position.z;
        xOffset = left.position.x - right.position.x;
    }

    public Vector3[] GetPositions(int passengerCount)
    {
        Vector3[] positions = new Vector3[passengerCount];
        positions[0] = startPoint.position;

        for (int i = 1; i < passengerCount; i++)
        {
            float xValue = Random.Range(right.position.x, right.position.x + xOffset);
            float yValue = right.position.y;
            float zValue = startPoint.position.z + i * zOffset;
            Vector3 pos = new Vector3(xValue, yValue, zValue);
            positions[i] = pos;
        }
        return positions;
    }
}
