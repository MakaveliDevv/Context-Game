using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed = 6f;
    public bool playerMovingOnLadder = false;
    public float radius = 2f;
    private CircleCollider2D circleCol;

    private void Update() 
    {
        if(circleCol == null) 
        {
            // Add a sphere collider
            circleCol = gameObject.AddComponent<CircleCollider2D>();
            circleCol.isTrigger = true;
            circleCol.radius = radius;
        }
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.TryGetComponent<PlayerController>(out var player) 
        && collider.TryGetComponent<Rigidbody2D>(out var rb))         
        {        
            Debug.Log("Made contact with a player");
            // Set the flag to true when player is detected
            player.playerDetected = true;
            
            // Handle vertical movement here if needed
            player.inputDirection = new(0, player.inputVector.y);


            if(player.inputDirection.y > 0) 
            {
                playerMovingOnLadder = true;
                rb.velocity = new(0, speed); // Up
            } 
            else if(player.inputDirection.y < 0) 
            {
                playerMovingOnLadder = true;
                rb.velocity = new(0, -speed); // Down
            } 
            else 
            {
                playerMovingOnLadder = false;
                rb.velocity = new(0, rb.velocity.y); // No input
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


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
