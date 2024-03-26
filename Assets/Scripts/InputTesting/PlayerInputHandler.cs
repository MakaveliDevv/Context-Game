using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
     private PlayerInput playerInput;
     private Mover mover;
     // private Controls controls;

     private void Awake() 
     {
          playerInput = GetComponent<PlayerInput>();
          var movers = Object.FindObjectsOfType<Mover>();
          var index = playerInput.playerIndex;
          mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);

          Debug.Log("Swag");
     }

     public void OnMove(InputAction.CallbackContext ctx) 
     {
          mover.SetInputVector(ctx.ReadValue<Vector2>());
     }
}
