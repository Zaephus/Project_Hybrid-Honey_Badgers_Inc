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

                SwitchChild((int)selector.type);
                selector.RotateChild((int)selector.rotation);
                selector.generator.FillPathList();
                selector.generator.SetStartTrack();

                if(selector.type >= TrackType.Switch_StraightLeft) {
                    SwitchTrack switchTrack = selector.track as SwitchTrack;
                    switchTrack.pathOne.switchTrack = switchTrack;
                    switchTrack.pathTwo.switchTrack = switchTrack;
                }

            }
        }
    }

    private void SwitchChild(int index)
    {
        if (Application.isEditor && selector.child != null)
        {
            DestroyImmediate(selector.child);
        }
        selector.child = PrefabUtility.InstantiatePrefab(selector.trackTypes[index]) as GameObject;
        selector.child.transform.SetParent(selector.transform);
        selector.child.transform.position = selector.transform.position;
        selector.track = selector.child.GetComponent<BaseTrack>();
    }

    private void OnEnable()
    {
        selector = (TrackSelector)target;
    }

}