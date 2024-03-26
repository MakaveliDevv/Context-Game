// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// // using UnityEngine.InputSystem;

// public class PlayerControllerr : MonoBehaviour
// {    
//     // Player
//     public bool isMoving = false;
//     [HideInInspector] public Rigidbody2D rb;
//     public float fallMultiplier;
//     private Vector2 vecGravity;
//     public Transform castPosition;
//     public LayerMask layermask;
//     public float radius;

//     // Movement
//     [SerializeField] private float moveSpeed;
//     [HideInInspector] public Vector2 inputDirection;

//     // Scaling
//     private Controller controller;
//     public GameObject playerRenderer;
//     public bool playerIsGrounded;


//     // Input System Test
//     // private PlayerInput _playerInput;
//     // public bool move;    
//     // public bool ableToJump;
//     // public float jumpForce;
//     // End
    
//     void Awake() 
//     {
//         rb = GetComponent<Rigidbody2D>();
//         controller = GetComponentInChildren<Controller>();
//     }

//     void Start() 
//     {
//         vecGravity = new(0f, - Physics2D.gravity.y);
//     }

//     void Update() 
//     { 
//         inputDirection = new(Input.GetAxisRaw("Horizontal"), 0f);
//         // MovePlayer();
//         // MoveTestPlayer();

//         Collider2D hit = Physics2D.OverlapCircle(castPosition.position, radius, layermask);
//         if(hit != null) 
//         {
//             playerIsGrounded = true;

//         } else 
//         {
//             playerIsGrounded = false;
//         }
//     }

//     // private void MoveTestPlayer() 
//     // {
//     //     if(rb.velocity.y < 0.03f) { rb.velocity -= fallMultiplier * Time.deltaTime * vecGravity; }
//     //     move = false;

//     //     if(inputDirection != new Vector2(0f, 0f) && playerIsGrounded)
//     //     {
//     //         move = true;
//     //         inputDirection = transform.TransformDirection(inputDirection);
//     //         inputDirection *= moveSpeed;
//     //         rb.velocity = new(inputDirection.x, rb.velocity.y);

//     //     } else { move = false; }

//     //     // Jump();
//     // }

//     // public void Jump(InputAction.CallbackContext context) 
//     // {
//     //     Debug.Log(context);
//     //     ableToJump = false;

//     //     if(playerIsGrounded && context.performed) 
//     //     {   
//     //         ableToJump = true;
//     //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
//     //         Debug.Log("Jump" + context.phase);
        
//     //     } else { ableToJump = false; }
//     // }

//     private void MovePlayer() 
//     {
//         isMoving = false;

//         if(inputDirection != new Vector2(0f, 0f) 
//         && !controller.isExpanding 
//         && !controller.isExpandingBack
//         && !controller.stopScalingCuzEndPointReached
//         && playerIsGrounded) 
//         {
//             isMoving = true;

//             inputDirection = transform.TransformDirection(inputDirection);
//             inputDirection *= moveSpeed;
//             rb.velocity = new(inputDirection.x, rb.velocity.y);
        
//         } else 
//         {
//             isMoving = false;            
//         }

//         // Check if player is in the air
//         if(rb.velocity.y < 0.03f) 
//         {
//             rb.velocity -= fallMultiplier * Time.deltaTime * vecGravity; 
//         }        
//     }
// }
