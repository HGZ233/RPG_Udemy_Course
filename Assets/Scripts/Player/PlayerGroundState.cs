using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    
    }

    public override void Exit()
    {
        base.Exit();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse1) &&HasNoSword() )
        {
            stateMachine.ChangeState(player.aimSwordState);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))//||Input.GetKey(KeyCode.X)Hold¹¥»÷Ä£Ê½
        {
            stateMachine.ChangeState(player.primaryAttackState);            
        }
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
     
        if (Input.GetKeyDown(KeyCode.Space)&& player.IsGroundDetected())//
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
