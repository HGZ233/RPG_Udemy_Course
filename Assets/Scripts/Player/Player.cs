using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;
    public bool isBusy { get; private set; }
    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    public float swordReturnImpact;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDurasion;
    private float defaultDashSpeed;
    //�Ƿ����ڳ�� �����������и�����
    public bool dashing;
    public float dashDir { get; private set; }

    public SkillManage skill { get; private set; }
    public GameObject sword { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSildeState wallSildeState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerThrowSwordState throwSwordState { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    public PlayerDeadState dieState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSildeState = new PlayerWallSildeState(this, stateMachine, "WallSilde");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        throwSwordState = new PlayerThrowSwordState(this, stateMachine, "isThrowSword");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Jump");
        dieState = new PlayerDeadState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManage.instance;
        stateMachine.Initialized(idleState);
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;

    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
        CheckForCounterAttackInput();
        if (Input.GetKeyDown(KeyCode.F))
        {
            skill.crystal.canUseSkill();
        }


    }
    public override void SlowEntityBy(float _slowPerecentage, float slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPerecentage);
        dashSpeed = dashSpeed * (1 - _slowPerecentage);
        jumpForce = jumpForce * (1 - _slowPerecentage);
        anim.speed = anim.speed * (1 - _slowPerecentage);
        Invoke("ReturnDefaultSpeed", slowDuration);
    }
    public override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
        dashSpeed = defaultDashSpeed;
        jumpForce = defaultJumpForce;
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }


    //todo ������Ż�
    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }
    private void CheckForDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManage.instance.dash.canUseSkill())
        {
            dashing = true;
            //�Ż����ܹ��ܣ���ֹ�����Ȱ����ܶ������޷��������ܷ��������
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);

        }
        //��ǽ״̬��������
        if (IsWallDetected())
        {
            return;
        }
    }
    private void CheckForCounterAttackInput()
    {
        //��Ծʱ��ֹ��
        if (rb.velocity.y != 0 || dashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && SkillManage.instance.counter.canUseSkill())
        {
            stateMachine.ChangeState(counterAttackState);
        }
        //��ǽ״̬��������
        if (IsWallDetected())
        {
            return;
        }
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(dieState);
    }




}
