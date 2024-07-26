using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonAttackState(Enemy _enemyBase, EnemyStateMachine _enemyMachine, string _animBoolName,Enemy_Skeleton _enemy) : base(_enemyBase, _enemyMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();

    }

    public override void Exit()
    {
        base.Exit();
        //记录最后一次攻击冷却时间，用于计算敌人攻击冷却
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
