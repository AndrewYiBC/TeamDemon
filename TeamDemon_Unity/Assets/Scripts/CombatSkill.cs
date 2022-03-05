using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSkill : MonoBehaviour
{
    [SerializeField] private Transform combatPoint;
    [SerializeField] private GameObject skillPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseSkill();
        }
    }

    private void UseSkill()
    {
        Instantiate(skillPrefab, combatPoint.position, combatPoint.rotation);
    }
}
