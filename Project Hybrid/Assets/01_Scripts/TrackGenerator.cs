using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {

    [SerializeField, Range(10, 40)]
    private int amountX = 15;
    [SerializeField, Range(10, 40)]
    private int amountY = 15;

    [SerializeField]
    private GameObject trackPrefab;

    [SerializeField, HideInInspector]
    private List<TrackRow> rows = new List<TrackRow>();

    public void Generate() {
        if(amountX > rows.Count) {
            for(int i = rows.Count; i < amountX; i++) {
                rows.Add(new TrackRow());

                for(int j = 0; j < amountY; j++) {
                    GameObject obj = Instantiate(trackPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    TrackSelector selector = obj.GetComponent<TrackSelector>();
                    selector.Inititialize(this);
                    rows[i].tracks.Add(selector);
                }
            }
        }
        else if(amountX < rows.Count) {
            for(int i = amountX; i < rows.Count; i++) {
                for(int j = 0; j < rows[i].tracks.Count; j++) {
                    DestroyImmediate(rows[i].tracks[j].gameObject);
                }
                rows.RemoveAt(i);
            } 
        }

        for(int i = 0; i < rows.Count; i++) {

            if(amountY > rows[i].tracks.Count) {
                for(int j = rows[i].tracks.Count; j < amountY; j++) {
                    GameObject obj = Instantiate(trackPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    obj.GetComponent<TrackSelector>().SwitchChild((int)obj.GetComponent<TrackSelector>().type);
                    rows[i].tracks.Add(obj.GetComponent<TrackSelector>());
                }
            }
            else if(amountY < rows[i].tracks.Count) {
                for(int j = amountY; j < rows[i].tracks.Count; j++) {
                    DestroyImmediate(rows[i].tracks[j].gameObject);
                    rows[i].tracks.RemoveAt(j);
                }
            }

            if(rows[i].tracks.Count == 0) {
                rows.RemoveAt(i);
            }

        }

    }

    public void SetConnections() {

        for(int x = 0; x < rows.Count; x++) {
            for(int y = 0; y < rows[x].tracks.Count; y++) {
                
                TrackSelector selector = rows[x].tracks[y];

                if(selector.type == 0) {
                    continue;
                }

                if(selector.type >= TrackType.Switch_StraightLeft) {

                    SwitchTrack switchTrack = selector.track as SwitchTrack;

                    Vector3 trackEndPosOne = switchTrack.pathOne[2].position;
                    Vector3 trackEndPosTwo = switchTrack.pathTwo[2].position;
                    BaseTrack otherTrackOne = null;
                    BaseTrack otherTrackTwo = null;

                    if(!(x-1 < 0 || rows[x-1].tracks[y].type == 0)) {
                        if(trackEndPosOne == rows[x-1].tracks[y].track.pathPoints[0].position || trackEndPosOne == rows[x-1].tracks[y].track.pathPoints[2].position) {
                            otherTrackOne = rows[x-1].tracks[y].track;
                        }
                        else if(trackEndPosTwo == rows[x-1].tracks[y].track.pathPoints[0].position || trackEndPosTwo == rows[x-1].tracks[y].track.pathPoints[2].position) {
                            otherTrackTwo = rows[x-1].tracks[y].track;
                        }
                    }

                    if(!(x+1 >= rows.Count || rows[x+1].tracks[y].type == 0)) {
                        if(trackEndPosOne == rows[x+1].tracks[y].track.pathPoints[0].position || trackEndPosOne == rows[x+1].tracks[y].track.pathPoints[2].position) {
                            otherTrackOne = rows[x+1].tracks[y].track;
                        }
                        else if(trackEndPosTwo == rows[x+1].tracks[y].track.pathPoints[0].position || trackEndPosTwo == rows[x+1].tracks[y].track.pathPoints[2].position) {
                            otherTrackTwo = rows[x+1].tracks[y].track;
                        }
                    }

                    if(!(y-1 < 0 || rows[x].tracks[y-1].type == 0)) {
                        if(trackEndPosOne == rows[x].tracks[y-1].track.pathPoints[0].position || trackEndPosOne == rows[x].tracks[y-1].track.pathPoints[2].position) {
                            otherTrackOne = rows[x].tracks[y-1].track;
                        }
                        else if(trackEndPosTwo == rows[x].tracks[y-1].track.pathPoints[0].position || trackEndPosTwo == rows[x].tracks[y-1].track.pathPoints[2].position) {
                            otherTrackTwo = rows[x].tracks[y-1].track;
                        }
                    }

                    if(!(y+1 >= rows[x].tracks.Count || rows[x].tracks[y+1].type == 0)) {
                        if(trackEndPosOne == rows[x].tracks[y+1].track.pathPoints[0].position || trackEndPosOne == rows[x].tracks[y+1].track.pathPoints[2].position) {
                            otherTrackOne = rows[x].tracks[y+1].track;
                        }
                        else if(trackEndPosTwo == rows[x].tracks[y+1].track.pathPoints[0].position || trackEndPosTwo == rows[x].tracks[y+1].track.pathPoints[2].position) {
                            otherTrackTwo = rows[x].tracks[y+1].track;
                        }
                    }

                    if(otherTrackOne != null) {
                        switchTrack.nextTrackOne = otherTrackOne;
                        if(otherTrackOne.nextTrack != switchTrack) {
                            otherTrackOne.previousTrack = switchTrack;
                        }
                    }

                    if(otherTrackTwo != null) {
                        switchTrack.nextTrackTwo = otherTrackTwo;
                        if(otherTrackTwo.nextTrack != switchTrack) {
                            otherTrackTwo.previousTrack = switchTrack;
                        }
                    }

                    switchTrack.ChangeTracksInEditor();
                    
                }
                else {

                    Vector3 trackEndPos = selector.track.pathPoints[2].position;
                    BaseTrack otherTrack = null;

                    if(!(x-1 < 0 || rows[x-1].tracks[y].type == 0)) {
                        if(trackEndPos == rows[x-1].tracks[y].track.pathPoints[0].position || trackEndPos == rows[x-1].tracks[y].track.pathPoints[2].position) {
                            otherTrack = rows[x-1].tracks[y].track;
                        }
                    }

                    if(!(x+1 >= rows.Count || rows[x+1].tracks[y].type == 0)) {
                        if(trackEndPos == rows[x+1].tracks[y].track.pathPoints[0].position || trackEndPos == rows[x+1].tracks[y].track.pathPoints[2].position) {
                            otherTrack = rows[x+1].tracks[y].track;
                        }
                    }

                    if(!(y-1 < 0 || rows[x].tracks[y-1].type == 0)) {
                        if(trackEndPos == rows[x].tracks[y-1].track.pathPoints[0].position || trackEndPos == rows[x].tracks[y-1].track.pathPoints[2].position) {
                            otherTrack = rows[x].tracks[y-1].track;
                        }
                    }

                    if(!(y+1 >= rows[x].tracks.Count || rows[x].tracks[y+1].type == 0)) {
                        if(trackEndPos == rows[x].tracks[y+1].track.pathPoints[0].position || trackEndPos == rows[x].tracks[y+1].track.pathPoints[2].position) {
                            otherTrack = rows[x].tracks[y+1].track;
                        }
                    }

                    if(otherTrack != null) {
                        selector.track.nextTrack = otherTrack;
                        otherTrack.previousTrack = selector.track;
                    }

                }

            }
        }

    }

    public void ResetTracks() {
        for(int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
            rows.Clear();
        }
    }

}