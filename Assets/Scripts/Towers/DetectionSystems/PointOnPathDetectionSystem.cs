using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif

[ExecuteAlways]
public class PointOnPathDetectionSystem : TargetDetectionSystem
{
    [SerializeField] private Vector2 detectionBoxSize;
    [SerializeField] private float intervalToSearchNewTarget;
    private MovementPath path;
    private readonly List<Line2D> containedLineSegments = new List<Line2D>();
    private float timeElapsed = 0;
    private bool isCooldownPassed = false;
    private Vector2? prevTargetPoint;

    private void Awake()
    {
        path = MovementPath.CurrentPath;
    }

    private void OnEnable()
    {
        InitSystem();
    }

    private void Update()
    {
        RunCooldownTimer();
    }

    public override void InitSystem()
    {
        base.InitSystem();
        Rect detectionRect = new Rect(GetOrigin() - (detectionBoxSize * 0.5f), detectionBoxSize);
        containedLineSegments.Clear();

        foreach (Line2D line in path.PathLines)
        {
            Line2D? containedLineSegment = detectionRect.ContainsLineSegment(line);
            if (containedLineSegment.HasValue)
            {
                containedLineSegments.Add(containedLineSegment.Value);
            }
        }
    }
  
    public override Vector2? GetDetectedTargetPos()
    {
        if(containedLineSegments.Count == 0)
        {
            return null;
        }
        if (isCooldownPassed)
        {
            isCooldownPassed = false;
            int randomLineSegmentIndex = Random.Range(0, containedLineSegments.Count);
            Line2D randomLine = containedLineSegments[randomLineSegmentIndex];
            Vector2 randomPointOnLine = randomLine.GetPointOnLine(Random.Range(0f, 1f));
            prevTargetPoint = randomPointOnLine;
            return randomPointOnLine;
        }
        else
        {
            return prevTargetPoint;
        }
        
    }

    private void RunCooldownTimer()
    {
        if (isCooldownPassed)
        {
            return;
        }
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= intervalToSearchNewTarget)
        {
            isCooldownPassed = true;
            timeElapsed = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GetOrigin(), detectionBoxSize);
        Gizmos.color = Color.red;
        foreach (Line2D line in containedLineSegments)
        {
            Gizmos.DrawLine(line.P1, line.P2);
        }
    }
}
