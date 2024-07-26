using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12f;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;

    public float bounceSpeed = 20f;
    private bool isBouncing = true;
    public int amountOfBounce = 4;
    public List<Transform> enemyTarget;
    private int targetIndex;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CircleCollider2D>();


    }
    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        rb.velocity = _dir; ;
        rb.gravityScale = _gravityScale;
        player = _player;
        anim.SetBool("Rotation", true);

    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 0.7f)
            {
                player.CatchTheSword();
            }
        }
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            {
                targetIndex++;
                amountOfBounce--;
                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            return;
        }
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                List<Collider2D> sortColliders = TargetDistance(colliders);
                foreach (var hit in sortColliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }

        StuckInfo(collision);
    }

    private void StuckInfo(Collider2D collision)
    {

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing && enemyTarget.Count > 0)
        {
            return;
        }
        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }

    /// <summary>
    /// 按检测到敌人距离排序
    /// </summary>
    /// <param name="colliders"></param>
    /// <returns></returns>
    private List<Collider2D> TargetDistance(Collider2D[] colliders)
    {
        List<KeyValuePair<Collider2D, float>> colliderDistanceList = new List<KeyValuePair<Collider2D, float>>();

        for (int i = 0; i < colliders.Length; i++)
        {
            var clTransform = colliders[i].gameObject.transform;
            var distance = Vector2.Distance(player.transform.position, clTransform.position);
            colliderDistanceList.Add(new KeyValuePair<Collider2D, float>(colliders[i], distance));
        }

        // 按距离排序
        colliderDistanceList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        // 创建排序后的Collider2D列表
        List<Collider2D> sortedColliders = new List<Collider2D>();
        for (int i = 0; i < colliderDistanceList.Count; i++)
        {
            sortedColliders.Add(colliderDistanceList[i].Key);
        }

        return sortedColliders;
    }

}
