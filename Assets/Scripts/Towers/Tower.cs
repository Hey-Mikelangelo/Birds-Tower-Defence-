using System;
using UnityEngine;

public class Tower : Unit, IColorAlphaChangable, IPlacable
{
    [SerializeField] private TowerCannonRotator towerCannonRotator;
    [SerializeField] private TargetDetectionSystem targetDetectionSystem;
    [SerializeField] private TowerCannon towerCannon;

    private SpriteRenderer[] spriteRenderers;
    private bool isPlaced;
    private bool isSetCanPlace;

    public event Action<Vector3> onRemovedFromPosition;

    public void Discard()
    {
        Destroy();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void PlaceOnMap()
    {
        isPlaced = true;
        targetDetectionSystem.InitSystem();

    }

    public void SetAllSpriteRenderersAlpha(float colorAlphaValue)
    {
        if (spriteRenderers == null)
        {
            return;
        }
        colorAlphaValue = Mathf.Clamp01(colorAlphaValue);
        foreach (var spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = colorAlphaValue;
            spriteRenderer.color = color;
        }
    }

    public void SetCanPlaceVisuals(bool canPlace)
    {
        if (canPlace && !isSetCanPlace)
        {
            SetAllSpriteRenderersAlpha(1f);
            isSetCanPlace = true;
        }
        else if (!canPlace && isSetCanPlace)
        {
            SetAllSpriteRenderersAlpha(0.4f);
            isSetCanPlace = false;
        }
    }

    private void Update()
    {
        if (!isPlaced)
        {
            return;
        }
        Vector3? targetPosition = targetDetectionSystem.GetDetectedTargetPos();
        towerCannonRotator.SetTarget(targetPosition);
        towerCannon.TryFire(targetPosition);
    }

    protected void Start()
    {
        base.Awake();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected override void BeforeDestroy()
    {
        base.BeforeDestroy();
        isPlaced = false;
        onRemovedFromPosition?.Invoke(transform.position);
    }



}
