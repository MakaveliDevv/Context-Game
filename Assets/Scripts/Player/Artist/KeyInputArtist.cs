using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInputArtist : InputController
{
    private PlayerInputActions _playerActions;
    private bool startScalingArtist;

    void Awake() 
    {
        _playerActions = new();
        _playerActions.PlayerArtist.Enable();
    }

    void Update() 
    {
        if(startPointObj != null && startPoint != null 
        && detectObj != null && endPoint != null) 
        {
            startPointObj.transform.position = startPoint.transform.position;
            detectObj.transform.position = endPoint.transform.position;
        }
    }
    
    public void ScaleTowardsPointArtist(InputAction.CallbackContext context) 
    {
        if (context.performed && objectCreated && extendPoint1 != null && extendPoint2 != null && _playerManag.playerType == PlayerManager.PlayerType.ARTIST) 
        {
            startScalingArtist = true;
            Debug.Log("Artist: " + context.performed + " Scale forward");            
            // Read button value (1 if pressed, 0 if released)
            float buttonValue = context.ReadValue<float>();
            // Check if button is pressed
            if (buttonValue > 0)
            {
                ScaleInputt(extendPoint1.gameObject, Vector2.right);
            }
        }
    }
    
    public void TeleportToPointArtist(InputAction.CallbackContext context) 
    {
        if (toExtandBack != null && context.performed && _playerManag.playerType == PlayerManager.PlayerType.ARTIST) 
        {
            startScalingArtist = false;
            Debug.Log("Artist: " + context.performed + " Teleport to point");
            // Read button value (1 if pressed, 0 if released)
            float buttonValue = context.ReadValue<float>();
            // Check if button is pressed
            if (buttonValue > 0)
            {
                TeleportInput(extendPoint2.gameObject);
            }
        }
    }


    // public void ScaleTowardsPointArtist(InputAction.CallbackContext context) 
    // {
    //     if (context.performed && objectCreated && extendPoint1 != null && extendPoint2 != null && _playerManag.playerType == PlayerManager.PlayerType.ARTIST) 
    //     {
    //         startScalingArtist = true;
    //         Debug.Log("Artist: " + context.performed + "Scale forward");            
    //         ScaleInputt(extendPoint1.gameObject, Vector2.right);
    //     }
    // }

    // public void TeleportToPointArtist(InputAction.CallbackContext context) 
    // {
    //     if (toExtandBack != null && context.performed && _playerManag.playerType == PlayerManager.PlayerType.ARTIST) 
    //     {
    //         startScalingArtist = false;
    //         Debug.Log(context.performed);   
    //         Debug.Log("Artist: " + context.performed + "Teleport to point");
    //         TeleportInput(extendPoint2.gameObject);
    //     }
    // }
}
