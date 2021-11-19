using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Mob
{
    [SerializeField] private ParticleSystem _effect;
    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage, true);
        Instantiate(_effect, entity.transform.position, Quaternion.identity, entity.transform);
    }
}
