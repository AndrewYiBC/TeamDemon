using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIndexRecorder : MonoBehaviour
{
    public static int RespawnIndex = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
