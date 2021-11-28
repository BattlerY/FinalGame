using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{
    [SerializeField] private ParticleSystem _effect;

    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage, DamageType.Magical);
        Instantiate(_effect, entity.transform.position, Quaternion.identity, entity.transform);
    }
}
