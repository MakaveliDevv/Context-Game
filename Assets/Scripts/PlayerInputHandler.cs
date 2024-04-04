using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
     private PlayerInput playerInput;
     private PlayerController player;

     private void Awake() 
     {
          // Player input component
          playerInput = GetComponent<PlayerInput>();

          var players = FindObjectsOfType<PlayerController>();

          // Initialize player index from the playerinput
          var index = playerInput.playerIndex;

          // Fetch the player index and initialize it to the index from playerinput
          player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
          Debug.Log(player.GetPlayerIndex());

     }

     // Movement input
     public void MovementInput(InputAction.CallbackContext ctx) 
     {
          player.SetInputVector(ctx.ReadValue<Vector2>());
     }

     // Jump input
     public void JumpInput(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
          {
               player.Jump();
               Debug.Log("We can jump");
          }
     }

     // Extend ability holding
     public void ExtendAbility(InputAction.CallbackContext ctx) 
     {
          if(!player.inputContr.isExtending && ctx.performed) 
          {
               Debug.Log(ctx + "is performerd");
               player.ExtendInput();
          }
     }

     // Extend ability by pressing
     public void ExtendAbilityPress(InputAction.CallbackContext ctx) 
     {
          if(player.inputContr.isRetracting && ctx.performed) 
          {
               Debug.Log(ctx.performed);
               player.ExtendInput();
          }
     }

     // Retract Input
     public void RetractInput(InputAction.CallbackContext ctx) 
     {
          if(!player.inputContr.isRetracting && ctx.performed) 
          {
               Debug.Log(ctx.performed);
               player.RetractInput();
          }
     }

     // Teleport ability
     public void TeleportAbility(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
          {
               Debug.Log("Teleport Input is working");
               player.TeleportInput();
          }
     }
}
