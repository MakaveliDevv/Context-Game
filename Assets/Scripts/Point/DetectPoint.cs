using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPoint : Point
{
    Vector2 previousPosition;

#pragma warning disable IDE0052 // Remove unread private members
    [SerializeField] private bool isMoving;
#pragma warning restore IDE0052 // Remove unread private members

    [SerializeField] private float radius = 1f;

#pragma warning disable IDE0052 // Remove unread private members
    private Coroutine movementCoroutine;
#pragma warning restore IDE0052 // Remove unread private members


    void Start() 
    {
        previousPosition = transform.position;
    }


    void Update()
    {
        CheckForCollision();
    }

    private bool CheckForCollision() 
    {
        movementCoroutine = StartCoroutine(CheckMovement());

        if (isMoving)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                // Check of the collided point has the same tag as the this points tag
                if (collider.TryGetComponent<ConnectPoint>(out var point) && point.type == Type.CONNECT_POINT)
                {
                    // Freeze object
                    InputController inputContr = GetComponentInParent<InputController>();
                    inputContr.Freeze();
                }
            }
        }

        return false;
    }


    IEnumerator CheckMovement()
    {
        // Check also only if its extending
        InputController inputContr = GetComponentInParent<InputController>();

        while (inputContr.isExtending)
        {
            yield return new WaitForSeconds(0.1f); // Adjust the interval based on your needs

            Vector2 currentPosition = transform.position;

            if (currentPosition != previousPosition)
            {
                isMoving = true;
                previousPosition = currentPosition;
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
