using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAinmationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats state = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(state);            
            }
        }
    }
    private void ThrowSword()
    {
        SkillManage.instance.sword.CreateSword();
    }
}
