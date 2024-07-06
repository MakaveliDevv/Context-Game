using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string sceneName;
    void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            LoadNextSceneFromStart();
        }
    }

    public void LoadNextSceneFromStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadGame() 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
