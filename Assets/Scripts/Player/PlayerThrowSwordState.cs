using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowSwordState : PlayerState
{
    public PlayerThrowSwordState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.2f);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
