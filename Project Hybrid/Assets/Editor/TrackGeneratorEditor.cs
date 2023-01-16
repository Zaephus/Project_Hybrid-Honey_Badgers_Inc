using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackGenerator))]
public class TrackGeneratorEditor : Editor {

    private TrackGenerator trackGen;

    public override void OnInspectorGUI() {

        using(var check = new EditorGUI.ChangeCheckScope()) {

            base.OnInspectorGUI();

            if(check.changed) {
                Generate();
            }

        }

        if(GUILayout.Button("Generate Tracks")) {
            Generate();
        }

        if(GUILayout.Button("Reset Tracks")) {
            trackGen.ResetTracks();
        }
    }

    private void OnEnable() {
        trackGen = (TrackGenerator)target;
    }

    private void Generate() {

        if(trackGen.amountX > trackGen.rows.Count) {
            for(int i = trackGen.rows.Count; i < trackGen.amountX; i++) {
                trackGen.rows.Add(new TrackRow());

                for(int j = 0; j < trackGen.amountY; j++) {
                    GameObject obj = PrefabUtility.InstantiatePrefab(trackGen.trackPrefab) as GameObject;
                    obj.transform.SetParent(trackGen.transform);
                    obj.transform.position = new Vector3(i, 0, j);
                    obj.transform.rotation = Quaternion.identity;
                    TrackSelector selector = obj.GetComponent<TrackSelector>();
                    selector.Inititialize(trackGen);
                    trackGen.rows[i].tracks.Add(selector);
                }
            }
        }
        else if(trackGen.amountX < trackGen.rows.Count) {
            for(int i = trackGen.amountX; i < trackGen.rows.Count; i++) {
                for(int j = 0; j < trackGen.rows[i].tracks.Count; j++) {
                    DestroyImmediate(trackGen.rows[i].tracks[j].gameObject);
                }
                trackGen.rows.RemoveAt(i);
            } 
        }

        for(int i = 0; i < trackGen.rows.Count; i++) {
            if(trackGen.amountY > trackGen.rows[i].tracks.Count) {
                for(int j = trackGen.rows[i].tracks.Count; j < trackGen.amountY; j++) {
                    GameObject obj = PrefabUtility.InstantiatePrefab(trackGen.trackPrefab) as GameObject;
                    obj.transform.SetParent(trackGen.transform);
                    obj.transform.position = new Vector3(i, 0, j);
                    obj.transform.rotation = Quaternion.identity;
                    //obj.GetComponent<TrackSelector>().SwitchChild((int)obj.GetComponent<TrackSelector>().type);
                    trackGen.rows[i].tracks.Add(obj.GetComponent<TrackSelector>());
                }
            }
            else if(trackGen.amountY < trackGen.rows[i].tracks.Count) {
                for(int j = trackGen.amountY; j < trackGen.rows[i].tracks.Count; j++) {
                    DestroyImmediate(trackGen.rows[i].tracks[j].gameObject);
                    trackGen.rows[i].tracks.RemoveAt(j);
                }
            }

            if(trackGen.rows[i].tracks.Count == 0) {
                trackGen.rows.RemoveAt(i);
            }
        }

    }

}