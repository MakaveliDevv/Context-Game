using UnityEngine;

public class InputController : Controller
{
    void Update() 
    {
        if(scriptableObj.detectPoint != null && collapsePoint != null) 
            detectPoint.transform.position = collapsePoint.transform.position - detectPointOffset;
    }

    public void Extend() 
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        CreateObject(scriptableObj);
        coroutine = StartCoroutine(ExtendObject(scriptableObj, extendPoint));
    }

    public void Retract() 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        freeze = false;
        coroutine = StartCoroutine(RetractObject(scriptableObj, extendPoint));
    }

    public void Collapse() 
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CollapseObject(scriptableObj, collapsePoint));
    }

    public void Teleport() 
    {
        StartCoroutine(CollapseObject(scriptableObj, collapsePoint));
    }
}