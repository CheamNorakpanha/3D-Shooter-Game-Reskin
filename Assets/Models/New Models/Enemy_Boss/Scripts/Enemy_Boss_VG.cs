using UnityEngine;

public class Enemy_Boss_VG : Enemy
{
    [Header("Boss details")]
    public float actionCooldown = 10;
    public float attackRange;

    [Header("Ability")]
    public ParticleSystem flamethrower;
    public float abilityCooldown;
    private float lastTimeUsedAbility;
    public float flamethrowDuration;
    public bool flamethrowActive { get; private set; }

    [Header("Jump Attack")]
    public float jumpAttackCooldown = 10;
    
    private float lastTimeJumped;
    public float travelTimeToTarget = 1;
    public float minJumpDistanceRequired;
    [Space]
    [SerializeField] private LayerMask whatToIgnore;

    public IdleState_Boss_VG idleState { get; private set; }
    public MoveState_Boss_VG moveState { get; private set; }
    public AttackState_Boss_VG attackState { get; private set; }
    public JumpAttackState_Boss_VG jumpAttackState { get; private set; }
    public AbilityState_Boss_VG abilityState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new IdleState_Boss_VG(this, stateMachine, "Idle");
        moveState = new MoveState_Boss_VG(this, stateMachine, "Move");
        attackState = new AttackState_Boss_VG(this, stateMachine, "Attack");
        jumpAttackState = new JumpAttackState_Boss_VG(this, stateMachine, "JumpAttack");
        abilityState = new AbilityState_Boss_VG(this, stateMachine, "Ability");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        if (ShouldEnterBattleMode())
            EnterBattleMode();
    }

    public override void EnterBattleMode()
    {
        base.EnterBattleMode();
        stateMachine.ChangeState(moveState);
    }

    public void ActivateFlamethrower(bool activate)
    {
        flamethrowActive = activate;

        if (!activate)
        {
            flamethrower.Stop();
            anim.SetTrigger("StopFlamethrower");
            Debug.Log("flame stopped!");
            return;
        }
        
        var mainModule = flamethrower.main;
        var extraModule =  flamethrower.transform.GetChild(0).GetComponent<ParticleSystem>().main;

        mainModule.duration = flamethrowDuration;
        extraModule.duration = flamethrowDuration;

        flamethrower.Clear();
        flamethrower.Play();
    }

    public bool CanDoAbility()
    {
        if (Time.time > lastTimeUsedAbility + abilityCooldown)
        {
            return true;
        }

        return false;
    }

    public bool CanDoJumpAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < minJumpDistanceRequired)
            return false;

        if (Time.time > lastTimeJumped + jumpAttackCooldown && IsPlayerInClearSight())
        {
            return true;
        }

        return false;
    }

    public void SetJumpAttackOnCooldown() => lastTimeJumped = Time.time;

    public void SetAbilityOnCooldown() => lastTimeUsedAbility = Time.time;

    public bool IsPlayerInClearSight()
    {
        Vector3 myPos = transform.position + new Vector3(0, 1.6f, 0); // Adjust for height
        Vector3 playerPos = player.position + Vector3.up;
        Vector3 directionToPlayer = (playerPos - myPos).normalized;

        if (Physics.Raycast(myPos, directionToPlayer, out RaycastHit hit, 100, ~whatToIgnore))
        {
            if (hit.transform == player || hit.transform.parent == player)
                return true;
        }

        return false;
    }


    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackRange;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (player != null)
        {
            Vector3 myPos = transform.position + new Vector3(0, 1.6f, 0); // Adjust for height
            Vector3 playerPos = player.position + Vector3.up;

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(myPos, playerPos);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minJumpDistanceRequired);
    }
}