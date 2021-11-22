using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Unit
{
    [SerializeField] private int _armorUp;

    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage);
        UpArmor(_armorUp);
    }
}
