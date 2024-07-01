using UnityEngine;
using System.Collections;

public class Delayspawn : MonoBehaviour
{
    [SerializeField] private float delay = 5f; // Time in seconds to wait before activation
    [SerializeField] private GameObject targetObject; // The GameObject to activate

    void Start()
    {
        // Start the coroutine to activate the GameObject after a delay
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private IEnumerator ActivateObjectAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        
        // Activate the target GameObject
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
