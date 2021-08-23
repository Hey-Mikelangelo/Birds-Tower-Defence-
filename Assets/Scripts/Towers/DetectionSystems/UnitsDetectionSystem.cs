using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class UnitsDetectionSystem : TargetDetectionSystem
{
    [SerializeField] protected float detectionRadius;
    [SerializeField] private LayerMask enemyLayermask;
    [SerializeField] private int maxDetectedEnemyCount = 5;
    private Collider2D[] collidersInRange;
    private Collider2D prevTargetCollider;
    public override Vector2? GetDetectedTargetPos()
    {
        for (int i = 0; i < collidersInRange.Length; i++)
        {
            collidersInRange[i] = null;
        }
        if (Physics2D.OverlapCircleNonAlloc(GetOrigin(), detectionRadius, collidersInRange, enemyLayermask.value) > 0)
        {
            return GetTargetFromColliders(collidersInRange).position;
        }
        else
        {
            return null;
        }
    }

    protected void Awake()
    {
        collidersInRange = new Collider2D[maxDetectedEnemyCount];
    }

    private Transform GetTargetFromColliders(Collider2D[] collidersInRange)
    {
        if (prevTargetCollider != null && 
            collidersInRange.Contains(prevTargetCollider))
        {
            return prevTargetCollider.transform;
        }
        if (collidersInRange[0] != null)
        {
            prevTargetCollider = collidersInRange[0];
            return collidersInRange[0].transform;
        }
        else
        {
            return null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(GetOrigin(), Vector3.forward, detectionRadius);
    }
#endif
}
