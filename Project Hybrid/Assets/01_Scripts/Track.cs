using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

    private enum TrackType {
        Straight_Horizontal = 0,
        Straight_Vertical = 1,
        T_Switch = 2
    };

    [SerializeField]
    private TrackType type;

    [SerializeField]
    private List<Material> mats = new List<Material>();

    public IEnumerator DestroySelf() {

        yield return new WaitForEndOfFrame();
        DestroyImmediate(this.gameObject);//
    }

    private void OnValidate() {
        switch(type) {

            case TrackType.Straight_Horizontal:
                GetComponent<MeshRenderer>().material = mats[0];
                break;

            case TrackType.Straight_Vertical:
                GetComponent<MeshRenderer>().material = mats[1];
                break;

            case TrackType.T_Switch:
                GetComponent<MeshRenderer>().material = mats[2];
                break;

        }
    }

}