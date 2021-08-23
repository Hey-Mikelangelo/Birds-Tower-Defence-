using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private int damage;
    [SerializeField] private int penetrateUnitsCount;

    private int damagedUnitsCount;
    protected override void OnCollisionWithUnitEnter(Unit unit)
    {
        if(damagedUnitsCount >= penetrateUnitsCount)
        {
            return;
        }
        unit.DealDamage(damage);
        damagedUnitsCount++;
        if(damagedUnitsCount == penetrateUnitsCount)
        {
            Destroy();
        }
    }

    protected override void OnSpawn()
    {
        base.OnSpawn();
        damagedUnitsCount = 0;
    }
}

