using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIndexRecorder : MonoBehaviour
{
    public static int RespawnIndex = 4;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
