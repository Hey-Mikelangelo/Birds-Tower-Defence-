using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MovementPath : MonoBehaviour
{
    public static MovementPath CurrentPath;
    public List<Vector2> PathPoints { get; private set; }
    public List<Line2D> PathLines { get; private set; }

    [SerializeField] private List<Transform> pathPointsTransforms = new List<Transform>();

    private void OnEnable()
    {
        pathPointsTransforms.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                pathPointsTransforms.Add(transform.GetChild(i));
            }
        }
        PathPoints = new List<Vector2>(pathPointsTransforms.Count);
        PathLines = new List<Line2D>(pathPointsTransforms.Count + 1);
        foreach (Transform pathTransform in pathPointsTransforms)
        {
            PathPoints.Add(pathTransform.position);
        }
        if (PathPoints.Count > 0)
        {
            Vector2 prevPoint = PathPoints[0];
            for (int i = 1; i < PathPoints.Count; i++)
            {
                Vector2 currentPoint = PathPoints[i];
                PathLines.Add(new Line2D(prevPoint, currentPoint));
                prevPoint = currentPoint;
            }
        }


        CurrentPath = this;

    }

    private void OnDrawGizmos()
    {
        if(pathPointsTransforms.Count == 0)
        {
            return;
        }
        Transform prevPointTransform = pathPointsTransforms[0];
        Transform pointTransform;
        for (int i = 1; i < pathPointsTransforms.Count; i++)
        {
            pointTransform = pathPointsTransforms[i];
            Gizmos.DrawLine(prevPointTransform.position, pointTransform.position);
            prevPointTransform = pointTransform;
        }
    }
}