using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCH : MonoBehaviour
{
    public static GameManagerCH instance { get; private set; }
    public CupheadScript cuphead;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
