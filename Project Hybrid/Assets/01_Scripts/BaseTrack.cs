using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrack : MonoBehaviour
{
    //public zodat de trein m kan vinden
    public Transform[] pathPoints;
    public BaseTrack nextTrack;
    [SerializeField] private Color start;
    [SerializeField] private Color middle;
    [SerializeField] private Color end;

    private void OnDrawGizmos()
    {
        for(int i =0; i < pathPoints.Length;)
        {
            if (i == 0) Gizmos.color = start;
            else if (i == pathPoints.Length - 1) Gizmos.color = end;
            else Gizmos.color = middle;

            Gizmos.DrawSphere(pathPoints[i].position, 0.1f);
            i++;
        }
    }
}
