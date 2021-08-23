
using UnityEngine;

[RequireComponent(typeof(PathFollowBehavior))]
public class Enemy : Unit
{
    [SerializeField] private int damage;
    [SerializeField] private ColorTemporaryChanger colorChanger;
    [SerializeField] private Color hitPulseColor;
    [SerializeField] private float hitColorPulseTime;

    public int Damage => damage;
    public override void DealDamage(int amount)
    {
        base.DealDamage(amount);
        if(amount > 0)
        {
            colorChanger.Pulse(hitPulseColor, hitColorPulseTime);
        }
    }
}
