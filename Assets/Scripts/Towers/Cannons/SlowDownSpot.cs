using UnityEngine;

public class SlowDownSpot : Projectile
{
    [SerializeField, Range(0, 1)] private float speedSlowDownMult;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveToTargetTime;
    private bool doMove;
    private float progress = 0;

    public void MoveToPosition(Vector3 startPosition, Vector3 targetPosition, float moveToTargetTime)
    {
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;
        this.moveToTargetTime = moveToTargetTime;
        doMove = true;
    }

    private void Update()
    {
        if (doMove)
        {
            progress += Time.deltaTime / moveToTargetTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            if(progress >= 1)
            {
                doMove = false;
            }
        }
    }

    protected override void OnCollisionWithUnitEnter(Unit unit)
    {
        Mover mover = unit.GetComponent<Mover>();
        mover.SetSpeedMultiplier(speedSlowDownMult);
    }

    protected override void OnCollisionWithUnitExit(Unit unit)
    {
        base.OnCollisionWithUnitExit(unit);
        Mover mover = unit.GetComponent<Mover>();
        mover.ResetNormalSpeedMultiplier();
    }
}
