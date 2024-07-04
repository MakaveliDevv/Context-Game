using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InputActionHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Dictionary<int, PlayerController> playersByIndex = new Dictionary<int, PlayerController>();

    private void Awake()
    {
        // Player input component
        playerInput = GetComponent<PlayerInput>();

        // Initialize the PlayerManager dictionary on Awake
        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            playersByIndex[player.GetPlayerIndex()] = player;
        }
    }

    // Ensure playersByIndex is updated whenever a new scene is loaded
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the dictionary with new PlayerControllers in the scene
        playersByIndex.Clear();
        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            playersByIndex[player.GetPlayerIndex()] = player;
        }
    }

    // Movement input
    public void MovementInput(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player))
        {
            player.SetInputVector(ctx.ReadValue<Vector2>());
        }
    }

    // Jump input
    public void JumpInput(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && ctx.performed)
        {
            player.Jump();
        }
    }

    // Designer
    public void TransformAbility(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && ctx.performed)
        {
            player.TransformInput();
        }
    }

    // Designer
    public void TransformBackAbility(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && ctx.performed)
        {
            player.Transform2Input();
        }
    }

    // Extend ability holding
    public void ExtendAbility(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && ctx.performed && !player.inputContr.isExtending)
        {
            player.ExtendInput();
        }
    }

    // Extend ability by pressing
    public void ExtendAbilityPress(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player))
        {
            if (ctx.performed && player.inputContr.objectCreated && !player.inputContr.isExtending)
            {
                player.ExtendInput();
            }
            else if (ctx.performed && player.inputContr.isRetracting)
            {
                player.ExtendInput();
            }
        }
    }

    // Retract Input
    public void RetractInput(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && !player.inputContr.isRetracting && ctx.performed)
        {
            player.RetractInput();
        }
    }

    // Teleport ability
    public void TeleportAbility(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player) && ctx.performed)
        {
            player.TeleportInput();
        }
    }

    // Rotate
    public void RotatePositive(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player))
        {
            player.inputContr.isNegative = false;
            player.inputContr.isPositive = ctx.performed;

            if (ctx.performed && player.inputContr.playerManager.playerType == PlayerManager.PlayerType.DESIGNER)
            {
                player.inputContr.RotateObject(1); // Rotate positively
            }
        }
    }

    public void RotateNegative(InputAction.CallbackContext ctx)
    {
        if (playersByIndex.TryGetValue(playerInput.playerIndex, out var player))
        {
            player.inputContr.isPositive = false;
            player.inputContr.isNegative = ctx.performed;

            if (ctx.performed && player.inputContr.playerManager.playerType == PlayerManager.PlayerType.DESIGNER)
            {
                player.inputContr.RotateObject(-1); // Rotate negatively
            }
        }
    }
}
