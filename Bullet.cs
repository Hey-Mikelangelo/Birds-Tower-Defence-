using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField, Range(0, 5)] private int dealtDamage;
    [SerializeField] private int maxPenetrateUnits;

    private int damagedUnitsCount;

    protected override void OnCollisionWithUnitEnter(Unit unit)
    {
        if(damagedUnitsCount < maxPenetrateUnits)
        {
            unit.DealDamage(dealtDamage);
            damagedUnitsCount++;
            if (damagedUnitsCount >= maxPenetrateUnits)
            {
                Destroy();
            }
        }
    }

    protected override void OnSpawn()
    {
        base.OnSpawn();
        damagedUnitsCount = 0;
    }
}
