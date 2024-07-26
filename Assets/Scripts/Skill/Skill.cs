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
}
