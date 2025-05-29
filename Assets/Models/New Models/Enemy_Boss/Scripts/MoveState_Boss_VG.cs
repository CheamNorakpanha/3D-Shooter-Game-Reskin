using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState_Boss_VG : EnemyState
{
    private Enemy_Boss_VG enemy;
    private Vector3 destination;

    public MoveState_Boss_VG(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss_VG;
    }
    public override void Enter()
    {
        base.Enter();

        enemy.agent.speed = enemy.walkSpeed;
        enemy.agent.isStopped = false;

        destination = enemy.GetPatrolDestination();
        enemy.agent.SetDestination(destination);
    }

    public override void Update()
    {
        base.Update();

        enemy.FaceTarget(GetNextPathPoint());

        if(enemy.inBattleMode)
        {
            Vector3 playerPOS = enemy.player.position;
            enemy.agent.SetDestination(playerPOS);

            if (enemy.CanDoJumpAttack())
                stateMachine.ChangeState(enemy.jumpAttackState);
            else if (enemy.PlayerInAttackRange())
                stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, destination) < .25f)
                stateMachine.ChangeState(enemy.idleState);
        }
    }
}
