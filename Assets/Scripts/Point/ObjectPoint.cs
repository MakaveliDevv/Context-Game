using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoint : MonoBehaviour
{
    [HideInInspector] public static ObjectPoint instance;
    public enum PointType 
    {
        EXTEND_POINT_1,
        EXTEND_POINT_2,
        TO_EXTEND_BACK_POINT,
        START_POINT,
        END_POINT
    }
    public PointType pointType;
    public string NameTag;

    private void Update() 
    {
        gameObject.tag = NameTag;
    }
}
