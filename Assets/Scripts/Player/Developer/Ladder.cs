using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    // PlayerManager _playerManag;
    private Rigidbody2D rbPlayer;
    private Mover _mover;
    public float speed = 6f;
    
    #pragma warning disable IDE0052 // Remove unread private members
    public bool playerMovingOnLadder = false;
    #pragma warning restore IDE0052 // Remove unread private members

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            // _playerManag = collider.GetComponent<PlayerManager>();
            _mover = collider.GetComponent<Mover>();
            rbPlayer = collider.GetComponent<Rigidbody2D>();
            
            // if(_playerManag.playerType != PlayerManager.PlayerType.DEVELOPER) 
            // {
                _mover.playerDetected = true; // Set the flag to true when player is detected
                
                // Handle vertical movement here if needed
                // float verticalInput = Input.GetAxisRaw("Vertical");
                _mover.inputDirection = new(0, _mover.inputVector.y);
                // float verticalInput = _mover.inputDirection.y;


                if(_mover.inputDirection.y > 0) 
                {
                    playerMovingOnLadder = true;
                    rbPlayer.velocity = new(0, speed); // Up
                } 
                else if(_mover.inputDirection.y < 0) 
                {
                    playerMovingOnLadder = true;
                    rbPlayer.velocity = new(0, -speed); // Down
                } 
                else 
                {
                    playerMovingOnLadder = false;
                    rbPlayer.velocity = new(0, rbPlayer.velocity.y); // No input
                }
            // }

        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            // if(_playerManag.playerType != PlayerManager.PlayerType.DEVELOPER)
            // {
                _mover.playerDetected = false; // Reset the flag when player exits the trigger area
            // } 
        } 
    }

    // ONLY THE DESIGNER AND ARTIST MAY CLIMB THE LADDER


}