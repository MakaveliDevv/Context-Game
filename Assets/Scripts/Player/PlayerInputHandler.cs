using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
     private PlayerInput playerInput;
     private PlayerController player;
     [SerializeField] private PlayerManager playerManag; 

     private void Awake() 
     {
          // Player input component
          playerInput = GetComponent<PlayerInput>();

          #pragma warning disable CS0618 // Type or member is obsolete
          var players = FindObjectsOfType<PlayerController>();
          playerManag = FindObjectOfType<PlayerManager>();
          #pragma warning restore CS0618 // Type or member is obsolete

          // Initialize player index from the playerinput
          var index = playerInput.playerIndex;

          // Fetch the player index and initialize it to the index from playerinput
          player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
          Debug.Log(player.GetPlayerIndex());
     }

     // Movement input
     public void MovementInput(InputAction.CallbackContext ctx) { player.SetInputVector(ctx.ReadValue<Vector2>()); }

     // Jump input
     public void JumpInput(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
               player.Jump();
     }

     // Designer
     public void TransformAbility(InputAction.CallbackContext ctx)
     {
          if(ctx.performed) 
               player.TransformInput();
     }

     // Designer
     public void TransformBackAbility(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed)
               player.Transform2Input();
     }

     // Extend ability holding
     public void ExtendAbility(InputAction.CallbackContext ctx) 
     {
          if(!player.inputContr.isExtending && ctx.performed) 
               player.ExtendInput();
          
     }

     // Extend ability by pressing
     public void ExtendAbilityPress(InputAction.CallbackContext ctx) 
     {
          if(player.inputContr.objectCreated && playerManag.playerType == PlayerManager.PlayerType.DESIGNER 
          && ctx.performed) 
               player.ExtendInput();
          
          else if(player.inputContr.isRetracting && ctx.performed)
               player.ExtendInput();  
     }

     // Retract Input
     public void RetractInput(InputAction.CallbackContext ctx) 
     {
          if(!player.inputContr.isRetracting && ctx.performed) 
               player.RetractInput();
     }

     // Teleport ability
     public void TeleportAbility(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
               player.TeleportInput(); 
     }

     // Rotate
     public void RotatePositive(InputAction.CallbackContext ctx) 
     {
          player.inputContr.isNegative = false;
          player.inputContr.isPositive = ctx.performed;
          if (ctx.performed && playerManag.playerType == PlayerManager.PlayerType.DESIGNER) 
               player.inputContr.RotateObject(1); // Rotate positively
     }

     public void RotateNegative(InputAction.CallbackContext ctx) 
     {
          player.inputContr.isPositive = false;
          player.inputContr.isNegative = ctx.performed;
          if (ctx.performed && playerManag.playerType == PlayerManager.PlayerType.DESIGNER)
               player.inputContr.RotateObject(-1); // Rotate negatively
     }
}