using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;

    private float lastTimeAttacked;

    private float comboWindow = 0.4f;



    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0; //�޸�bug xinput��¼��һ�ε�ֵ�����¹���ʱ������ȷ
        if (comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
        //�ı乥�٣�TODO
        //player.anim.speed = ?;

        float attackDir = player.facingDir;
        if (xInput!=0)
        {
            attackDir = xInput;
        }
      
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
    
        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.15f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
