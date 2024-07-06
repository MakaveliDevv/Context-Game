// using UnityEngine;
// using UnityEngine.InputSystem;
// using System.Collections.Generic;

// public class PlayerInputHandler : MonoBehaviour
// {
//     private Dictionary<int, PlayerController> playersByIndex = new();

//     private void OnEnable()
//     {
//         // Subscribe to input events
//         InputManager.Instance.MovementInputEvent += MovementInput;
//         InputManager.Instance.JumpInputEvent += JumpInput;
//         InputManager.Instance.TransformAbilityEvent += TransformAbility;
//         InputManager.Instance.TransformBackAbilityEvent += TransformBackAbility;
//         InputManager.Instance.ExtendAbilityEvent += ExtendAbility;
//         InputManager.Instance.ExtendAbilityPressEvent += ExtendAbilityPress;
//         InputManager.Instance.RetractInputEvent += RetractInput;
//         InputManager.Instance.TeleportAbilityEvent += TeleportAbility;
//         InputManager.Instance.RotatePositiveEvent += RotatePositive;
//         InputManager.Instance.RotateNegativeEvent += RotateNegative;

//         // Register all existing player controllers
//         RegisterPlayers();
//     }

//     private void OnDisable()
//     {
//         // Unsubscribe from input events
//         InputManager.Instance.MovementInputEvent -= MovementInput;
//         InputManager.Instance.JumpInputEvent -= JumpInput;
//         InputManager.Instance.TransformAbilityEvent -= TransformAbility;
//         InputManager.Instance.TransformBackAbilityEvent -= TransformBackAbility;
//         InputManager.Instance.ExtendAbilityEvent -= ExtendAbility;
//         InputManager.Instance.ExtendAbilityPressEvent -= ExtendAbilityPress;
//         InputManager.Instance.RetractInputEvent -= RetractInput;
//         InputManager.Instance.TeleportAbilityEvent -= TeleportAbility;
//         InputManager.Instance.RotatePositiveEvent -= RotatePositive;
//         InputManager.Instance.RotateNegativeEvent -= RotateNegative;
//     }

//     private void RegisterPlayers()
//     {
//         // Find all existing player controllers and register them
//         var players = FindObjectsOfType<PlayerController>();
//         foreach (var player in players)
//         {
//             playersByIndex[player.GetPlayerIndex()] = player;
//         }
//     }

//     // Input event handlers
//     private void MovementInput(int playerId, Vector2 direction)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.SetInputVector(direction);
//         }
//     }

//     private void JumpInput(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.Jump();
//         }
//     }

//     private void TransformAbility(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.TransformInput();
//         }
//     }

//     private void TransformBackAbility(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.Transform2Input();
//         }
//     }

//     private void ExtendAbility(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.ExtendInput();
//         }
//     }

//     private void ExtendAbilityPress(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.ExtendInput();
//         }
//     }

//     private void RetractInput(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.RetractInput();
//         }
//     }

//     private void TeleportAbility(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.TeleportInput();
//         }
//     }

//     private void RotatePositive(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.inputContr.RotateObject(1);
//         }
//     }

//     private void RotateNegative(int playerId)
//     {
//         if (playersByIndex.TryGetValue(playerId, out var player))
//         {
//             player.inputContr.RotateObject(-1);
//         }
//     }
// }
