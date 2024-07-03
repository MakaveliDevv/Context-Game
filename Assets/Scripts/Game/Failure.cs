using UnityEngine;

public class Failure : MonoBehaviour
{
    // [SerializeField] private GameManager gameManager;
    
    // void Start() 
    // {
    //     gameManager = GameObject.FindAnyObjectByType<GameManager>();
    // }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell out of bounds.");

            // Check the number of players and spawn positions
            if (GameManager.instance.playersInGame.Count == 0)
            {
                Debug.LogError("No players found in GameManager.");
                return;
            }

            if (GameManager.instance.spawnPositions.Count == 0)
            {
                Debug.LogError("No spawn positions found in GameManager.");
                return;
            }

            // Reposition all players to their respective spawn positions
            for (int i = 0; i < GameManager.instance.playersInGame.Count; i++)
            {
                if (i < GameManager.instance.spawnPositions.Count)
                {
                    GameManager.instance.playersInGame[i].transform.position = GameManager.instance.spawnPositions[i].position;
                    Debug.Log($"Repositioning player {i} to spawn position {i}");
                }
                else
                {
                    Debug.LogError("Not enough spawn positions for all players.");
                }
            }
        }
    }
}
