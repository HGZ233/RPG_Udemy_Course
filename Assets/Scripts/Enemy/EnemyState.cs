using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    protected Rigidbody2D rb;
    //¹¥»÷×´Ì¬½áÊø´¥·¢Æ÷
    protected bool triggerCalled;
    private string animBoolName;
    protected float stateTimer;
    


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _enemyMachine, string _animBoolName)
    {
        enemyBase = _enemyBase;
        stateMachine = _enemyMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
        
    }
    public virtual void Update()
    {
        stateTimer-=Time.deltaTime;

   
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
