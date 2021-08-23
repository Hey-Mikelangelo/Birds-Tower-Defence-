using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class PlacableSpawnButton : MonoBehaviour
{
    [SerializeField] private PlacableObjectSO placableObject;

    private TextMeshProUGUI textContainer;

    private void Awake()
    {
        textContainer = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        textContainer.text = $"{placableObject.Name}: \n{placableObject.Cost}$";
    }

    public void OnButtonPressed()
    {
        placableObject.OnSpawnButtonPressed();
    }
}
