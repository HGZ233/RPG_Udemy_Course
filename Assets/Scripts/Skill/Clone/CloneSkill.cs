using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : MonoBehaviour
{
    [Header("Clone Info")]
    [SerializeField] private float cloneDuration;
    [SerializeField] private GameObject clonePrefab;
    [Space]
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform _clonePosation)
    {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetupClone(_clonePosation,cloneDuration, canAttack);
       
    }
}
