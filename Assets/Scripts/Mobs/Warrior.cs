
public class Warrior : Mob
{
    public override void UseAbilities(Entity entity)
    {
        entity.TakeDamage(Damage);
    }
}
