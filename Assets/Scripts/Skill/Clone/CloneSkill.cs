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

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool createCloneOnCounterAttack;
    [Header("Clone Can Duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    [Header("Crystal Instead Of Clone")]
     public bool crystalInsteadOfClone;

    public void CreateClone(Transform _clonePosation, Vector3 _offset)
    {
        if (crystalInsteadOfClone)
        {
            SkillManage.instance.crystal.CreateCrystal(_clonePosation);
            return;
        }
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetupClone(_clonePosation, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform), canDuplicateClone, chanceToDuplicate);

    }
    public void CreateCloneOnDushStart()
    {
        if (createCloneOnDashStart)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }
    public void CreateCloneOnDushOver()
    {
        if (createCloneOnDashOver)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }
    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (createCloneOnCounterAttack)
        {
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(1 * player.facingDir, 0)));
        }
    }
    private IEnumerator CreateCloneWithDelay(Transform _transform,Vector2 _offset)
    {
        yield return new WaitForSeconds(0.1f);
        CreateClone(_transform, _offset);
    }
}
