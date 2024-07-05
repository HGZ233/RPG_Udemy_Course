using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField]
    private float moveSpeed = 7;
    [SerializeField]
    private float jumpForm =12;
    //[SerializeField]
    private float xInput;
    [Header("Dash Info")]
    [SerializeField]
    private float dashSpeed = 15;
    [SerializeField]
    private float dashDuration;
    //冲刺计时器
    private float dashTime;
    //冲刺冷却时间计时器
    private float dashCooldownTimer;
    //冲刺冷却
    [SerializeField]
    private float dashCooldown = 2;

    private bool isOnCooldown=true;
    [Header("Attack Info")]
    private bool isAttacking;

    private int comboCounter;
    [SerializeField]
    private float comboTimeCounter;
    [SerializeField]
    private float comboTime =0.3f;

    protected override void Start()
    {
        base.Start();

    }   
    // Start is called before the first frame update
    //void  Start()
    //{

    //    //rb = GetComponent<Rigidbody2D>();
    //    //anim = GetComponentInChildren<Animator>();
    //    whatIsGround = LayerMask.GetMask("Ground");
    //}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Movement();
        ChackInput();
        //CollisionChecks();
        AnimatorController();
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;
    }


    private void ChackInput()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnCooldown&&!isAttacking)
        {
            if (dashCooldownTimer<0&&!isAttacking)
            {
                dashCooldownTimer = dashCooldown;
                dashTime = dashDuration;
            }
        }
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
    }
    /// <summary>
    /// 攻击事件
    /// </summary>
    private void StartAttackEvent()
    {
        if (!isGrounded) 
        {
            return;
        };
        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;
        //在comboTime时间内键入攻击状态向下变化，否则攻击状态归0
        comboTimeCounter = comboTime;
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
           // StartCoroutine(DashCooldown());
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
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);


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
    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }


    //IEnumerator DashCooldown()
    //{
    //    isOnCooldown = false;
    //    yield return new WaitForSeconds(dashCooldownTimer);
    //    isOnCooldown = true;
    //}


}
