using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    [HideInInspector] public static GameManager instance;

    void Awake() 
    {
        if(instance != null) 
            Destroy(this);
        
        else
            instance = this;

        DontDestroyOnLoad(instance);
    }
    #endregion
    
    public List<Point> connectPointList;
}
