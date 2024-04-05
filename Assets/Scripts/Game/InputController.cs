using UnityEngine;

public class InputController : Controller
{
    void Update() 
    {
        if(scriptableObj.detectPoint != null && collapsePoint != null) 
            detectPoint.transform.position = collapsePoint.transform.position - detectPointOffset;
    }

    public void ExtendObj() 
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        // Check if it's not the designer
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType != PlayerManager.PlayerType.DESIGNER) 
        {
            CreateObject(scriptableObj);
            coroutine = StartCoroutine(ExtendObject(scriptableObj, extendPoint));
        }
        else if(TryGetComponent<PlayerManager>(out var designer) && designer.playerType == PlayerManager.PlayerType.DESIGNER)
            coroutine = StartCoroutine(ExtendObject(scriptableObj, extendPoint));

    }

    public void TransformToObject() 
    {
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType == PlayerManager.PlayerType.DESIGNER)
           CreateObject(scriptableObj);
    }

    public void TransformToCharacter() 
    {
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType == PlayerManager.PlayerType.DESIGNER)
            TransformBack();
    }


    // public void ExtendDesignersObj() 
    // {
    //     if(coroutine != null)
    //         StopCoroutine(coroutine);

       
    //     coroutine = StartCoroutine(ExtendObject(scriptableObj, extendPoint));
    // }

    public void RetractObj() 
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