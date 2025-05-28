using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_VG : Enemy
{
    public float attackRange;

    public IdleState_Boss_VG idleState { get; private set; }
    public MoveState_Boss_VG moveState { get; private set; }
    public AttackState_Boss_VG attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new IdleState_Boss_VG(this, stateMachine, "Idle");
        moveState = new MoveState_Boss_VG(this, stateMachine, "Move");
        attackState = new AttackState_Boss_VG(this, stateMachine, "Attack");
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

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackRange;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}