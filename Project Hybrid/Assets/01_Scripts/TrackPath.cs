using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackPath {
    public List<Transform> pathPoints = new List<Transform>();
    public SwitchTrack switchTrack;
}