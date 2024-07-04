using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedGameplayTime;
    [SerializeField] private float timeLimit = 600f; // 10 minutes
    [SerializeField] private AudioClip[] sounds;
    private AudioSource audioSource;
    [SerializeField] private int loadSceneDelay = 3;
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
        if (!gameOver)
        {
            elapsedGameplayTime -= Time.deltaTime;
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
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool HasReachedTimeLimit()
    {
        return elapsedGameplayTime <= 0;
    }

    private void ReachedTimeLimit()
    {
        if (HasReachedTimeLimit())
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
        if(players.Count <= spawnPositions.Count) 
        {
            for (int i = 0; i < players.Count; i++)
            {
                Instantiate(players[i], spawnPositions[i].position, Quaternion.identity);
            }
        }
        else 
        {
            Debug.LogError("Not enough spawn positions for all players");
        }

        // Find the players in game
        var PlayersInGame = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < PlayersInGame.Length; i++)
        {
            playersInGame.Add(PlayersInGame[i]);
        }

        // Find timer
        var timerText_gameObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerText_gameObj != null)
        {
            timerText = timerText_gameObj.GetComponent<TextMeshProUGUI>();
            timerText.gameObject.SetActive(true);

            // Reset the timer only if it's the GameOverScene or StartScene
            if (scene.name == "GameOverScene" || scene.name == "StartScene")
            {
                elapsedGameplayTime = timeLimit;
            }
        }
    }
}
