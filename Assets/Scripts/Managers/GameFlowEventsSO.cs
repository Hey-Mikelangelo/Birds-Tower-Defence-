using UnityEngine;

[CreateAssetMenu]
public class GameFlowEventsSO : ScriptableObject
{
    public event System.Action onGameOver;

    public void GameOver()
    {
        onGameOver?.Invoke();
        Debug.Log("gameOver");  
    }
}
