using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Number of players required to trigger scene switch
    public int requiredPlayers = 3; // Adjust as needed
    
    // Counter for players in trigger zone
    private int playersInZone = 0;

    // Scene to switch to
    public string nextSceneName;

    // Function to handle when a player enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            playersInZone++;
            CheckPlayersCount();
        }
    }

    // Function to handle when a player exits the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            playersInZone--;
            playersInZone = Mathf.Max(0, playersInZone); // Ensure playersInZone doesn't go negative
        }
    }

    // Function to check if all players are in the zone and switch scene if true
    private void CheckPlayersCount()
    {
        if (playersInZone >= requiredPlayers)
        {
            SwitchScene();
        }
    }

    // Function to switch scene
    private void SwitchScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
