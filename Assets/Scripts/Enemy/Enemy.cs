using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : Entity
{

    [Header("Stunned Info")]
    //ѣ��ʱ��
    public float stunDuration;
    //ѣ�η���
    public Vector2 stunDirection;
    protected bool canBeStuned;
    [SerializeField] protected GameObject counterImage;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    [SerializeField] private LayerMask whatIsPlayer;
    [Header("Attack Info")]
    public float attackDistance;
    public float attackColdDown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
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
