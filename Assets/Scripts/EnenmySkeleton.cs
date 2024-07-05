using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmySkeleton : Entity
{
    [Header("Move Info")]
    [SerializeField]
    private float moveSpeed = 4;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(moveSpeed*facingDir,rb.velocity.y);
        if (!isGrounded)
        {
            Flip();
        }
    }
}
