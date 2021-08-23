using UnityEngine;

[CreateAssetMenu]
public class PlacableObjectSO : ScriptableObject
{
    public event System.Action<PlacableObjectSO> onSpawnButtonPressed;

    [SerializeField] private PlacableType type;
    [SerializeField] private GameObject prefab;
    [SerializeField] private new string name;
    [SerializeField] private int cost;
    public PlacableType PlacableType => type;
    public string Name => name;
    public int Cost => cost;

    public IPlacable NewPlacableObject => GetNewPlacable();

    public void OnSpawnButtonPressed()
    {
        onSpawnButtonPressed?.Invoke(this);
    }

    private IPlacable GetNewPlacable()
    {
        return Instantiate(prefab).GetComponent<IPlacable>();
    }
}
