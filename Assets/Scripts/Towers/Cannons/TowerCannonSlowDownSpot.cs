using UnityEngine;

public class TowerCannonSlowDownSpot : TowerCannon
{
    [SerializeField] private float timeForBulletToReachTarget;
    [SerializeField, Range(0, 1)] private float minAlignmentCoef;

    protected override bool CanFire()
    {
        if (TargetPosition.HasValue)
        {
            Vector2 directionToTarget = (TargetPosition.Value - ShootOriginTransform.position);
            directionToTarget.Normalize();
            float alignmentCoef = Vector2.Dot(ShootOriginTransform.right, directionToTarget);
            return alignmentCoef >= minAlignmentCoef;
        }
        return false;
    }


    protected override void FireAction()
    {
        GameObject slowDownSpot = GetProjectile().gameObject;
        slowDownSpot.transform.position = ShootOriginTransform.position;
        slowDownSpot.GetComponent<SlowDownSpot>().MoveToPosition(ShootOriginTransform.position, TargetPosition.Value, timeForBulletToReachTarget);
    }
}

