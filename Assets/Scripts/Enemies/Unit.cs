using UnityEngine;

public abstract class Unit : AThing, IPoolable
{
    [SerializeField] private int initialHealth;
    public int Health { get; private set; }

    public virtual void DealDamage(int amount)
    {
        amount = Mathf.Clamp(amount, 0, Health);
        Health -= amount;

        if(Health <= 0)
        {
            Destroy();
        }
    }

    protected override void OnSpawn()
    {
        base.OnSpawn();
        Health = initialHealth;
    }
}
