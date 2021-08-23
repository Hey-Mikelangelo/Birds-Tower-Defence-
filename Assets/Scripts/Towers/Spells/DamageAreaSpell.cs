using UnityEngine;

public class DamageAreaSpell : AreaSpell
{
    [SerializeField] private int dealtDamage;

    protected override void SpellAction()
    {
        base.SpellAction();
        foreach(Unit unit in unitsInRange)
        {
            unit.DealDamage(dealtDamage);
        }
    }
}
