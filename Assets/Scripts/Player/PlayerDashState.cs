using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDurasion;
        SkillManage.instance.clone.CreateCloneOnDushStart();
        // SkillManage.instance.clone.CreateClone(player.transform,Vector3.zero);

    }

    public override void Exit()
    {
        base.Exit();
        SkillManage.instance.clone.CreateCloneOnDushOver();
        player.SetVelocity(0, rb.velocity.y);
        player.dashing = false;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);

        }
    }
}
