using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum TrackType {
    None = 0,
    Straight = 1,
    Corner = 2,
    StartPoint = 3,
    EndPoint = 4,
    Tunnel = 5,
    Switch_StraightLeft = 6, // Only switch types should be numbered higher than this
    Switch_StraightRight = 7,
    Switch_LeftRight = 8
};

public enum Rotation
{
    None = 0,
    Quarter = 90,
    Half = 180,
    ThreeQuarters = 270
};

[SelectionBase]
public class TrackSelector : MonoBehaviour {

    public TrackType type = 0;
    public Rotation rotation = 0;

    public BaseTrack track;

    public TrackGenerator generator;
    public GameObject[] trackTypes;
    public GameObject child;

    public void Inititialize(TrackGenerator _generator) {
        generator = _generator;
        child = Instantiate(trackTypes[0], transform.position, Quaternion.identity, transform);
        track = child.GetComponent<BaseTrack>();
    }

    public IEnumerator DestroySelf() {

        yield return new WaitForEndOfFrame();
        DestroyImmediate(this.gameObject);
    }

    public void RotateChild(int rotation)
    {
        child.transform.eulerAngles = new Vector3(0,-rotation,0);
    }

}