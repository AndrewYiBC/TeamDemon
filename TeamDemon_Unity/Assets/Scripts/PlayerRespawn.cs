using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float respawnOffsetY;

    void Start()
    {
        Vector3 respawnOffset = new Vector3(0f, respawnOffsetY, 0f);
        playerTransform.position = transform.GetChild(RespawnIndexRecorder.RespawnIndex).position + respawnOffset;
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
