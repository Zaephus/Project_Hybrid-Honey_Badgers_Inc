using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {

    [SerializeField, Range(1, 20)]
    private int amountX = 1;
    [SerializeField, Range(1, 20)]
    private int amountY = 1;

    [SerializeField]
    private GameObject trackPrefab;

    private List<List<Track>> tracks = new List<List<Track>>();

    private void OnValidate() {

        Generate();

        Debug.Log(tracks.Count);

        // if(tracks.Count != 0) {

        //     if(tracks.Count * tracks[0].Count > amountX * amountY) {

        //         for(int i = 0; i < tracks.Count; i++) {
        //             for(int j = 0; j < tracks[i].Count; j++) {

        //                 if(tracks[i][j].transform.position.x > amountX || tracks[i][j].transform.position.y > amountY) {
        //                     Track trackToRemove = tracks[i][j];
        //                     tracks[i].Remove(trackToRemove);
        //                     trackToRemove.DestroySelf();
        //                 }

        //             }

        //             if(tracks[i].Count == 0) {
        //                 tracks.RemoveAt(i);
        //             }

        //         }

        //     }

        // }
        // else {

        //     for(int x = 0; x < amountX; x++) {

        //         if(x >= tracks.Count) {
        //             tracks.Add(new List<Track>());
        //         }

        //         for(int z = 0; z < amountY; z++) {

        //             if(x < tracks.Count && z < tracks[0].Count) {
        //                 continue;
        //             }

        //             GameObject obj = Instantiate(trackPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
        //             tracks[x].Add(obj.GetComponent<Track>());

        //         }
        //     }

        // }

    }

    private void Generate() {
        if(amountX > tracks.Count) {
            for(int i = tracks.Count; i < amountX; i++) {
                tracks.Add(new List<Track>());

                for(int j = 0; j < amountY; j++) {
                    GameObject obj = Instantiate(trackPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    tracks[i].Add(obj.GetComponent<Track>());
                }
            }
        }
        else if(amountX < tracks.Count) {
            ResetTracks();
        }
    }


    private void ResetTracks() {
        foreach(Transform child in transform) {
            DestroyImmediate(child.gameObject);
            tracks.Clear();
        }
    }

}