using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyPassedCounter : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;

    public event System.Action<int> onDealtDamage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject passedGO = collider.gameObject;
        if (enemyLayerMask.Contains(passedGO.layer))
        {
            Enemy enemy = passedGO.GetComponent<Enemy>();
            int damage = enemy.Damage;
            onDealtDamage?.Invoke(damage);
            enemy.DealDamage(int.MaxValue);
        }

    }
}
