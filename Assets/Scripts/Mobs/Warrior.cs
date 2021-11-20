
public class Warrior : Unit
{
    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage);
    }
}
