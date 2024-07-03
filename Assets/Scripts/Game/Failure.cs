using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager class

public class Failure : MonoBehaviour
{
    public Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the trigger
        if (other.CompareTag("Player"))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // other.transform.position = spawnPosition.position;
        }
    }
}
