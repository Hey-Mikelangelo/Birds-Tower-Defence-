using UnityEngine;

[CreateAssetMenu]
public class UnsignedIntValueSO : ScriptableObject
{
    [SerializeField] private int value;
    public int Value => value;

    public event System.Action<int> onValueChanged;

    public void AddValue(int addValue)
    {
        value += addValue;
        onValueChanged?.Invoke(value);
    }

    public void RemoveValue(int removeValue)
    {
        removeValue = Mathf.Clamp(removeValue, 0, Value);
        value -= removeValue;
        onValueChanged?.Invoke(value);
    }

    public void SetValue(int newValue)
    {
        if(newValue < 0)
        {
            newValue = 0;
        }
        value = newValue;
    }

    public static implicit operator int(UnsignedIntValueSO intValueSO)
    {
        return intValueSO.Value;
    }
}
