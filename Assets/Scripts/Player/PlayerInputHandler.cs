using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
     private PlayerInput playerInput;
     private PlayerController player;
     private PlayerManager[] playerManagers;

     private void Awake() 
     {
          // Player input component
          playerInput = GetComponent<PlayerInput>();

          #pragma warning disable CS0618 // Type or member is obsolete
          var players = FindObjectsOfType<PlayerController>();
          playerManagers = FindObjectsOfType<PlayerManager>();
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
          if( ctx.performed && !player.inputContr.isExtending) 
               player.ExtendInput();
          
     }

     // Extend ability by pressing
     public void ExtendAbilityPress(InputAction.CallbackContext ctx) 
     {
          foreach (var playerManag in playerManagers)
          {
               if (ctx.performed && player.inputContr.objectCreated && !player.inputContr.isExtending
               && playerManag.playerType == PlayerManager.PlayerType.DESIGNER)
                    player.ExtendInput();

               else if(ctx.performed && player.inputContr.isRetracting)
               player.ExtendInput();          
          }       
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

          
          foreach (var playerManag in playerManagers)
          {
               if (ctx.performed && playerManag.playerType == PlayerManager.PlayerType.DESIGNER)
                    player.inputContr.RotateObject(1); // Rotate positively
                    
          }
     }

     public void RotateNegative(InputAction.CallbackContext ctx) 
     {
          player.inputContr.isPositive = false;
          player.inputContr.isNegative = ctx.performed;

          foreach (var playerManag in playerManagers)
          {
               if (ctx.performed && playerManag.playerType == PlayerManager.PlayerType.DESIGNER)
                    player.inputContr.RotateObject(-1); // Rotate negatively
                    
          }

     }
}


// using UnityEngine;
// using UnityEngine.InputSystem;
// using System.Linq;
// using System.Collections.Generic;

// public class PlayerInputHandler : MonoBehaviour
// {
//      private Dictionary<int, PlayerController> playersByIndex = new Dictionary<int, PlayerController>();
//      private Dictionary<int, PlayerManager.PlayerType> indexToPlayerType = new Dictionary<int, PlayerManager.PlayerType>()
//      {
//           {0, PlayerManager.PlayerType.ARTIST},
//           {1, PlayerManager.PlayerType.DESIGNER},
//           {2, PlayerManager.PlayerType.DEVELOPER}
//      };

//     // Reference to the PlayerManager
//     public PlayerManager playerManager;

//     private void OnPlayerJoined(PlayerInput playerInput)
//     {
//         // Initialize player index from the playerinput
//         var index = playerInput.playerIndex;

//         // Find the corresponding player type for the index
//         if (indexToPlayerType.TryGetValue(index, out var playerType))
//         {
//           // Instantiate the correct player manager based on the player type
//           var playerManager = gameObject.AddComponent<PlayerManager>();
//           playerManager.playerType = playerType;

//           // Debug log to confirm the player type
//           Debug.Log($"Player {index} is a {playerType}");

//           // Find the player controller based on the player type
//           var player = playersByIndex.ContainsKey(index) ? playersByIndex[index] : null;
//           if (player == null)
//           {
//                Debug.LogError($"No player controller found for index {index}");
//                return;
//           }

//           // Assign the player controller to the player manager
//           playerManager.playerController = player;

//           // Set the reference to the PlayerManager
//           this.playerManager = playerManager;

//           // Do any other initialization specific to the player type here if needed
//         }
//         else
//         {
//             Debug.LogError($"No player type found for index {index}");
//         }
//     }

//      private void OnEnable()
//      {
//           // Find all player controllers and store them in the playersByIndex dictionary
//           var players = FindObjectsOfType<PlayerController>();
//           foreach (var player in players)
//           {
//                playersByIndex[player.GetPlayerIndex()] = player;
//           }

//           // Subscribe to the player joined event
//           PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
//      }

//      private void OnDisable()
//      {
//           // Unsubscribe from the player joined event
//           PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
//      }

//      // Movement input
//      public void MovementInput(InputAction.CallbackContext ctx)
//      {
//           var playerIndex = ctx.control.device.deviceId;
//           if (playersByIndex.TryGetValue(playerIndex, out var player))
//           {
//                player.SetInputVector(ctx.ReadValue<Vector2>());
//           }
//      }

//      // Jump input
//      public void JumpInput(InputAction.CallbackContext ctx)
//      {
//           var playerIndex = ctx.control.device.deviceId;
//           if (playersByIndex.TryGetValue(playerIndex, out var player) && ctx.performed)
//           {
//                player.Jump();
//           }
//      }

//      // Designer
//      public void TransformAbility(InputAction.CallbackContext ctx)
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(ctx.performed) 
//                player.TransformInput();
//      }
//      }

//      // Designer
//      public void TransformAbility(InputAction.CallbackContext ctx, PlayerManager playerManager)
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(ctx.performed) 
//                player.TransformInput();
//      }
//      }

//      // Designer
//      public void TransformBackAbility(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(ctx.performed)
//                player.Transform2Input();
//      }
//      }

//      // Extend ability holding
//      public void ExtendAbility(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(!player.inputContr.isExtending && ctx.performed) 
//                player.ExtendInput();
//      }
//      }

//      // Extend ability by pressing
//      public void ExtendAbilityPress(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(player.inputContr.objectCreated && ctx.performed) 
//                player.ExtendInput();
//           else if(player.inputContr.isRetracting && ctx.performed)
//                player.ExtendInput();  
//      }
//      }

//      // Retract Input
//      public void RetractInput(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(!player.inputContr.isRetracting && ctx.performed) 
//                player.RetractInput();
//      }
//      }

//      // Teleport ability
//      public void TeleportAbility(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {
//      var playerIndex = ctx.control.device.deviceId;
//      if (playersByIndex.TryGetValue(playerIndex, out var player))
//      {
//           if(ctx.performed) 
//                player.TeleportInput(); 
//      }
//      }

//      // Rotate
//      public void RotatePositive(InputAction.CallbackContext ctx, PlayerManager playerManager) 
//      {    
//           var playerIndex = ctx.control.device.deviceId;
//           if(playerManager && playerManager.playerType == PlayerManager.PlayerType.DESIGNER && ctx.performed) 
//           {
//                if (playersByIndex.TryGetValue(playerIndex, out var player))
//                {
//                     Debug.Log("Positive Input");
//                     player.inputContr.RotateObject(1);
//                }
//           }
//      }

//      public void RotateNegative(InputAction.CallbackContext ctx, PlayerManager playerManager)
//      {
//           var playerIndex = ctx.control.device.deviceId;
//           if(playerManager && playerManager.playerType == PlayerManager.PlayerType.DESIGNER && ctx.performed) 
//           {
//                if (playersByIndex.TryGetValue(playerIndex, out var player))
//                {
//                     Debug.Log("Negative Input");
//                     player.inputContr.RotateObject(-1);
//                }
//           }
//      }
// }

