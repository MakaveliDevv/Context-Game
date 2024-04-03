using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtendableObj_", menuName = "Scriptables/ExtendableObj")]
public class ExtendableObj : ScriptableObject
{
    public GameObject extendableObj, detectObj, startObj;
    public Vector3 initialScale = new(0, 0, 0);
}
