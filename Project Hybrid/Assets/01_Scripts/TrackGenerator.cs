using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrackGenerator : MonoBehaviour {

    [HideInInspector]
    public List<TrackPath> paths = new List<TrackPath>();
    [HideInInspector]
    public BaseTrack startTrack;

    [Range(4, 40)]
    public int amountX = 15;
    [Range(4, 40)]
    public int amountY = 15;

    public GameObject trackPrefab;

    [HideInInspector]
    public List<TrackRow> rows = new List<TrackRow>();

    private void Start() {
        FillPathList();
        SetStartTrack();
    }

    public void FillPathList() {

        paths.Clear();

        for(int x = 0; x < rows.Count; x++) {
            for(int y = 0; y < rows[x].tracks.Count; y++) {

                if(rows[x].tracks[y].type == 0) {
                    continue;
                }

                if(rows[x].tracks[y].type >= TrackType.Switch_StraightLeft) {
                    SwitchTrack switchTrack = rows[x].tracks[y].track as SwitchTrack;
                    paths.Add(switchTrack.pathOne);
                    switchTrack.pathOne.switchTrack = switchTrack;
                    paths.Add(switchTrack.pathTwo);
                    switchTrack.pathTwo.switchTrack = switchTrack;
                    continue;
                }

                paths.Add(rows[x].tracks[y].track.path);

            }
        }

    }

    public void SetStartTrack() {
        
        for(int x = 0; x < rows.Count; x++) {
            for(int y = 0; y < rows[x].tracks.Count; y++) {

                if(rows[x].tracks[y].type == TrackType.StartPoint) {
                    startTrack = rows[x].tracks[y].track;
                    break;
                }
            }
        }

    }

    public void ResetTracks() {
        for(int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
            rows.Clear();
        }
    }

}