using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : Entity
{

    [Header("Stunned Info")]
    //眩晕时间
    public float stunDuration;
    //眩晕方向
    public Vector2 stunDirection;
    protected bool canBeStuned;
    [SerializeField] protected GameObject counterImage;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;
    [SerializeField] private LayerMask whatIsPlayer;
    [Header("Attack Info")]
    public float attackDistance;
    public float attackColdDown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine {  get; private set; }
    public string lastAnimBoolName {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
        // stateMachine.Initialized();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
            moveSpeed = defaultMoveSpeed;
        }
    }
    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false);
    }

    #region Counter Attack Wiundow
    public virtual void OpenCounterAttackWindow()
    {
        canBeStuned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStuned = false;
        counterImage.SetActive(false);

    }
    #endregion

    public virtual bool CanBeStunned()
    {
        if (canBeStuned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50f, whatIsPlayer);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

}
