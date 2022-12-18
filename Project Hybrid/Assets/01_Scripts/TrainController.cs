using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    private Transform[] pathPoints = new Transform[3];

    [SerializeField]
    private BaseTrack startTrack;
    [SerializeField]
    private BaseTrack currentTrack;

    private BaseTrack nextTrack;

    private bool isReversed;

    private void Start() {
        currentTrack = startTrack;
        nextTrack = startTrack;
        SetPath();
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

        transform.localRotation = pathPoints[2].rotation;
        
        if(nextTrack == null) {
            Debug.LogWarning("Dead End");
        }
        else {
            SetPath();
        }

    }

    private void SetPath() {

        if(nextTrack != null) {
            currentTrack = nextTrack;
        }
        
        pathPoints = currentTrack.pathPoints;

        if(currentTrack is SwitchTrack) {

            SwitchTrack switchTrack = (SwitchTrack)currentTrack;

            if(Vector3.Distance(transform.position, switchTrack.pathOne[0].position) > Vector3.Distance(transform.position, switchTrack.pathOne[2].position) || Vector3.Distance(transform.position, switchTrack.pathTwo[0].position) > Vector3.Distance(transform.position, switchTrack.pathTwo[2].position)) {
                
                if(Vector3.Distance(switchTrack.pathOne[0].position, pathPoints[0].position) <= 0.1f) {
                    pathPoints = switchTrack.pathOne;
                }
                else {
                    pathPoints = switchTrack.pathTwo;
                }
                
                pathPoints = ReversePath(pathPoints);
                nextTrack = currentTrack.previousTrack;

            }
            else {
                nextTrack = currentTrack.nextTrack;
            }

        }
        else {
            if(Vector3.Distance(transform.position, pathPoints[0].position) > Vector3.Distance(transform.position, pathPoints[2].position)) {
                pathPoints = ReversePath(pathPoints);
                nextTrack = currentTrack.previousTrack;
            }
            else {
                nextTrack = currentTrack.nextTrack;
            }
        }

        StartCoroutine(MoveOverPathPoints());

    }

    private Transform[] ReversePath(Transform[] _pathPoints) {
        foreach(Transform point in _pathPoints) {
            point.eulerAngles = new Vector3(point.eulerAngles.x, point.eulerAngles.y - 180, point.eulerAngles.z);
        }
        System.Array.Reverse(_pathPoints);
        return _pathPoints;
    } 

}