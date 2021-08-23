using UnityEngine;

public abstract class AThing : MonoBehaviour, IPoolable
{
    private PrefabPool parentPool;

    protected void Destroy()
    {
        if (parentPool != null)
        {
            ReturnToPool();
        }
        else
        {
            BeforeDestroy();
            Destroy(gameObject);
        }
    }

     protected virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    protected virtual void BeforeDestroy()
    {
        gameObject.SetActive(false);

    }

    protected virtual void Awake()
    {
        OnSpawn();
    }

    private void ReturnToPool()
    {
        parentPool.ReturnObject(this);
    }

    public void OnSpawnFromPool()
    {
        OnSpawn();
    }

    public void BeforeReturnToPool()
    {
        BeforeDestroy();
    }

    public void SetParentPool(PrefabPool pool)
    {
        parentPool = pool;
    }

}
