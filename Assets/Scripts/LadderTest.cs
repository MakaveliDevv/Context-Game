using UnityEngine;

public class LadderTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Designer")
        {
            Debug.Log("Found player");
            // Disable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

         if (other.name == "Developer")
        {
            Debug.Log("Found player");
            // Disable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

         if (other.name == "Artist")
        {
            Debug.Log("Found player");
            // Disable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            // Re-enable gravity or any other downward forces on the player
            other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
