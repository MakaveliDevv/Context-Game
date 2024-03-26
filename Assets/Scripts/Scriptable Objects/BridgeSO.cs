using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeParameters_", menuName = "Bridge/BridgeSO")]
public class BridgeSO : ScriptableObject
{
//     public GameObject objectPrefab;
//     public Vector3 initialScale = new(0, 0, 0);
// // Assuming initialRotation is a Quaternion
//     private Quaternion initialRotation = Quaternion.identity;
//     public Quaternion zRotation = Quaternion.Euler(0, 0, 90);

//     Quaternion finalRotation = initialRotation * zRotation;
//     public float height = .2f, depth = 1f;

    public GameObject objectPrefab;
    public Vector3 initialScale = new Vector3(0, 0, 0);
    private Quaternion initialRotation = Quaternion.identity;
    public Quaternion zRotation = Quaternion.Euler(0, 0, 90f);
    public float height = 0.2f, depth = 1f;

    [HideInInspector] public Quaternion finalRotation;

    // Constructor
    public BridgeSO()
    {
        // Combine initial rotation with the Z-axis rotation
        finalRotation = initialRotation * zRotation;
    }
}
