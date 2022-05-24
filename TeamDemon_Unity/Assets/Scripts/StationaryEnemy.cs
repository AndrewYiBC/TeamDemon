using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    private bool isFacingLeft = false;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform projectileStartingTransform;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float projectileCooldown;
    private bool isInCooldown = false;

    [SerializeField] private float activeRadius;
    private bool isActive = false;

    void Update()
    {
        isActive = Vector2.Distance(playerTransform.position, transform.position) < activeRadius;

        if (isActive)
        {
            if ((isFacingLeft && transform.position.x < playerTransform.position.x) || (!isFacingLeft && transform.position.x > playerTransform.position.x))
            {
                isFacingLeft = !isFacingLeft;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        
        if (isActive && !isInCooldown)
        {
            CreateProjectile();
            StartCoroutine(ProjectileCooldownCoroutine());
        }
    }

    private IEnumerator ProjectileCooldownCoroutine()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(projectileCooldown);
        isInCooldown = false;
    }

    private void CreateProjectile()
    {
        Vector2 relativeVector = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        float angleOffset = Vector2.SignedAngle(new Vector2(1, 0), relativeVector);
        float angleY = 0f;
        if (isFacingLeft)
        {
            angleY = 180f;
        }
        Instantiate(projectilePrefab, projectileStartingTransform.position, Quaternion.Euler(projectileStartingTransform.rotation.eulerAngles + new Vector3(0f, angleY, angleOffset)));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, activeRadius);
    }
}
