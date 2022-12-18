using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum TrackType {
        None = 0,
        Straight = 1,
        Corner_Left = 2,
        Corner_Right = 3,
        Switch_StraightLeft = 4, // Only switch types should be numbered above this
        Switch_StraightRight = 5,
        Switch_LeftRight = 6
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
    [SerializeField] private GameObject[] trackTypes;
    [SerializeField] private GameObject child;

    public void Inititialize(TrackGenerator _generator) {
        generator = _generator;
        SwitchChild((int)type);
    }

    public IEnumerator DestroySelf() {

        yield return new WaitForEndOfFrame();
        DestroyImmediate(this.gameObject);
    }

    public void SwitchChild(int index)
    {
        if (Application.isEditor && child != null)
        {
            DestroyImmediate(child);
        }
        child = Instantiate(trackTypes[index], transform);
        track = child.GetComponent<BaseTrack>();
    }

    public void RotateChild(int rotation)
    {
        child.transform.eulerAngles = new Vector3(0,-rotation,0);
    }

}