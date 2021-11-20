using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Unit
{
    [SerializeField] private int ArmorUp;
    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage);
        UpArmor(ArmorUp);
    }
}
