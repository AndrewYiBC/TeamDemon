using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private int respawnIndex = 1;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int getRespawnIndex()
    {
        return respawnIndex;
    }

    public void setRespawnIndex(int index)
    {
        respawnIndex = index;
    }
}
