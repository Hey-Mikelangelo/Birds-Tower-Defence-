using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float timeToDestroy;
    private float timeElapsed;

    private void OnEnable()
    {
        timeElapsed = 0;
    }


    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeToDestroy)
        {
            projectile.DestroyProjectile();
        }
    }
}
