using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private float cloneDuration;
    [SerializeField] private GameObject clonePrefab;
    [Space]
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform _clonePosation,Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetupClone(_clonePosation,cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform));
       
    }
}
