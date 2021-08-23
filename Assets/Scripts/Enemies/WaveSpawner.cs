using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveSO> waves = new List<WaveSO>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject martenPrefab;
    [SerializeField] private GameObject worlfPrefab;


    private int currentWaveIndex = 0;
    private int currentBatchIndex = 0;
    private int currentEnemyIndex = 0;
    private Timer timer;
    private void Awake()
    {
        timer = new Timer(this);
    }

    private void OnEnable()
    {
        timer.OnTimerCycle += Timer_OnTimerCycle;
    }

    private void OnDisable()
    {
        timer.OnTimerCycle -= Timer_OnTimerCycle;
    }

    private void NewBatch(WaveSO.Batch batch)
    {
        timer.SetCycleTime(batch.spawnInterval);

    }

    private void Start()
    {
        timer.SetCycleTime(GetCurrentBatch().spawnInterval);
        timer.Start();
    }

    private WaveSO GetCurrentWave() => waves[currentWaveIndex];
    private WaveSO.Batch GetCurrentBatch() => GetCurrentWave().Batches[currentBatchIndex];
    private EnemyType GetCurrentEnemyType() => GetCurrentBatch().enemies[currentEnemyIndex];

    private void Timer_OnTimerCycle()
    {
        timer.Reset();
        timer.SetCycleTime(GetCurrentBatch().spawnInterval);
        EnemyType enemyType = GetCurrentEnemyType();
        SpawnEnemy(enemyType);

        if(GetCurrentBatch().enemies.Count - 1 == currentEnemyIndex)
        {
            if(GetCurrentWave().Batches.Count - 1 == currentBatchIndex)
            {
                if(waves.Count - 1 == currentWaveIndex)
                {
                    timer.StopAndReset();
                }
                else
                {
                    currentWaveIndex++;
                    currentBatchIndex = 0;
                    currentEnemyIndex = 0;
                    WaveSO newWave = GetCurrentWave();
                    timer.Reset();
                    timer.SetCycleTime(newWave.TimeToWaitBeforeWave);
                }
            }
            else
            {
                currentBatchIndex++;
                currentEnemyIndex = 0;
                timer.Reset();
                timer.SetCycleTime(GetCurrentBatch().spawnInterval);
            }
        }
        else
        {
            currentEnemyIndex++;
        }


    }

    private void SpawnEnemy(EnemyType enemyType)
    {    
        Instantiate(GetEnemyPrefab(enemyType), spawnPoint.position, Quaternion.identity);
    }

    private GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Martem:
                return martenPrefab;
            case EnemyType.Worlf:
                return worlfPrefab;
            default: 
                return null;
        }
    }

}
