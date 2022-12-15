using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackGenerator))]
public class TrackEditor : Editor {

    private TrackGenerator trackGen;

    public override void OnInspectorGUI() {

        using(var check = new EditorGUI.ChangeCheckScope()) {

            base.OnInspectorGUI();

            if(check.changed) {
                trackGen.Generate();
            }

        }

        if(GUILayout.Button("Generate Tracks")) {
            trackGen.Generate();
        }

        if(GUILayout.Button("Reset Tracks")) {
            trackGen.ResetTracks();
        }
    }

    private void OnEnable() {
        trackGen = (TrackGenerator)target;
    }

}