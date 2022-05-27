using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInstruction : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float yOffset;

    void Update()
    {
        Vector3 offset = new Vector3(0f, yOffset, 0f);
        transform.position = playerTransform.position + offset;
    }
}
