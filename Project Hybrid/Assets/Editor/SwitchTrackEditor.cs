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
                ChangeIcon();
            }
        }
    }

    private void OnEnable()
    {
        switchTrack = (SwitchTrack)target;
    }

    private void ChangeIcon() {
        if(switchTrack.iconTransform.childCount != 0) {
            GameObject child = switchTrack.iconTransform.GetChild(0).gameObject;
            DestroyImmediate(child);
        }

        switch(switchTrack.inputType) {

            case SwitchTrack.InputType.Green:
                Instantiate(switchTrack.greenInputIcon, switchTrack.iconTransform.position, Quaternion.identity, switchTrack.iconTransform);
                break;

            case SwitchTrack.InputType.Yellow:
                Instantiate(switchTrack.yellowInputIcon, switchTrack.iconTransform.position, Quaternion.identity, switchTrack.iconTransform);
                break;

            case SwitchTrack.InputType.Blue:
                Instantiate(switchTrack.blueInputIcon, switchTrack.iconTransform.position, Quaternion.identity, switchTrack.iconTransform);
                break;

            case SwitchTrack.InputType.Red:
                Instantiate(switchTrack.redInputIcon, switchTrack.iconTransform.position, Quaternion.identity, switchTrack.iconTransform);
                break;

        }
    }

}