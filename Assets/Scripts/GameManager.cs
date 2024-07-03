using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedGameplayTime = 0f;
    [SerializeField] private float timeLimit = 600f; // 10 minutes
    [SerializeField] private AudioClip[] sounds;
    private AudioSource audioSource;
    [SerializeField] private int loadSceneDelay = 3;
    private bool gameOver;

    #region Singleton

    [HideInInspector] public static GameManager instance;

    private void Awake()
    {
        if (instance != null && gameObject != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(instance);

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    private void Update()
    {
        if (!gameOver)
        {
            elapsedGameplayTime += Time.deltaTime;
            UpdateTimerText(elapsedGameplayTime);
            ReachedTimeLimit();
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public bool HasReachedTimeLimit(float limit)
    {
        return elapsedGameplayTime >= limit;
    }

    private void ReachedTimeLimit()
    {
        if (HasReachedTimeLimit(timeLimit))
        {
            gameOver = true;

            audioSource.clip = sounds[1];
            audioSource.Play();
            elapsedGameplayTime = 0f;

            StartCoroutine(DelayedLoadScene("GameOverScene", loadSceneDelay));
        }
    }

    private IEnumerator DelayedLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        gameOver = false;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var timerText_gameObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerText_gameObj != null)
        {
            timerText = timerText_gameObj.GetComponent<TextMeshProUGUI>();
            timerText.gameObject.SetActive(true);

            // Reset the timer only if it's the GameOverScene
            if (scene.name == "GameOverScene")
            {
                elapsedGameplayTime = 0f;
            }
            else if(scene.name == "StartScene")
            {
                elapsedGameplayTime = 0f;
            }
        }
    }
}
