using UnityEngine;

public class TowerCannonFireBullet : TowerCannon
{
    [SerializeField] private float bulletSpeed = 5;
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
        GameObject bullet = GetProjectile().gameObject;
        bullet.transform.position = ShootOriginTransform.position;
        bullet.transform.rotation = ShootOriginTransform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = ShootOriginTransform.right * bulletSpeed;
    }
}

