using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private CircleArea startArea;
    public CircleArea StartArea { get { return startArea; } }
    [SerializeField] private AIPointPatrol[] points;
    public int Length { get { return points.Length; } }
    public AIPointPatrol this[int i] { get => points[i]; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (var point in points)
        Gizmos.DrawWireSphere(point.transform.position, point.Radius);
    }
}
