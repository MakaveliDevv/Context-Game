using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
     private PlayerInput playerInput;
     private Mover mover;

     private void Awake() 
     {
          // PLAYER INPUT COMPONENT
          playerInput = GetComponent<PlayerInput>();

          // FIND OBJECTS WITH MOVER SCRIPT
          var movers = Object.FindObjectsOfType<Mover>();

          // INITIALIZE PLAYER INDEX FROM PLAYER INPUT
          var index = playerInput.playerIndex;

          // GET THE PLAYER INDEX FROM THE MOVER SCRIPT AND INITIALIZE IT TO THE INDEX FROM PLAYER INPUT
          // EACH PLAYER WITH A MOVER SCRIPT HAS AN INDEX
          mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
          Debug.Log(mover.GetPlayerIndex());

     }


     public void OnMove(InputAction.CallbackContext ctx) 
     {
          mover.SetInputVector(ctx.ReadValue<Vector2>());
     }

     public void JumpInput(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
          {
               mover.Jump();
               Debug.Log("We can jump");
          }
     }

     public void AbilityInput(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
          {
               mover.UseAbility();
          }
     }

     public void TeleportInput(InputAction.CallbackContext ctx) 
     {
          if(ctx.performed) 
          {
               Debug.Log("Teleport Input is working");
               mover.UseTeleport();
          }
     }


//      protected IEnumerator Scaling(GameObject _scaleObj, Vector3 _targetDirection)
//     {
//         while (true) // Continuously scale
//         {
//             Vector3 initialScale = new(_scaleObj.transform.localScale.x, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);
//             Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
//             _scaleObj.transform.localScale = targetScale;
//             isExpanding = true;
//             // Debug.Log("We are scaling!");
            
//             _detectPoint = detectObj.GetComponent<DetectionPoint>();

//             // // Check for connect points while scaling
//             if (_detectPoint.PointDetected())
//             {
//                 // Debug.Log("Point detected!, calling from controller script");
                
//                 // Stop scaling
//                 FreezeScaling(_scaleObj, _scaleObj.transform.localScale);
//             } 
//             yield return null;
//         }
//     }


//       public bool ScaleInputt(GameObject _objectToScale, Vector2 _direction) 
//     {
//         if(_instantiateObj != null && !_player.isMoving) 
//         {
//             if (coroutine != null)
//                 StopCoroutine(coroutine);

//             if(!stopScalingCuzEndPointReached) 
//             {
//                 isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
//                 coroutine = StartCoroutine(Scaling(_objectToScale, _direction));
//             } 

//             return true;
//         }

//         return false;
//     }

//      public void ScaleTowardsPointArtist(InputAction.CallbackContext context) 
//     {
//         if (context.performed && objectCreated && extendPoint1 != null && extendPoint2 != null && _playerManag.playerType == PlayerManager.PlayerType.ARTIST) 
//         {
//             startScalingArtist = true;
//             Debug.Log("Artist: " + context.performed + " Scale forward");            
//             // Read button value (1 if pressed, 0 if released)
//             float buttonValue = context.ReadValue<float>();
//             // Check if button is pressed
//             if (buttonValue > 0)
//             {
//                 ScaleInputt(extendPoint1.gameObject, Vector2.right);
//             }
//         }
//     }

}
