using System;
using UnityEngine;

public abstract class Spell : AThing, IPlacable
{
    [SerializeField] private LayerMask affectedLayer;
    [SerializeField] private float spellDuration;
    [SerializeField] private float spellActionCooldown;

    private Timer timer;
    private bool isSetCanPlace = true;

    public event Action<Vector3> onRemovedFromPosition;

    private void StartSpell()
    {
        timer.SetCycleTime(spellActionCooldown);
        timer.Start(stopAfterTime: spellDuration);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Discard()
    {
        Destroy();
    }

    protected override void Awake()
    {
        timer = new Timer(this);
        base.Awake();
    }

    private void OnEnable()
    {
        timer.OnTimerCycle += Timer_OnTimerCycle;
        timer.onTimerStopped += Timer_onTimerStopped;
    }

    private void OnDisable()
    {
        timer.OnTimerCycle -= Timer_OnTimerCycle;
        timer.onTimerStopped -= Timer_onTimerStopped;

    }

    protected override void BeforeDestroy()
    {
        base.BeforeDestroy();
        timer.StopAndReset();
        onRemovedFromPosition?.Invoke(transform.position);
    }

    protected override void OnSpawn()
    {
        base.OnSpawn();
    }

    private void EndSpell()
    {
        Destroy();
    }

    private void Timer_OnTimerCycle()
    {
        SpellAction();
    }

    private void Timer_onTimerStopped()
    {
        EndSpell();
    }

    public void PlaceOnMap()
    {
        StartSpell();
    }

    public void SetCanPlaceVisuals(bool canPlace)
    {
        if (canPlace && !isSetCanPlace)
        {
            SetCanPlaceSpellVisuals(canPlace);
            isSetCanPlace = true;
        }
        else if (!canPlace && isSetCanPlace)
        {
            SetCanPlaceSpellVisuals(canPlace);
            isSetCanPlace = false;
        }
    }

    protected abstract void SpellAction();
    protected abstract void SetCanPlaceSpellVisuals(bool canPlace);
}
