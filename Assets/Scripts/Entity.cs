using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    //角色动画控制器
    protected Animator anim;
    //物理组件
    protected Rigidbody2D rb;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision Info")]
    [SerializeField]
    protected float groundCheckDistance;
    [SerializeField]
    protected Transform groundCheack;
    [SerializeField]
    [Space]
    protected Transform wallCheck;
    [SerializeField]
    protected float wallCheckDistance;
    [SerializeField]
    protected LayerMask whatIsGround;
    //是否在地面
    protected bool isGrounded;
    protected bool isWallDetected;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        whatIsGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheack.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// 显示对象绘制工具
    /// 用来在Scene调试判断射线是否触发
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheack.position, new Vector3(groundCheack.position.x, groundCheack.position.y - groundCheckDistance));
    }
}
