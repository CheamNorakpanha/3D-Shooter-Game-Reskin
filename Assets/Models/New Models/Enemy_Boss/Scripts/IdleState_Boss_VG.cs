using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Boss_VG : EnemyState
{
    private Enemy_Boss_VG enemy;

    public IdleState_Boss_VG(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss_VG;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.inBattleMode && enemy.PlayerInAttackRange())
            stateMachine.ChangeState(enemy.attackState);

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
