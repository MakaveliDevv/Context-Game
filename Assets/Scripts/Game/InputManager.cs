// using UnityEngine;
// using System;

// public class InputManager : MonoBehaviour
// {
//     public static InputManager Instance;

//     public event Action<int, Vector2> MovementInputEvent;
//     public event Action<int> JumpInputEvent;
//     public event Action<int> TransformAbilityEvent;
//     public event Action<int> TransformBackAbilityEvent;
//     public event Action<int> ExtendAbilityEvent;
//     public event Action<int> ExtendAbilityPressEvent;
//     public event Action<int> RetractInputEvent;
//     public event Action<int> TeleportAbilityEvent;
//     public event Action<int> RotatePositiveEvent;
//     public event Action<int> RotateNegativeEvent;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Keep this object across scenes
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void InvokeMovementInput(int playerId, Vector2 direction)
//     {
//         MovementInputEvent?.Invoke(playerId, direction);
//     }

//     public void InvokeJumpInput(int playerId)
//     {
//         JumpInputEvent?.Invoke(playerId);
//     }

//     public void InvokeTransformAbility(int playerId)
//     {
//         TransformAbilityEvent?.Invoke(playerId);
//     }

//     public void InvokeTransformBackAbility(int playerId)
//     {
//         TransformBackAbilityEvent?.Invoke(playerId);
//     }

//     public void InvokeExtendAbility(int playerId)
//     {
//         ExtendAbilityEvent?.Invoke(playerId);
//     }

//     public void InvokeExtendAbilityPress(int playerId)
//     {
//         ExtendAbilityPressEvent?.Invoke(playerId);
//     }

//     public void InvokeRetractInput(int playerId)
//     {
//         RetractInputEvent?.Invoke(playerId);
//     }

//     public void InvokeTeleportAbility(int playerId)
//     {
//         TeleportAbilityEvent?.Invoke(playerId);
//     }

//     public void InvokeRotatePositive(int playerId)
//     {
//         RotatePositiveEvent?.Invoke(playerId);
//     }

//     public void InvokeRotateNegative(int playerId)
//     {
//         RotateNegativeEvent?.Invoke(playerId);
//     }
// }
