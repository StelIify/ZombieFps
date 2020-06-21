using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("Instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;

    }


    
}
