using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] private ConcretePoolsManager<Projectile> projectilesPool;
    public static ConcretePoolsManager<Projectile> ProjectilesPool { get; private set; }

    private void Awake()
    {
        ProjectilesPool = projectilesPool;
    }
}

