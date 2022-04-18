using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    [SerializeField] private float hp = 100f;

    public void DecreaseHP (float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        }
    }

    private void Die ()
    {
        Destroy(gameObject);
    }
}
