using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedGameplayTime;
    [SerializeField] private float timeLimit = 600f; // 10 minutes
    [SerializeField] private AudioClip[] sounds;
    private AudioSource audioSource;
    [SerializeField] private float loadSceneDelay = 1.5f;
    private bool gameOver;
    public List<GameObject> players = new();
    public List<GameObject> playersInGame = new();
    public List<Transform> spawnPositions = new();

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

    private void Start()
    {
        elapsedGameplayTime = timeLimit;
    }

    private void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Tutorial")
        {
            if (timerText == null)
            {
                Debug.Log("TimerText is null in Update, trying to find it.");
                var timerTextGameObj = GameObject.FindGameObjectWithTag("Timer");
                if (timerTextGameObj != null)
                {
                    timerText = timerTextGameObj.GetComponent<TextMeshProUGUI>();
                }
            }

            if (timerText != null && !timerText.gameObject.activeInHierarchy)
            {
                Debug.Log("Activating TimerText in Update.");
                timerText.gameObject.SetActive(true);
            }
        }
        else if (currentSceneName == "GameOver" || currentSceneName == "StartScene")
        {
            return;
        }

        if (!gameOver)
        {
            elapsedGameplayTime -= Time.deltaTime;
            UpdateTimerText(elapsedGameplayTime);
            if (HasReachedTimeLimit())
            {
                TriggerGameOver();
            }
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private bool HasReachedTimeLimit()
    {
        return elapsedGameplayTime <= 0.03f;
    }

    private void TriggerGameOver()
    {
        gameOver = true;
        elapsedGameplayTime = 0f;
        audioSource.clip = sounds[1];
        audioSource.Play();

        SceneManager.LoadScene("GameOver");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        Cursor.lockState = CursorLockMode.Locked;

        spawnPositions.Clear();
        playersInGame.Clear();

        // Fetch the spawn positions
        var spawnPositionsInScene = GameObject.FindGameObjectsWithTag("SpawnPosition");
        for (int i = 0; i < spawnPositionsInScene.Length; i++)
        {
            spawnPositions.Add(spawnPositionsInScene[i].transform);
        }

        // Ensure there are enough spawn positions for all players
        if (players.Count <= spawnPositions.Count)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Instantiate(players[i], spawnPositions[i].position, Quaternion.identity);
            }
        }

        // Find the players in game
        var PlayersInGame = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < PlayersInGame.Length; i++)
        {
            playersInGame.Add(PlayersInGame[i]);
        }

        // Use a coroutine to delay the timer fetch
        StartCoroutine(FetchTimerWithDelay(scene));
    }

    private IEnumerator FetchTimerWithDelay(Scene scene)
    {
        yield return new WaitForSeconds(0.1f);

        var timerTextGameObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerTextGameObj != null)
        {
            timerText = timerTextGameObj.GetComponent<TextMeshProUGUI>();
            Debug.Log("TimerText fetched successfully in OnSceneLoaded.");
        }
        else
        {
            Debug.Log("Failed to fetch TimerText in OnSceneLoaded.");
        }

        // Reset the timer only if it's the GameOver or StartScene
        if (scene.name == "GameOver" || scene.name == "StartScene")
        {
            elapsedGameplayTime = timeLimit;
            gameOver = false;

            if (timerText != null)
            {
                timerText.gameObject.SetActive(false);
                Debug.Log("TimerText deactivated in OnSceneLoaded.");
            }
        }
        else if (scene.name == "Tutorial" && timerText != null)
        {
            timerText.gameObject.SetActive(true);
            Debug.Log("TimerText activated in OnSceneLoaded.");
        }
    }
}
