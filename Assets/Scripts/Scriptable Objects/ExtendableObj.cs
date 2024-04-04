using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtendableObj_", menuName = "Scriptables/ExtendableObj")]
public class ExtendableObj : ScriptableObject
{
    public GameObject extendableObject;
    public GameObject detectPoint;
    public Vector3 initialScale = new(0, 0, 0);
}
