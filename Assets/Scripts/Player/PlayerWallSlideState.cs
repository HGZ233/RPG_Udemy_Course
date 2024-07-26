using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSildeState : PlayerState
{
    public PlayerWallSildeState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (yInput < 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.velocity.y * 0.6f);
        }
        if (!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
