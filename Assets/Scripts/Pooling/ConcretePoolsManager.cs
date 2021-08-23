using System.Collections.Generic;
using UnityEngine;

public class ConcretePoolsManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private List<PoolWithName> pools = new List<PoolWithName>();
        
    public ConcretePrefabPool<T> GetPool(string name)
    {
        foreach (PoolWithName pool in pools)
        {
            if(pool.name == name)
            {
                return pool.pool;
            }
        }
        return null;
    }

    [System.Serializable]
    public struct PoolWithName
    {
        public string name;
        public ConcretePrefabPool<T> pool;
    }

}

