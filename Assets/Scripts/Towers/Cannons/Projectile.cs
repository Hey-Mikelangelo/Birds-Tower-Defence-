using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Projectile : AThing
{
    [SerializeField] private LayerMask validLayerMask;

    public void DestroyProjectile()
    {
        Destroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isValidCollision(collision, out Unit unit))
        {
            OnCollisionWithUnitEnter(unit);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isValidCollision(collision, out Unit unit))
        {
            OnCollisionWithUnitExit(unit);
        }
    }

    private bool isValidCollision(Collision2D collision, out Unit unit)
    {
        unit = null;
        return validLayerMask.Contains(collision.gameObject.layer) 
            && collision.gameObject.TryGetComponent(out unit);
    }

    protected abstract void OnCollisionWithUnitEnter(Unit unit);
    protected virtual void OnCollisionWithUnitExit(Unit unit) { }

}
