using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    public float speed = 6f;
    public bool playerMovingOnLadder = false;

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.TryGetComponent<PlayerController>(out var player))         
        {
            player = collider.GetComponent<PlayerController>();
            rbPlayer = collider.GetComponent<Rigidbody2D>();
        
            player.playerDetected = true; // Set the flag to true when player is detected
            
            // Handle vertical movement here if needed
            player.inputDirection = new(0, player.inputVector.y);


            if(player.inputDirection.y > 0) 
            {
                playerMovingOnLadder = true;
                rbPlayer.velocity = new(0, speed); // Up
            } 
            else if(player.inputDirection.y < 0) 
            {
                playerMovingOnLadder = true;
                rbPlayer.velocity = new(0, -speed); // Down
            } 
            else 
            {
                playerMovingOnLadder = false;
                rbPlayer.velocity = new(0, rbPlayer.velocity.y); // No input
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
}
