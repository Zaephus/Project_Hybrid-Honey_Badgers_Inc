using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseTrack))]
public class TrackEditor : Editor {

    protected virtual void OnEnable() {

        GameObject targetGO = ((BaseTrack)target).gameObject;
        SceneView sceneView = EditorWindow.focusedWindow as SceneView;
        
        if (targetGO.transform.parent != null && sceneView != null) {
            GameObject[] currentSelection = Selection.gameObjects;
            int idx = -1;
            for (int i = 0; i < Selection.gameObjects.Length; i++) {
                if (Selection.gameObjects[i].GetInstanceID() == targetGO.GetInstanceID()) {
                    idx = i;
                }
            }
            if (idx != -1) {
                currentSelection[idx] = targetGO.transform.parent.gameObject;;
                Selection.objects = currentSelection;
            }
            return;
        }

    }

}