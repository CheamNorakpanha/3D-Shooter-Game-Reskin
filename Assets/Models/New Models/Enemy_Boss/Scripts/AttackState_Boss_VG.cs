using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Boss_VG : EnemyState
{
    private Enemy_Boss_VG enemy;

    public AttackState_Boss_VG(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss_VG;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetFloat("AttackAnimIndex", Random.Range(0, 2));
        enemy.agent.isStopped = true;
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            if (enemy.PlayerInAttackRange())
                stateMachine.ChangeState(enemy.idleState);
            else
                stateMachine.ChangeState(enemy.moveState);
        }
    }
}
