using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private Vector2 inputDirection = Vector2.zero;   
    private Vector2 inputVector = Vector2.zero;
    private Rigidbody2D rb;

    [SerializeField] private int playerIndex = 0;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int GetPlayerIndex() 
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction) 
    {
        inputVector = direction;
    }

    void Update() 
    {
        inputDirection = new(inputVector.x, 0);
        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection *= moveSpeed;

        // Apply movement
        rb.velocity = new Vector2(inputDirection.x, rb.velocity.y);
    }
}
