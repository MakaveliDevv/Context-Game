using UnityEngine;

public class ExtendableObject : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float extendDistance = 2f; // Distance to extend the object
    public float extendSpeed = 5f; // Speed of extension

    private float initialScaleX; // Initial x scale of the object
    private float targetScaleX; // Target x scale when extended

    void Start()
    {
        initialScaleX = transform.localScale.x;
        targetScaleX = initialScaleX;
    }

    void Update()
    {
        // Extending the object towards the direction the player is facing when Extend button is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetScaleX = initialScaleX + extendDistance;
        }

        // Retracting the object back to the player's scale when Retract button is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            targetScaleX = initialScaleX;
        }

        // Change the object's scale towards the target scale
        float newScaleX = Mathf.Lerp(transform.localScale.x, targetScaleX, extendSpeed * Time.deltaTime);
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }
}
