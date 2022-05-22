using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void DecreaseHP (float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        } else
        {
            DecreaseHPColorChange();
        }
    }

    private void Die ()
    {
        Destroy(gameObject);
    }

    private void DecreaseHPColorChange()
    {
        StartCoroutine(ColorChangeCoroutine());
    }

    private IEnumerator ColorChangeCoroutine()
    {
        sr.color = new Color(1f, 0.4f, 0.4f, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
