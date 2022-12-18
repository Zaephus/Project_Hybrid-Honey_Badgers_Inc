using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackSelector))]
public class TrackSelectorEditor : Editor
{
    private TrackSelector selector;

    public override void OnInspectorGUI()
    {

        using (var check = new EditorGUI.ChangeCheckScope())
        {

            base.OnInspectorGUI();

            if (check.changed)
            {
                selector.SwitchChild((int)selector.type);
                selector.RotateChild((int)selector.rotation);
                selector.generator.SetConnections();
            }
        }
    }

    private void OnEnable()
    {
        selector = (TrackSelector)target;
    }

}

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
