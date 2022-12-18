using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum TrackType {
        Straight = 0,
        Corner_Left = 1,
        Corner_Right = 2,
        Switch_StraightLeft = 3,
        Switch_StraightRight = 4,
        Switch_LeftRight = 5
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

    [SerializeField] private GameObject[] trackTypes;
    [SerializeField] private GameObject child;
    public BaseTrack track;

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