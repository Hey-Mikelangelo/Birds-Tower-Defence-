using UnityEngine;

public abstract class TowerCannon : MonoBehaviour
{
    [SerializeField] private string projectileName;
    [SerializeField] private Transform shootOriginTransform;
    [SerializeField] private float cooldown;
    private ConcretePrefabPool<Projectile> projectilesPool;

    protected Transform ShootOriginTransform => shootOriginTransform;
    protected Vector3? TargetPosition { get; private set; }

    private float timeElapsed = 0;
    private bool isCooldownPassed = false;

    private void Start()
    {
        projectilesPool = PoolsManager.ProjectilesPool.GetPool(projectileName);
    }

    protected virtual void Update()
    {
        RunCooldownTimer();
    }

    protected Projectile GetProjectile()
    {
        return projectilesPool.GetInstance();
    }

    private void RunCooldownTimer()
    {
        if (isCooldownPassed)
        {
            return;
        }
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= cooldown)
        {
            isCooldownPassed = true;
            timeElapsed = 0;
        }
    }

    public void TryFire(Vector3? targetPosition)
    {
        TargetPosition = targetPosition;
        if (isCooldownPassed && CanFire())
        {
            isCooldownPassed = false;
            FireAction();
        }
    }

    protected abstract bool CanFire();
    protected abstract void FireAction();
}

