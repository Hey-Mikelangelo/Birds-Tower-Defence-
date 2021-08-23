using UnityEngine;

public class ConcretePrefabPool<T> : MonoBehaviour where T : MonoBehaviour{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialInstances = 50;

    private PrefabPool pool;

    private void Awake()
    {
        pool = new PrefabPool(prefab, initialInstances);
    }

    public T GetInstance()
    {
        T newInstance = pool.GetInstance() as T;
        newInstance.transform.parent = transform;
        return newInstance;
    }
}
