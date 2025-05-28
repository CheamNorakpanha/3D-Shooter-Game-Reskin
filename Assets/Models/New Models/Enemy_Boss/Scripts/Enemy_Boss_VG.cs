using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_VG : Enemy
{
    public IdleState_Boss_VG idleState { get; private set; }
    public MoveState_Boss_VG moveState { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        idleState = new IdleState_Boss_VG(this, stateMachine, "Idle");
        moveState = new MoveState_Boss_VG(this, stateMachine, "Move");
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
    }
}