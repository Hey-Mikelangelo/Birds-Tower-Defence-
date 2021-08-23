using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveSO : ScriptableObject
{
    [SerializeField] private float timeToWaitBeforeWave = 1;
    [SerializeField] private List<Batch> batches = new List<Batch>();
    public List<Batch> Batches => batches;
    public float TimeToWaitBeforeWave => timeToWaitBeforeWave;
    [System.Serializable]
    public class Batch
    {
        [Range(0.01f, 3)]
        public float spawnInterval;
        public List<EnemyType> enemies;
    }
}
