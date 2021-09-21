using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectTargetArea : MonoBehaviour
{
    [SerializeField] private Transform detectionCenterPoint = null; // Marcamos el Centro en torno al que va a Detectar
    [SerializeField] private Vector2 detectionArea = new Vector2(2, 2);
    [SerializeField] private LayerMask targetsLayerMask = 8; // Que LayerMasks tiene que Detectar
    private Collider2D target = null;

    public PlayerController Player { get; private set; }

    public void Start()
    {
        if (detectionCenterPoint == null)
            detectionCenterPoint = transform;
    }

    public void CheckArea()
    {
        target = Physics2D.OverlapBox(detectionCenterPoint.position, detectionArea, 0f, targetsLayerMask);
        if (target != null) 
            Player = target.gameObject.GetComponent<PlayerController>();
        else
            Player = null;
    }

    public bool DetectTarget()
    {
        return Player != null ? true : false;
    }
    private void OnDrawGizmosSelected()
    {
        if (detectionCenterPoint != null)
            Gizmos.DrawWireCube(detectionCenterPoint.position, detectionArea);
    }
}