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
        //���������ƽ�ɫ������ɺ���ֻ��У������Ҫʵ�ֱ��滬��Ч����Ҫע�͵�
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
