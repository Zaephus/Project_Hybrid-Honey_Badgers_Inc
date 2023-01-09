using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwitchTrack))]
public class SwitchTrackEditor : Editor
{
    private SwitchTrack switchTrack;
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if (check.changed)
            {
                switchTrack.ChangeTracksInEditor();
            }
        }
    }

    private void OnEnable()
    {
        switchTrack = (SwitchTrack)target;
    }
}