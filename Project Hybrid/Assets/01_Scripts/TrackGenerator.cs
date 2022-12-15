using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {

    [SerializeField, Range(10, 40)]
    private int amountX = 15;
    [SerializeField, Range(10, 40)]
    private int amountY = 15;

    [SerializeField]
    private GameObject trackPrefab;

    private List<TrackRow> rows = new List<TrackRow>();

    public void Generate() {
        if(amountX > rows.Count) {
            for(int i = rows.Count; i < amountX; i++) {
                rows.Add(new TrackRow());

                for(int j = 0; j < amountY; j++) {
                    GameObject obj = Instantiate(trackPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    obj.GetComponent<TrackSelector>().SwitchChild((int)obj.GetComponent<TrackSelector>().type);
                    rows[i].tracks.Add(obj.GetComponent<TrackSelector>());
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
                    GameObject obj = Instantiate(trackPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
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


    public void ResetTracks() {
        for(int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
            rows.Clear();
        }
    }

}