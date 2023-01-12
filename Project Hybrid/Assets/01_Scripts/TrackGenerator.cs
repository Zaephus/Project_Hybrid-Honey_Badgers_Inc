using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrackGenerator : MonoBehaviour {

    [HideInInspector]
    public List<TrackPath> paths = new List<TrackPath>();
    [HideInInspector]
    public BaseTrack startTrack;

    [SerializeField, Range(4, 40)]
    private int amountX = 15;
    [SerializeField, Range(4, 40)]
    private int amountY = 15;

    [SerializeField]
    private GameObject trackPrefab;

    [SerializeField, HideInInspector]
    private List<TrackRow> rows = new List<TrackRow>();

    public void Generate() {

        if(amountX > rows.Count) {
            for(int i = rows.Count; i < amountX; i++) {
                rows.Add(new TrackRow());

                for(int j = 0; j < amountY; j++) {
                    GameObject obj = PrefabUtility.InstantiatePrefab(trackPrefab) as GameObject;
                    obj.transform.SetParent(transform);
                    obj.transform.position = new Vector3(i, 0, j);
                    obj.transform.rotation = Quaternion.identity;
                    TrackSelector selector = obj.GetComponent<TrackSelector>();
                    selector.Inititialize(this);
                    rows[i].tracks.Add(selector);
                }
            }
        }
        else if(amountX < rows.Count) {
            for(int i = amountX; i < rows.Count; i++) {
                for(int j = 0; j < rows[i].tracks.Count; j++) {
                    DestroyImmediate(rows[i].tracks[j].gameObject);
                }
                rows.RemoveAt(i);
            } 
        }

        for(int i = 0; i < rows.Count; i++) {
            if(amountY > rows[i].tracks.Count) {
                for(int j = rows[i].tracks.Count; j < amountY; j++) {
                    GameObject obj = PrefabUtility.InstantiatePrefab(trackPrefab) as GameObject;
                    obj.transform.SetParent(transform);
                    obj.transform.position = new Vector3(i, 0, j);
                    obj.transform.rotation = Quaternion.identity;
                    obj.GetComponent<TrackSelector>().SwitchChild((int)obj.GetComponent<TrackSelector>().type);
                    rows[i].tracks.Add(obj.GetComponent<TrackSelector>());
                }
            }
            else if(amountY < rows[i].tracks.Count) {
                for(int j = amountY; j < rows[i].tracks.Count; j++) {
                    DestroyImmediate(rows[i].tracks[j].gameObject);
                    rows[i].tracks.RemoveAt(j);
                }
            }

            if(rows[i].tracks.Count == 0) {
                rows.RemoveAt(i);
            }
        }

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
                    paths.Add(switchTrack.pathTwo);
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