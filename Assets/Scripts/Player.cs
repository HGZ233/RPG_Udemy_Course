using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //角色动画控制器
    public Animator anim;
    //物理组件
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 7;
    [SerializeField]
    private float jumpForm =12;
    [Header("Dash info")]
    [SerializeField]
    private float dashSpeed = 15;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashTime;
    //[SerializeField]
    private float xInput;
    
    private int facingDir = 1;
    
    private bool facingRight = true;

    private bool isGrounded;
    [Header("Collision Info")]
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        whatIsGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChackInput();
        dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashDuration;
        }
        CollisionChecks();
        AnimatorController();
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void ChackInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
        FlipController();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, jumpForm);
        }
    }
    private void AnimatorController()
    {
       bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }else if(rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y-groundCheckDistance));
    }
}
