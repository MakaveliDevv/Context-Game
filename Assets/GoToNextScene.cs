using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToNextScene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            LoadNextScene();
        }

        if(Input.GetKeyDown(KeyCode.P)) 
        {
            ReloadScene();
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
