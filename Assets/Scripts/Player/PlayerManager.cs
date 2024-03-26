using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    [HideInInspector] public static PlayerManager instance;

    void Awake() 
    {
        if(instance != null) 
            Destroy(this);
        
        else
            instance = this;

    }
    #endregion

    public bool artist;
    public bool designer;
    public bool developer; 

    public enum PlayerType 
    {
        ARTIST,
        DESIGNER,
        DEVELOPER
    }

    public PlayerType playerType;

    public void Update() 
    {
        WhichPlayer();
    }

    public void WhichPlayer() 
    {
        switch (playerType)
        {
            case(PlayerType.ARTIST):
                artist = true;
                designer = false;
                developer = false;

            break;

            case(PlayerType.DESIGNER):
                designer = true;
                developer = false;
                artist = false;

            break;

            case(PlayerType.DEVELOPER):
                developer = true;
                artist = false;
                designer = false;

            break;
        }
    }
}