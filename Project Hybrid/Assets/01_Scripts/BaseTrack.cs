using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrack : MonoBehaviour
{
    public TrackPath path;
    [SerializeField] private Color start;
    [SerializeField] private Color middle;
    [SerializeField] private Color end;

    private void OnDrawGizmos()
    {
        for(int i =0; i < path.pathPoints.Count;)
        {
            if (i == 0) Gizmos.color = start;
            else if (i == path.pathPoints.Count - 1) Gizmos.color = end;
            else Gizmos.color = middle;

            Gizmos.DrawSphere(path.pathPoints[i].position, 0.1f);
            i++;
        }
    }
}
