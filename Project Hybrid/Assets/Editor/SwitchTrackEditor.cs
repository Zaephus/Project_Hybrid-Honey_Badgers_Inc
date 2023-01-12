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
                ChangeIcon();
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        switchTrack = (SwitchTrack)target;
    }

    private void ChangeIcon() {

        switch(switchTrack.inputType) {

            case SwitchTrack.InputType.Green:
                switchTrack.greenInputIcon.SetActive(true);
                switchTrack.yellowInputIcon.SetActive(false);
                switchTrack.blueInputIcon.SetActive(false);
                switchTrack.redInputIcon.SetActive(false);

                switchTrack.greenInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.yellowInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.blueInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.redInputIcon.transform.rotation = Quaternion.identity;
                break;

            case SwitchTrack.InputType.Yellow:
                switchTrack.greenInputIcon.SetActive(false);
                switchTrack.yellowInputIcon.SetActive(true);
                switchTrack.blueInputIcon.SetActive(false);
                switchTrack.redInputIcon.SetActive(false);

                switchTrack.greenInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.yellowInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.blueInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.redInputIcon.transform.rotation = Quaternion.identity;
                break;

            case SwitchTrack.InputType.Blue:
                switchTrack.greenInputIcon.SetActive(false);
                switchTrack.yellowInputIcon.SetActive(false);
                switchTrack.blueInputIcon.SetActive(true);
                switchTrack.redInputIcon.SetActive(false);

                switchTrack.greenInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.yellowInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.blueInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.redInputIcon.transform.rotation = Quaternion.identity;
                break;

            case SwitchTrack.InputType.Red:
                switchTrack.greenInputIcon.SetActive(false);
                switchTrack.yellowInputIcon.SetActive(false);
                switchTrack.blueInputIcon.SetActive(false);
                switchTrack.redInputIcon.SetActive(true);

                switchTrack.greenInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.yellowInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.blueInputIcon.transform.rotation = Quaternion.identity;
                switchTrack.redInputIcon.transform.rotation = Quaternion.identity;
                break;

        }
    }

}