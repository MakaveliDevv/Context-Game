using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum PointName 
    {
        BRIDGE_POINT, 
        LADDER_POINT, 
        GRAPPLER_POINT,
        PIVOT_POINT,
    }

    public PointName pointName;

    Vector3 previousPosition;
    Vector3 currentPosition;
    public bool isMoving;

    
    public bool Moving(Transform _point) 
    {
        // Check if this is the first time moving
        if (previousPosition == Vector3.zero)
        {
            previousPosition = _point.transform.position;
            return false; // No movement yet
        }
        
        Vector3 currentPosition = _point.transform.position;
        
        // Compare current position with the previous position
        if (currentPosition != previousPosition) 
        {
            previousPosition = currentPosition;
            isMoving = true;
            return true; // Object is moving
        } 
        else 
        {
            isMoving = false;
            return false; // Object is not moving
        }
    }
}
