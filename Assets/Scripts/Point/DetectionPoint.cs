using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionPoint : MonoBehaviour
{
    public static DetectionPoint instance;
    // public Controller controller_Script;
    
    // #region Singleton
    // void Awake() 
    // {
    //     if(instance != null) 
    //         Destroy(this);
        
    //     else
    //         instance = this;
    // }
    // #endregion

    private GameManager gameManager;
    [SerializeField] private float detectRadius;
    Vector3 previousPosition;
    private bool isMoving = false;

    void Start() 
    {
        gameManager = GameManager.instance;
        previousPosition = transform.position;
        // controller_Script = GetComponentInParent<Controller>();
        // transform.position = point.transform.position;

        // // Create a new GameObject for visualization
        // GameObject sphereVisualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        // // Remove the collider component from the visualization sphere
        // Destroy(sphereVisualizer.GetComponent<Collider>());

        // // Set the position of the visualization sphere to match the transform position
        // sphereVisualizer.transform.position = transform.position;

        // // Scale the visualization sphere based on the detection radius
        // sphereVisualizer.transform.localScale = new Vector3(detectRadius * 2f, detectRadius * 2f, detectRadius * 2f);
        
        // // Set the visualization sphere as a child of the current GameObject
        // sphereVisualizer.transform.parent = transform;
        
    }


    public bool Moving() 
    {
        Vector3 currenPosition = transform.position;
        if(currenPosition != previousPosition) 
        {
            previousPosition = currenPosition;
            isMoving = true;
            return true;
        
        } else 
        {
            isMoving = false;
        }
        
        // Debug.Log(previousPosition);
        return false;
    }

    public bool PointDetected() 
    {
        gameManager.connectPointList.Clear();
        Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);

        if (hitPoint != null)
        {
            Point point = hitPoint.GetComponent<Point>();

            if (point != null)
            {
                gameManager.connectPointList.Add(point);
                Debug.Log("Connect point detected!");
                return true;
            }
        }

        return false;
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
