using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    PlayerManager _playerManag;
    private Rigidbody2D rbPlayer;
    private PlayerController _playerContr;
    public float speed = 6f;
    
    #pragma warning disable IDE0052 // Remove unread private members
    private bool playerMovingOnLadder = false;
    #pragma warning restore IDE0052 // Remove unread private members

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            _playerManag = collider.GetComponent<PlayerManager>();
            _playerContr = collider.GetComponent<PlayerController>();
            rbPlayer = collider.GetComponent<Rigidbody2D>();
            
            if(_playerManag.playerType != PlayerManager.PlayerType.DEVELOPER) 
            {
                _playerContr.playerDetected = true; // Set the flag to true when player is detected
                
                // Handle vertical movement here if needed
                float verticalInput = Input.GetAxisRaw("Vertical");

                if(verticalInput > 0) 
                {
                    playerMovingOnLadder = true;
                    rbPlayer.velocity = new(0, speed); // Up
                } 
                else if(verticalInput < 0) 
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
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            if(_playerManag.playerType != PlayerManager.PlayerType.DEVELOPER)
            {
                Debug.Log(_playerManag.playerType);
                _playerContr.playerDetected = false; // Reset the flag when player exits the trigger area
            } 
        } 

        // if (collider.GetComponent<PlayerManager>()) 
        // {
        //     _playerContr.playerDetected = false; // Reset the flag when player exits the trigger area
        // }
    }

    // ONLY THE DESIGNER AND ARTIST MAY CLIMB THE LADDER


}
