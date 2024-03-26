using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGoDown : MonoBehaviour
{
    public float lerpFactor = 1f; // Adjust this to control the speed of movement
    public float divideFactorForLerpFactor = 1f; // You can adjust this if needed
    private Vector3 targetPosition;
    private float startTime;

    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Set the target position to move down by 5 units
            targetPosition = new(transform.position.x, transform.position.y - 5f, transform.position.z);
            startTime = Time.time; // Record the start time
        }

        // Calculate the fraction of journey completed based on time
        float journeyTime = Time.time - startTime;
        float fracJourney = journeyTime * lerpFactor / divideFactorForLerpFactor;

        // Move towards the target position using Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Mathf.Clamp01(fracJourney));
    }
}
