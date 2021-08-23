using System.Collections.Generic;
using UnityEngine;

public class PrefabPool
{
    private readonly GameObject prototype;
    private readonly Queue<IPoolable> readyElements;

    public PrefabPool(GameObject prototype, int initialInstances)
    {
        this.prototype = prototype;
        readyElements = new Queue<IPoolable>();

        for (int i = 0; i < initialInstances; i++)
        {
            var instance = CreateNewInstance();
            instance.BeforeReturnToPool();
            readyElements.Enqueue(instance);
        }
    }

    public void ReturnObject(IPoolable obj)
    {
        obj.BeforeReturnToPool();
        readyElements.Enqueue(obj);
    }

    public IPoolable GetInstance()
    {
        var instance = readyElements.Count < 1 ? CreateNewInstance() : readyElements.Dequeue();
        instance.OnSpawnFromPool();
        return instance;
    }

    private IPoolable CreateNewInstance()
    {
        var go = Object.Instantiate(prototype);
        var poolable = go.GetComponent<IPoolable>();
        poolable.SetParentPool(this);
        return poolable;
    }

}
