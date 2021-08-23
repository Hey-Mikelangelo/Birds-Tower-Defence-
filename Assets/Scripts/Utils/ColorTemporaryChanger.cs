using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorTemporaryChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    
    public void Pulse(Color color, float pulseTime)
    {
        spriteRenderer.color = color;
        if(pulseTime <= 0)
        {
            pulseTime = 0.1f;
        }
        if (enabled)
        {
            StopCoroutine(nameof(ColorPulseEnd));
            StartCoroutine(ColorPulseEnd(pulseTime));
        }   
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetTemporaryColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void ResetOriginalColor()
    {
        spriteRenderer.color = originalColor;
    }

    private IEnumerator ColorPulseEnd(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = originalColor;
    }
}
