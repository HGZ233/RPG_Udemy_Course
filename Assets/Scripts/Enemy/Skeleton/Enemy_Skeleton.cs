using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
 

    #region State
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState {  get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunState { get; private set; }
    public SkeletonDeadState dieState { get; private set; }

    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunState = new SkeletonStunnedState(this, stateMachine, "Stun", this);
        dieState = new SkeletonDeadState(this, stateMachine, "Idle", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialized(idleState);
    }

    protected override void Update()
    {
        base.Update();
 
    }
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;

    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(dieState);
    }
}
