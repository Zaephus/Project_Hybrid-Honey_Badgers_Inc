using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwitchTrack))]
public class SwitchTrackEditor : TrackEditor
{
    private SwitchTrack switchTrack;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if(check.changed || position != switchTrack.transform.position || rotation != switchTrack.transform.rotation || scale != switchTrack.transform.localScale)
            {
                position = switchTrack.transform.position;
                rotation = switchTrack.transform.rotation;
                scale = switchTrack.transform.localScale;

                switchTrack.ChangeTracksInEditor();
                switchTrack.ChangeIcon();
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        switchTrack = (SwitchTrack)target;
    }

}