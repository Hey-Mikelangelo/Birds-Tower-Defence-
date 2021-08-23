using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class AreaSpell : Spell
{
    [SerializeField] private float areaRadius;
    [SerializeField] private int maxAffectedUnits;
    [SerializeField] private ColorTemporaryChanger colorChanger;
    [SerializeField] private Transform spellAreaUnitCircle;
    [SerializeField] private Color pulseColor;
    [SerializeField] private float colorPulseTime;
    [SerializeField] private Color cannotPlaceIndicationColor;

    protected List<Unit> unitsInRange { get; private set; } = new List<Unit>();
    private Collider2D[] collidersInRange;

    protected override void Awake()
    {
        base.Awake();
        collidersInRange = new Collider2D[maxAffectedUnits];
        spellAreaUnitCircle.transform.localScale = new Vector3(areaRadius * 2, areaRadius * 2, areaRadius * 2);
    }

    protected override void SpellAction()
    {
        unitsInRange.Clear();
        Physics2D.OverlapCircleNonAlloc(GetSpellCenter(), areaRadius, collidersInRange);
        for (int i = 0; i < collidersInRange.Length; i++)
        {
            Collider2D collider = collidersInRange[i];
            if (collider != null && collider.TryGetComponent(out Unit unit))
            {
                unitsInRange.Add(unit);
            }
        }
        colorChanger.Pulse(pulseColor, colorPulseTime);
    }

    private Vector2 GetSpellCenter()
    {
        return transform.position;
    }

    protected override void SetCanPlaceSpellVisuals(bool canPlace)
    {
        if (!canPlace)
        {
            colorChanger.SetTemporaryColor(cannotPlaceIndicationColor);
        }
        else
        {
            colorChanger.ResetOriginalColor();
        }
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.forward, areaRadius);
    }
#endif
}
