using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class StatsText : MonoBehaviour
{
    [SerializeField] private UnsignedIntValueSO unsignedIntValueSO;
    private TextMeshProUGUI textContainer;

    private void Awake()
    {
        textContainer = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        unsignedIntValueSO.onValueChanged += UnsignedIntValueSO_onValueChanged;
        UnsignedIntValueSO_onValueChanged(unsignedIntValueSO.Value);
    }

    private void OnDisable()
    {
        unsignedIntValueSO.onValueChanged -= UnsignedIntValueSO_onValueChanged;
    }

    private void UnsignedIntValueSO_onValueChanged(int newValue)
    {
        textContainer.text = newValue.ToString();
    }
}
