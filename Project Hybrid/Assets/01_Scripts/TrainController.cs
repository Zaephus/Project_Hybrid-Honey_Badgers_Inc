using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();

    [SerializeField]
    private TrackGenerator trackGenerator;

    [SerializeField]
    private BaseTrack startTrack;

    [SerializeField]
    private TrackPath currentPath;
    [SerializeField]
    private TrackPath nextPath;

    private void Start() {
        currentPath = startTrack.path;
        nextPath = startTrack.path;
        SetPathPoints();
    }

    private void Update() {}

    private IEnumerator MoveOverPathPoints() {

        float completion = 0f;

        Quaternion startRotation = transform.rotation;

        while(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(pathPoints[2].position.x, 0, pathPoints[2].position.z)) >= 0.01f) {
            Vector3 lerpOne = Vector3.Lerp(pathPoints[0].position, pathPoints[1].position, completion);
            Vector3 lerpTwo = Vector3.Lerp(pathPoints[1].position, pathPoints[2].position, completion);

            Vector3 move = Vector3.Lerp(lerpOne, lerpTwo, completion);
            transform.position = new Vector3(move.x, transform.position.y, move.z);
            transform.localRotation = Quaternion.Lerp(startRotation, pathPoints[2].rotation, completion);

            completion += moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        transform.localRotation = pathPoints[2].rotation;
        
        if(nextPath == null || nextPath.pathPoints.Count == 0) {
            Debug.LogWarning("Dead End");
        }
        else {
            currentPath = nextPath;
            SetPathPoints();
        }

    }

    private void SetPathPoints() {

        //The train is at the start of the currentPath when this code is executed,
        //since before it was called currentPath was set to nextPath.

        //Setting the current pathpoints

        for(int i = 0; i < pathPoints.Count; i++) {
            pathPoints[i].position = currentPath.pathPoints[i].position;
            pathPoints[i].rotation = currentPath.pathPoints[i].rotation;
        }

        if(Vector3.Distance(transform.position, currentPath.pathPoints[0].position) > Vector3.Distance(transform.position, currentPath.pathPoints[2].position)) {
            pathPoints = ReversePath(pathPoints);
        }

        //Setting the nextpath
        
        StartCoroutine(MoveOverPathPoints());
        StartCoroutine(SetNextPath());

    }

    private IEnumerator SetNextPath() {

        List<TrackPath> possiblePaths = new List<TrackPath>();

        foreach(TrackPath path in trackGenerator.paths) {
            if(path.pathPoints[1].position == currentPath.pathPoints[1].position) {
                continue;
            }
            if(Vector3.Distance(pathPoints[2].position, path.pathPoints[0].position) <= 0.5f) {
                possiblePaths.Add(path);
            }
            if(Vector3.Distance(pathPoints[2].position, path.pathPoints[2].position) <= 0.5f) {
                possiblePaths.Add(path);
            }
        }

        if(possiblePaths.Count == 2) {
            if(possiblePaths[0].switchTrack != possiblePaths[1].switchTrack) {
                nextPath = possiblePaths[0];
            }
            else {
                SwitchTrack switchTrack = possiblePaths[0].switchTrack;
                if(switchTrack.path.pathPoints[2].position == possiblePaths[0].pathPoints[2].position) {
                    nextPath = possiblePaths[0];
                }
                else {
                    nextPath = possiblePaths[1];
                }
            }
        }
        else if(possiblePaths.Count == 1) {
            nextPath = possiblePaths[0];
        }
        else {
            nextPath = null;
        }

        yield return null;

    }

    private List<Transform> ReversePath(List<Transform> _pathPoints) {
        List<Transform> points = new List<Transform>();
        points.AddRange(_pathPoints);
        foreach(Transform point in points) {
            point.eulerAngles = new Vector3(point.eulerAngles.x, point.eulerAngles.y + 180, point.eulerAngles.z);
        }
        points.Reverse();
        return points;
    } 

}