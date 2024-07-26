using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotActive(true);

    }

    public override void Exit()
    {
        base.Exit();
 
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.throwSwordState);
        }
        //不知道啥问题放在Enter无效，后续再查
        player.SetZeroVelocity();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
        {
            player.Flip();

        }

        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
        {
            player.Flip();
        }
    }
}
