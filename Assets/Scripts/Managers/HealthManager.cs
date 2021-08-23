using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnsignedIntValueSO health;
    [SerializeField] private EnemyPassedCounter enemyPassedCounter;
    [SerializeField] private GameFlowEventsSO gameFlowEvents;

    private void Start()
    {
        health.SetValue(maxHealth);
    }

    private void OnEnable()
    {
        enemyPassedCounter.onDealtDamage += EnemyPassedCounter_onDealtDamage;
        health.onValueChanged += Health_onValueChanged;
    }
    private void OnDisable()
    {
        enemyPassedCounter.onDealtDamage -= EnemyPassedCounter_onDealtDamage;
        health.onValueChanged -= Health_onValueChanged;
    }
    private void Health_onValueChanged(int healthValue)
    {
        if(healthValue == 0)
        {
            gameFlowEvents.GameOver();
            //TODO change to someting less lazy
            EditorApplication.isPaused = true;
        }
    }

    private void EnemyPassedCounter_onDealtDamage(int damage)
    {
        health.RemoveValue(damage);
    }

    
}
