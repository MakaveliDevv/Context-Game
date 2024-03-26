// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// { 
//     // private PlayerInput _playerInput;
//     // public Rigidbody2D rb;
//     // public float moveSpeed;
//     // private PlayerInputActions _playerActions;
//     // private PlayerManager pManager;
//     // private Controller controller;
//     public float fallMultiplier;
//     // [HideInInspector] public Vector2 inputDirection;
//     public Transform castPosition;
//     public float radius;
//     public bool playerIsGrounded;
//     private Vector2 vecGravity;
//     // public bool isMoving = false;
//     // public bool playerDetected;
//     public LayerMask layermask;
//     public GameObject playerRenderer;


//     // void Awake() 
//     // {
//     //     pManager = GetComponent<PlayerManager>();
//     // }
//     void 
//     Start() 
//     {
//         vecGravity = new(0f, - Physics2D.gravity.y);
//     }


//     void Update() 
//     { 
//         Collider2D hit = Physics2D.OverlapCircle(castPosition.position, radius, layermask);
//         if(hit != null) 
//         {
//             playerIsGrounded = true;

//         } else 
//         {
//             playerIsGrounded = false;
//         }

//     }


//     public void MovePlayer(InputAction.CallbackContext ctx) 
//     {
//         if(ctx.performed) 
//         {
//             if (!controller.isExpanding && !controller.isExpandingBack 
//             && !controller.stopScalingCuzEndPointReached && playerIsGrounded) 
//             {
//                 inputDirection = ctx.ReadValue<Vector2>();
                
//                 // Check if the player can move along the Y-axis
//                 if (!playerDetected) 
//                 {
//                     inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder
//                 }

//                 // Apply movement
//                 rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);

//             }

//         }
//     }
    
// }
