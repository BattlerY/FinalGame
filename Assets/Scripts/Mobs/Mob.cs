using UnityEngine;
using UnityEngine.Events;

public enum Factions
{
    Hero,
    Enemy
}
public enum BattleMod
{
    Finding,
    Fighting
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Mob : Entity
{
    [SerializeField] protected int Damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _reward;
    [SerializeField] private int _cost;

    private Animator _animator;
    private BattleMod _battleMod;
    private Entity _curentGoal;
    private float _attackTime;
    private int _direction;

    public int Cost => _cost;

    public static event UnityAction<int> Dying;

    public abstract void UseAbilities(Entity entity);

    public override void OnDying()
    {
        _animator.Play(GeneralAnimatorController.States.Death);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder--;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;

        if (Faction == Factions.Enemy)
              Dying(_reward);
    }

    private void Awake()
    {
        _direction = Faction == Factions.Enemy ? -1 : 1;
        _animator = GetComponent<Animator>();
        _attackTime = 0;
    }

    private void Update()
    {
        if (_battleMod == BattleMod.Finding)
        {
            transform.Translate(Vector2.right * _direction * Time.deltaTime * _speed);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.right * _direction, _attackRange);

            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<Entity>(out _curentGoal) && _curentGoal.Faction != Faction)
                {
                    _battleMod = BattleMod.Fighting;
                    _animator.Play(GeneralAnimatorController.States.Slash);
                    _attackTime = 0;
                    break;
                }
            }
        }
        else if (_battleMod == BattleMod.Fighting)
        {
            _attackTime += Time.deltaTime;

            if (_attackTime >= _attackSpeed)
            {
                _attackTime = 0;
                UseAbilities(_curentGoal);
            }

            if (_curentGoal.IsDead)
            {
                _animator.Play(GeneralAnimatorController.States.Run);
                _battleMod = BattleMod.Finding;
            }
        }
    }
}
