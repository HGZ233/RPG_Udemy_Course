using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //»÷ÍËÐ§¹û
    [Header("knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    protected bool isKnocked;
    [Header("Coilision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundChecKDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallChecKDistance;
    [SerializeField] protected LayerMask whatIsGround;


    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public EntityFX fx { get; private set; }
    #endregion

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponentInChildren<EntityFX>();
    }

    protected virtual void Update()
    {

    }
    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(0.07f);
        isKnocked = false;
    }

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    #region Velocity
    public void SetZeroVelocity()
    {
        SetVelocity(0, 0);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundChecKDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallChecKDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundChecKDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallChecKDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
}
