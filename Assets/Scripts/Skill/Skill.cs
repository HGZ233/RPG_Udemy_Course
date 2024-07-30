using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManage.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool canUseSkill()
    {
        if (cooldownTimer < 0)
        {
            useSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }
    public virtual void useSkill()
    {
    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        return closestEnemy;
    }
}
