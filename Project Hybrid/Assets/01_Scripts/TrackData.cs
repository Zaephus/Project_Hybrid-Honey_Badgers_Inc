using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackData {

    public int ID;
    public Vector3 position;
    public float rotation;
    public int trackType;
    public int previousTrackID;
    public int nextTrackID;

}

[System.Serializable]
public class SwitchTrackData : TrackData {

    public int switchState;
    public int nextTrackOneID;
    public int nextTrackTwoID;
    public int inputType;

}