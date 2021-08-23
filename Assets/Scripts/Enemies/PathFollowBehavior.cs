using UnityEngine;

public class PathFollowBehavior : MonoBehaviour
{
    private MovementPath movementPath;
    [SerializeField] private Mover mover;

    private int targetPointIndex;

    private void Awake()
    {
        movementPath = MovementPath.CurrentPath;
    }
    private void Start()
    {
        targetPointIndex = 0;
    }

    private void FixedUpdate()
    {
        if (DoSetNextPoint())
        {
            SetNextTargetPoint();
        }

        Vector2 targetPoint = GetTargetPoint();
        Vector2 currentPosition = GetCurrentPosition();
        float distanceToTargetPoint = Vector2.Distance(targetPoint, currentPosition);
        Vector2 directionToTargetPoint = GetDirectionToTargetPoint();

        mover.Move(directionToTargetPoint, distanceToTargetPoint);
    }

    private bool DoSetNextPoint()
    {
        return Vector2.Distance(GetTargetPoint(), GetCurrentPosition()) < 0.001f;
    }

    private bool SetNextTargetPoint()
    {
        if (movementPath.PathPoints.Count != targetPointIndex + 1)
        {
            targetPointIndex++;
            return true;
        }
        return false;
    }

    private Vector2 GetCurrentPosition()
    {
        return transform.position;
    }

    private Vector2 GetTargetPoint()
    {
        return movementPath.PathPoints[targetPointIndex];
    }

    private Vector2 GetDirectionToTargetPoint()
    {
        return (GetTargetPoint() - GetCurrentPosition()).normalized;
    }
}
