using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private bool playerMovingOnLadder;
    [SerializeField] private float radius = .25f;
    [SerializeField] private Vector2 colliderOffset = new();
    private CircleCollider2D circleCol;

    private void Start() 
    {
        if(circleCol == null) 
        {
            // Add a circle collider
            circleCol = gameObject.AddComponent<CircleCollider2D>();
            circleCol.isTrigger = true;
            circleCol.offset = colliderOffset;
            circleCol.radius = radius;
        }
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.TryGetComponent<PlayerController>(out var player) 
        && collider.TryGetComponent<Rigidbody2D>(out var rb))         
        {        
            // Debug.Log("Made contact with a player");
            // Set the flag to true when player is detected
            player.playerDetected = true;
            
            // Handle vertical movement here if needed
            player.inputDirection = new Vector2(0, player.inputVector.y);

            if(player.inputDirection.y > 0) 
            {
                playerMovingOnLadder = true;
                rb.velocity = new Vector2(0, speed); // Up
            } 
            else if(player.inputDirection.y < 0) 
            {
                playerMovingOnLadder = true;
                rb.velocity = new Vector2(0, -speed); // Down
            } 
            else 
            {
                playerMovingOnLadder = false;
                rb.velocity = new Vector2(0, rb.velocity.y); // No input
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.TryGetComponent<PlayerController>(out var player)) 
        {
            player.playerDetected = false; // Reset the flag when player exits the trigger area
        } 
    }

    // private void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }
}
