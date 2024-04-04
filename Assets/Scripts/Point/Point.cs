using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum Type 
    {
        PIVOT_POINT,
        CONNECT_POINT,
        DETECT_POINT
    }

    public Type type;
    public string NameTag;

    private void Update() 
    {
        gameObject.tag = NameTag;
    }
}
