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

        destination = enemy.GetPatrolDestination();
        enemy.agent.SetDestination(destination);
    }

    public override void Update()
    {
        base.Update();

        enemy.FaceTarget(GetNextPathPoint());

        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance + .05f)
            stateMachine.ChangeState(enemy.idleState);
    }
}
