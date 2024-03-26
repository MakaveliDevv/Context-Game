using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class AsYouWish : MonoBehaviour
{
    EdgeCollider2D edgeCollider2D;
    LineRenderer myline;
    // Start is called before the first frame update
    void Start()
    {
        edgeCollider2D = this.GetComponent<EdgeCollider2D>();
        myline = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetEdgeCollider(myline);
        
    }

    private void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new();

        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRenderPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRenderPoint.x, lineRenderPoint.y));
        }

        edgeCollider2D.SetPoints(edges);
    }
}
