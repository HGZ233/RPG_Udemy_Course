using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //这里是限制角色动作完成后出现滑行，如果需要实现冰面滑行效果需要注释掉
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
  
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0&&!player.isBusy)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
