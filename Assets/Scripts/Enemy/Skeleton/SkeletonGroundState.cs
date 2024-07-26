using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Enemy_Skeleton enemy;

    protected Transform player;
    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _enemyMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _enemyMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManage.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 3)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

}
