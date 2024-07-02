using Unity.Mathematics;
using UnityEngine;

public class InputController : Controller
{
    private bool afterReachingConnectPoint;
    void Update() 
    {
        if(scriptableObj.detectPoint != null && collapsePoint != null) 
            detectPoint.transform.position = collapsePoint.transform.position - detectPointOffset;


        // Check if the extendable object is active in the hierarchy
        if (newGameObject != null && newGameObject.activeInHierarchy)
        {
            // Fetch the spriter renderer game object
            var script = newGameObject.GetComponent<Swag>();
            var spriteRenderer = script.spriteRenderer;

            Debug.Log("Object active in scene");

            // Check if the connect point is reached
            if (reached_connectPoint) 
            {
                Debug.Log("Connect point reached");
                
                // If the BoxCollider2D component exists, enable it
                if (spriteRenderer.TryGetComponent<BoxCollider2D>(out var boxCollider))
                {
                    Debug.Log("Enabling BoxCollider2D");
                    boxCollider.enabled = true;
                    afterReachingConnectPoint = true;
                }
                else 
                {
                    Debug.LogWarning("BoxCollider2D not found on the instantiated object.");
                }
            }
            else if(spriteRenderer.TryGetComponent<BoxCollider2D>(out var boxCollider))
            {                   
                boxCollider.enabled = false;
            }

            if(afterReachingConnectPoint && isRetracting)
            {
                // If the BoxCollider2D component exists, enable it
                if (spriteRenderer.TryGetComponent<BoxCollider2D>(out var boxCollider))
                {
                    Debug.Log("Enabling BoxCollider2D");
                    boxCollider.enabled = true;
                }
                else 
                {
                    Debug.LogWarning("BoxCollider2D not found on the instantiated object.");
                } 
            }
        }
    }

    public void ExtendObj() 
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        // Check if it's not the designer
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType != PlayerManager.PlayerType.DESIGNER) 
        {
            CreateObject(scriptableObj);
            coroutine = StartCoroutine(ExtendObject(extendPoint));
        }
        else if(TryGetComponent<PlayerManager>(out var designer) && designer.playerType == PlayerManager.PlayerType.DESIGNER)
            coroutine = StartCoroutine(ExtendObject(extendPoint));

    }

    // Is for the designer
    public void TransformToObject() 
    {
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType == PlayerManager.PlayerType.DESIGNER)
           CreateObject(scriptableObj);
    }

    // Is for the designer
    public void TransformToCharacter() 
    {
        if(TryGetComponent<PlayerManager>(out var player) && player.playerType == PlayerManager.PlayerType.DESIGNER)
            TransformBack();
    }


    public void RetractObj() 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        freeze = false;
        coroutine = StartCoroutine(RetractObject(extendPoint));
    }

    public void Collapse() 
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CollapseObject(collapsePoint));
    }

    public void Teleport() { StartCoroutine(CollapseObject(collapsePoint)); }
}