using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManage : MonoBehaviour
{
    public static SkillManage instance;

    public DashSkill dash { get; private set; }

    public CloneSkill clone { get; private set; }

    public SwordSkill sword { get; private set; }
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        sword = GetComponent<SwordSkill>();
    }
}
