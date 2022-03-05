using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSkill : MonoBehaviour
{
    [SerializeField] private Transform combatPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseSkill();
        }
    }

    private void UseSkill()
    {

    }
}
