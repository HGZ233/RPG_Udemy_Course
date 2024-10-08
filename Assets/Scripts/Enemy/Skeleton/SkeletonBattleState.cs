using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;


    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _enemyMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _enemyMachine, _animBoolName)
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

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (canAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }
        if (Vector3.Distance(player.position, enemy.transform.position) >= enemy.attackDistance)
        {
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        }
       


    }
    private bool canAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackColdDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
