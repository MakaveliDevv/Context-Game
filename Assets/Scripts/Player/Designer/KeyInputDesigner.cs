using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeyInputDesigner : InputController
{
    private PlayerInputActions _playerActions;
    public bool startScalingDesigner;

    void Awake() 
    {
        _playerActions = new();
        _playerActions.PlayerDesigner.Enable();
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
    public void ScaleTowardsPointDesigner(InputAction.CallbackContext context) 
    {
        if (context.performed && objectCreated && extendPoint1 != null && extendPoint2 != null && _playerManag.playerType == PlayerManager.PlayerType.DESIGNER) 
        {
            startScalingDesigner = true;
            Debug.Log("Designer: " + context.performed + " Scale forward");            
            // Read button value (1 if pressed, 0 if released)
            float buttonValue = context.ReadValue<float>();
            // Check if button is pressed
            if (buttonValue > 0)
            {
                ScaleInputt(extendPoint1.gameObject, Vector2.right);
            }
        }
    }
    
    public void TeleportToPointDesigner(InputAction.CallbackContext context) 
    {
        if (toExtandBack != null && context.performed && _playerManag.playerType == PlayerManager.PlayerType.DESIGNER) 
        {
            startScalingDesigner = false;
            Debug.Log("Designer: " + context.performed + " Teleport to point");
            // Read button value (1 if pressed, 0 if released)
            float buttonValue = context.ReadValue<float>();
            // Check if button is pressed
            if (buttonValue > 0)
            {
                TeleportInput(extendPoint2.gameObject);
            }
        }
    }

    // public void ScaleTowardsPointDesigner(InputAction.CallbackContext context) 
    // {
    //     if (context.performed && objectCreated && extendPoint1 != null && extendPoint2 != null && _playerManag.playerType == PlayerManager.PlayerType.DESIGNER) 
    //     {
    //         startScalingDesigner = true;
    //         Debug.Log("Designer: " + context.performed + "Scale forward");            
    //         ScaleInputt(extendPoint1.gameObject, Vector2.right);
    //     }
    // }


    // public void TeleportToPointDesigner(InputAction.CallbackContext context) 
    // {
    //     if (toExtandBack != null && context.performed && _playerManag.playerType == PlayerManager.PlayerType.DESIGNER) 
    //     {
    //         startScalingDesigner = false;
    //         Debug.Log(context.performed);   
    //         Debug.Log("Designer: " + context.performed + "Teleport to point");
    //         TeleportInput(extendPoint2.gameObject);
    //     }
    // }
}
